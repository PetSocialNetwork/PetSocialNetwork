using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PetSocialNetwork.API.Configurations;
using PetSocialNetwork.API.Services;
using PetSocialNetwork.API.Validators;
using PetSocialNetwork.Data;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Microsoft.AspNetCore.Hosting;

namespace PetSocialNetwork.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            JwtConfig jwtConfig = builder.Configuration
                .GetRequiredSection("JwtConfig")
                .Get<JwtConfig>();
            if (jwtConfig is null)
            {
                throw new InvalidOperationException("JwtConfig is not configured");
            }
            builder.Services.AddSingleton(jwtConfig);


            TelegramBotConfig telegramBot = builder.Configuration
               .GetRequiredSection("TelegramBotConfig")
               .Get<TelegramBotConfig>();
            if (telegramBot is null)
            {
                throw new InvalidOperationException("TelegramBot is not configured");
            }
            builder.Services.AddSingleton(telegramBot);


            builder.Services.AddControllersWithViews();
            builder.Services.AddCors(
                options => options.AddDefaultPolicy(
                    cors => cors
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()));

            builder.Services.AddControllers();

            builder.Services.AddDbContext<PetSocialNetworkDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddSingleton<ITokenService, TokenService>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        RequireSignedTokens = true,
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidAudiences = new[] { jwtConfig.Audience },
                        ValidIssuer = jwtConfig.Issuer
                    };
                });

            builder.Services.AddAuthorization();

            builder.Services.AddValidatorsFromAssemblyContaining<UserProfileValidator>();
            builder.Services.AddFluentValidationAutoValidation();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}