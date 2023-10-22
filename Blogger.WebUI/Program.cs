using Blazored.LocalStorage;
using Blazored.Toast;
using Blogger.Application.Extensions;
using Blogger.Infrastructure.Extensions;
using Blogger.Persistence.Extensions;
using Blogger.WebUI;
using Blogger.WebUI.Handler;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5186") });
builder.Services.AddHttpClient("blog", options =>
{
	options.BaseAddress = new Uri("http://localhost:5186/");
}).AddHttpMessageHandler<CustomAuthorizationHandler>();

// Add services to the container.
builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureServiceLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddTransient<CustomAuthorizationHandler>();

builder.Services.AddBlazoredToast();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
