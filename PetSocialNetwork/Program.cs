using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.Configurations;
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

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

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
