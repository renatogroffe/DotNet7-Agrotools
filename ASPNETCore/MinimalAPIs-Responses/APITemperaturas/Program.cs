using Microsoft.OpenApi.Models;
using APITemperaturas.Models;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "APITemperaturas",
            Description = "API de conversão de temperaturas implementada com .NET 7 + Minimal APIs", 
            Version = "v1",
            Contact = new OpenApiContact()
            {
                Name = "Renato Groffe",
                Url = new Uri("https://github.com/renatogroffe"),
            },
            License = new OpenApiLicense()
            {
                Name = "MIT",
                Url = new Uri("http://opensource.org/licenses/MIT"),
            }
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "APITemperaturas v1");
});

app.MapGet("/ConversorTemperaturas/Fahrenheit/{temperaturaFahrenheit}",
    Results<Ok<Temperatura>, BadRequest> (double temperaturaFahrenheit) =>
    {
        if (temperaturaFahrenheit < -459.67)
        {
            app.Logger.LogError($"Valor de temperatura em Fahrenheit inválido: {temperaturaFahrenheit}");
            return TypedResults.BadRequest();
        }
        var result = new Temperatura(temperaturaFahrenheit);
        app.Logger.LogInformation($"{result.Fahrenheit} graus Fahrenheit = " +
            $"{result.Celsius} graus Celsius = " +
            $"{result.Kelvin} graus Kelvin");
        return TypedResults.Ok(result);
    });

app.Run();