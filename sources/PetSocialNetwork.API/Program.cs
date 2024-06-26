using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.API.DTOs;
using PetSocialNetwork.API.Validators;
using PetSocialNetwork.Data;

namespace PetSocialNetwork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(
                options => options.AddDefaultPolicy(
                    cors => cors
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()));

            builder.Services.AddControllers();

            builder.Services.AddDbContext<PetSocialNetworkDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddScoped<IValidator<UserProfileDTO>, UserProfileValidator>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
