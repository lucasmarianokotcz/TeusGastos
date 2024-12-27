using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TeusGastos.Shared.Servicos.MercadoServico;
using TeusGastos.Shared.Servicos.UnidadeMedidaServico;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IUnidadeMedidaServico, UnidadeMedidaServicoHttp>();
builder.Services.AddScoped<IMercadoServico, MercadoServicoHttp>();

await builder.Build().RunAsync();