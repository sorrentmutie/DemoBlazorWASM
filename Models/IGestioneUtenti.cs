using System.Net.Http.Json;

namespace DemoBlazorWASM.Models;

public interface IGestioneUtenti
{
    string Salute(string name);
    Task<ReqResResponse?> GetDataAsync();
    void Cancel();
    Task<int> Create(ReqResRequest reqRes);
}

public class GestioneUtenti : IGestioneUtenti
{
    private readonly IHttpClientFactory httpClientFactory;
    private CancellationTokenSource cancellationToken = new CancellationTokenSource();

    public GestioneUtenti(IHttpClientFactory httpClientFactory)
    {
        this.httpClientFactory = httpClientFactory;

    }

    public void Cancel()
    {
        cancellationToken.Cancel();
    }

    public async Task<int> Create(ReqResRequest reqRes)
    {
        var httpClient = httpClientFactory.CreateClient("reqres");
        var response = await httpClient.PostAsJsonAsync("users", reqRes);
        if(response.IsSuccessStatusCode)
        {
            return (int) response.StatusCode;
        } else
        {
            return 500;
        }
    }

    public async Task<ReqResResponse?> GetDataAsync()
    {
        var httpClient = httpClientFactory.CreateClient("reqres");
        var response = await httpClient.GetAsync("users?delay=10", HttpCompletionOption.ResponseHeadersRead,
             cancellationToken.Token);
        if(response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ReqResResponse>();
        }
        return null;
    }

    public string Salute(string name)
    {
        return $"Ciao, {name}";
    }
}