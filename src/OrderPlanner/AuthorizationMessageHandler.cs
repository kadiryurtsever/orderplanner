using Blazored.LocalStorage;

public class AuthorizationMessageHandler : DelegatingHandler
{
    ILocalStorageService _storageService;
    public AuthorizationMessageHandler(ILocalStorageService storageService)
    {
        _storageService = storageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if(await _storageService.ContainKeyAsync("access_token"))
        {
            var token = await _storageService.GetItemAsStringAsync("access_token");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        Console.WriteLine("Authorization Message Handler was called.");
        return await base.SendAsync(request, cancellationToken);
    }
}
