using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using PetSocialNetwork.App;
using PetSocialNetwork.App.Mediator.Handlers.QueryHandlers.UserProfile;
using PetSocialNetwork.App.Mediator.Queries;
using PetSocialNetwork.App.Services.Auth;
using PetSocialNetwork.App.Services.UserProfile;
using PetSocialNetwork.App.Services.UserProfile.Contracts;
using PetSocialNetwork.App.ViewModel;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddMediatR(opt=>opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IPipelineBehavior<GetUserProfileQuery, UserProfileViewModel>, GetUserProfileQueryHandler>();

// Регистрация IHttpClientFactory
//builder.Services.AddHttpClient("BaseAPI", client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl"));
//});

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl"))});
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();