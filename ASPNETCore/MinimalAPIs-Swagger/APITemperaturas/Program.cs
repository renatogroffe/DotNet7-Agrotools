using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using APITemperaturas.Models;

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
    (double temperaturaFahrenheit) =>
    {
        if (temperaturaFahrenheit < -459.67)
        {
            app.Logger.LogError($"Valor de temperatura em Fahrenheit inválido: {temperaturaFahrenheit}");
            return Results.BadRequest();
        }
        var result = new Temperatura(temperaturaFahrenheit);
        app.Logger.LogInformation($"{result.Fahrenheit} graus Fahrenheit = " +
            $"{result.Celsius} graus Celsius = " +
            $"{result.Kelvin} graus Kelvin");
        return Results.Ok(result);
    })
    .WithOpenApi(operation => {
        operation.Description =
            "Conversão de temperaturas em Fahrenheit para os valores equivalentes em graus Celsius e Kelvin";
        operation.Summary = "Efetua a conversão de uma temperatura em Fahrenheit";
        operation.Parameters[0].Description = "Temperatura em graus Fahrenheit a ser convertida";
        operation.Parameters[0].AllowEmptyValue = false;       
        operation.Responses = new OpenApiResponses
        {
            ["200"] = new OpenApiResponse
            {
                Description = "Resultado da conversão (com valores em Celsius e Kelvin)"
            },
            ["400"] = new OpenApiResponse
            {
                Description = "Temperatura em Fahrenheit inválida"
            }                       
        };
        return operation;
    });

app.Run();