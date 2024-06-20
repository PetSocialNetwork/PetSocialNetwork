using Microsoft.EntityFrameworkCore;
using PetSocialNetwork.Data;

public class Program
{
    private static readonly string Env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
                                         Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ??
                                         "Development";
    
    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        var builder = Host
            .CreateDefaultBuilder(args)
            .UseEnvironment(Env);

        builder.ConfigureAppConfiguration(Configure);
        builder.ConfigureServices(ConfigureServices);
        
        return builder;
    }
    
    public static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        using var scope = host.Services.CreateScope();

        await scope.ServiceProvider
            .GetRequiredService<PetSocialNetworkDbContext>()
            .Database.MigrateAsync();
    }

    private static void Configure(IConfigurationBuilder configurationBuilder)
    {
        var settingsPath = Path.Combine(
            Path.GetDirectoryName(Environment.ProcessPath!)!,
            "AppSettings");
        
        configurationBuilder
            .AddJsonFile(
                Path.Combine(settingsPath, "appsettings.json"))
            .AddJsonFile(
                Path.Combine(settingsPath, $"appsettings.{Env}.json"))
            .AddEnvironmentVariables();
    }

    private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services) =>
        services.AddDbContext<PetSocialNetworkDbContext>(
            builder => builder.UseNpgsql(ctx.Configuration.GetConnectionString("Default"),
                options => options.MigrationsAssembly(typeof(Program).Assembly.FullName)));
}