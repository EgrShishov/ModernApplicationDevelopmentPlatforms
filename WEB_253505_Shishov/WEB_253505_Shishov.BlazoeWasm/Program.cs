using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WEB_253505_Shishov.BlazoeWasm;
using WEB_253505_Shishov.BlazoeWasm.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetSection("DataApi").Value) });

builder.Services.AddOidcAuthentication(options =>
{
    //builder.Configuration.Bind("Local", options.ProviderOptions);
    builder.Configuration.Bind("Keycloak", options.ProviderOptions);
});

builder.Services.AddScoped<IDataService, DataService>();

await builder.Build().RunAsync();
