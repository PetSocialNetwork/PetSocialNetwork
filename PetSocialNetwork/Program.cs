using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.Configurations;
using PetSocialNetwork.Data;
using System;

namespace PetSocialNetwork
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var postgresConfig = builder.Configuration
               .GetRequiredSection("PostgresConfig")
               .Get<PostgresConfig>();
            if (postgresConfig is null)
            {
                throw new InvalidOperationException("PostgresConfig is not configured");
            }

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql
                ($"Host={postgresConfig.ServerName};Port={postgresConfig.Port};Database={postgresConfig.DatabaseName};Username={postgresConfig.UserName};Password={postgresConfig.Password};"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
