using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PetSocialNetwork.Data.Migrator;

public class PetSocialNetworkDbContextFactory : IDesignTimeDbContextFactory<PetSocialNetworkDbContext>
{
    public PetSocialNetworkDbContext CreateDbContext(string[] args)
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ??
                  Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
                  "Development";
        
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("AppSettings/appsettings.json")
            .AddJsonFile($"AppSettings/appsettings.{env}.json")
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<PetSocialNetworkDbContext>()
            .UseNpgsql(
                configuration.GetConnectionString("Default"),
                options => options.MigrationsAssembly(GetType().Assembly.FullName));

        return new PetSocialNetworkDbContext(optionsBuilder.Options);
    }
}