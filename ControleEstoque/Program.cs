using LidyDecorApp.Web;
using LidyDecorApp.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ClientesService>();
builder.Services.AddScoped<ProdutosService>();
builder.Services.AddScoped<UsuariosService>();
builder.Services.AddScoped<OrcamentosService>();
builder.Services.AddScoped<SharedService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
