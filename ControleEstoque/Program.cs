using LidyDecorApp.Web;
using LidyDecorApp.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ClientesService>();
builder.Services.AddScoped<ProdutosService>();
builder.Services.AddScoped<UsuariosService>();
builder.Services.AddScoped<OrcamentosService>();
builder.Services.AddScoped<ServicosService>();
builder.Services.AddScoped<SharedService>();
builder.Services.AddScoped<AuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthStateProvider>());

builder.Services.AddTransient<UnauthorizedRedirectHandler>();

builder.Services.AddScoped(sp =>
{
    var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress;
    
    var redirectHandler = sp.GetRequiredService<UnauthorizedRedirectHandler>();
    redirectHandler.InnerHandler = new HttpClientHandler();

    var httpClient = new HttpClient(redirectHandler) { BaseAddress = new Uri(apiBaseUrl) };

    // Adicionar Interceptor para Token
    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");

    return httpClient;
});

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
