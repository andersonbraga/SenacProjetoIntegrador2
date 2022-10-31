using Controle_Estoque.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);
//var sqlConnectionConfiguration = new SqlConnectionConfiguration (Configuration.GetConnectionString("cadastro_db"));
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<SqlConnectionConfiguration>(); //AddSingleton.Obter a conex�o uma unica vez. Obtem a conex�o
builder.Services.AddSingleton<IDapperDal, DapperDAL>(); // Registro do Servi�o da camada acesso dados
builder.Services.AddScoped<IProdutoService, ProdutoService>(); // Para usar na implementa��o do servi�o,

//Services.AddSingleton<WeatherForecastService>();

var app = builder.Build();
//var sqlConnectionConfiguration = new SqlConnectionConfiguration(builder.Configuration.GetConnectionString("default")); // String de conex�o

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
