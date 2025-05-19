using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UrlShortener.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register HttpClient for DI (used in Index.razor)
builder.Services.AddScoped(sp => new System.Net.Http.HttpClient
{
    BaseAddress = new System.Uri("http://localhost:30080/") // API base URL
});

await builder.Build().RunAsync();