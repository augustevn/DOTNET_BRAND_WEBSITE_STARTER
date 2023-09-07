using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Brand.Spa;
using NewsletterSubscribers.SharedSpa;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddNewsletterSubscribersSharedSpa(builder.Configuration);

var apiUrl = builder.Configuration.GetValue<string>("ApiUrl") ?? string.Empty;
builder.Services.AddScoped(sp => new HttpClient {BaseAddress = new Uri(apiUrl)});

await builder.Build().RunAsync();