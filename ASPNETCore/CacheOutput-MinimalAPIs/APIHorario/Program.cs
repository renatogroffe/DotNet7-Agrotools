using Microsoft.OpenApi.Models;
using APIHorario.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "APIHorario",
        Description = "APIHorario - Testando Output Cache middleware com Minimal APIs",
        Version = "v1"
    });
});

builder.Services.AddOutputCache(options =>
{
    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(5);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIHorario v1");
});

app.MapGet("/nocache", () =>
{
    var result = new Resultado() { Mensagem = "Teste sem cache" };
    app.Logger.LogInformation($"{result.HorarioAtual} {result.Mensagem}");
    return result;
});

app.MapGet("/cache", () =>
{
    var result = new Resultado() { Mensagem = "Teste com cache" };
    app.Logger.LogInformation($"{result.HorarioAtual} {result.Mensagem}");
    return result;
}).CacheOutput();

app.MapGet("/cachequerystring", (string valorTeste) =>
{
    var result = new Resultado()
    {
        Mensagem = $"Teste com cache | Query string: {nameof(valorTeste)} = {valorTeste}"
    };
    app.Logger.LogInformation($"{result.HorarioAtual} {result.Mensagem}");
    return result;
})
.CacheOutput(policy =>
{
    policy.SetVaryByQuery("valorTeste");
    policy.Expire(TimeSpan.FromSeconds(15));
});

app.UseOutputCache();

app.Run();