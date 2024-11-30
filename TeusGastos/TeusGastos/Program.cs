using MudBlazor.Services;
using Scalar.AspNetCore;
using TeusGastos.Components;
using TeusGastos.Shared.Contexto;
using TeusGastos.Shared.Servicos.UnidadeMedidaServico;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddMudServices();
builder.Services.AddControllers();
builder.Services.AddScoped<IUnidadeMedidaServico, UnidadeMedidaServicoRepositorio>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(TeusGastos.Client._Imports).Assembly);

app.Run();