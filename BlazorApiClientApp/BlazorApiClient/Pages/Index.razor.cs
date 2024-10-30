using BlazorApiClient.Models;

namespace BlazorApiClient.Pages;

public partial class Index
{
    private AuthenticationModel login = new();
    private bool isLoggedIn = false;

    protected override void OnInitialized()
    {
        isLoggedIn = !string.IsNullOrWhiteSpace(tokeninfo.Token);
    }

    private async void HandleValidSubmit()
    {
        // Create Client
        var client = factory.CreateClient("api");

        //Call the Method
        var info = await client.PostAsJsonAsync<AuthenticationModel>("Authentication/Token", login);
        tokeninfo.Token = await info.Content.ReadAsStringAsync();
        isLoggedIn = true;

        await InvokeAsync(StateHasChanged);
    }

    private void LogOut()
    {
        tokeninfo.Token = "";
        isLoggedIn = false;
    }
}