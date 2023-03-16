using DemoBlazorWASM;
using DemoBlazorWASM.Models;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Polly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddScoped<IGestioneUtenti, GestioneUtenti>();
builder.Services.AddHttpClient("reqres", client =>
{
    client.BaseAddress = new Uri("https://reqres.in/api/");
})
.AddTransientHttpErrorPolicy (b => b.WaitAndRetryAsync(new[]
{
     TimeSpan.FromSeconds(5),
     TimeSpan.FromSeconds(10),
     TimeSpan.FromSeconds(25)
}));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
