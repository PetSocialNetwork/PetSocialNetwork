using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace PetSocialNetwork.App.Services.Auth;

public class AuthStateProvider : AuthenticationStateProvider
{
    private const string TokenLocalStorageKey = "PetSocialNetwork.Auth.Token";
    
    private readonly IJSRuntime _javaScript;
    private readonly HttpClient _httpClient;

    public AuthStateProvider(IJSRuntime javaScript, HttpClient httpClient)
    {
        _javaScript = javaScript;
        _httpClient = httpClient;
    }

    
    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        throw new NotImplementedException();
        // var token = await _javaScript.InvokeAsync<string>("localStorage.getItem", "authToken");
        //
        // if (string.IsNullOrEmpty(token))
        // {
        //     throw new Exception();
        //     // return new AuthenticationState(
        //     //     new ClaimsPrincipal(
        //     //         new ClaimsIdentity()));
        // }
    }
}