using System.Text.Json;
using ExemploJsonPolymorphism.Models;

Console.WriteLine("**** Melhorias na serializacao/desserializacao JSON em .NET 7 ****");

static string GetResultString<T>(string nomeInstancia, T instancia) =>
    $"{nomeInstancia}: {JsonSerializer.Serialize<T>(instancia)} | {instancia!.GetType().FullName}";

var pessoa = new Pessoa()
{
    Nome = "Joao Silva",
    CPF = "11111111111"
};
Console.WriteLine(GetResultString<Pessoa>(nameof(pessoa), pessoa));

Console.WriteLine();
var aluno = new Aluno()
{
    Nome = "Renato Groffe",
    CPF = "22222222222",
    Curso = "Terraform"
};
var jsonPessoa2 = JsonSerializer.Serialize<Pessoa>(aluno);
Console.WriteLine($"Antes da desserializacao: {jsonPessoa2}");
var pessoa2 = JsonSerializer.Deserialize<Pessoa>(jsonPessoa2);
Console.WriteLine(GetResultString<Pessoa>(nameof(pessoa2), pessoa2!));

Console.WriteLine();
Console.WriteLine("Termino dos testes com os tipos Pessoa e Aluno...");

Console.WriteLine();
var veiculo = new Veiculo()
{
    Codigo = "00001",
    Descricao = "Aviao Embraer Super Tucano"
};
Console.WriteLine(GetResultString<Veiculo>(nameof(veiculo), veiculo));

Console.WriteLine();
var carro = new Carro()
{
    Codigo = "00002",
    Descricao = "Toyota Prius",
    Hibrido = true
};
var jsonVeiculo2 = JsonSerializer.Serialize<Veiculo>(carro);
Console.WriteLine($"Antes da desserializacao: {jsonVeiculo2}");
var veiculo2 = JsonSerializer.Deserialize<Veiculo>(jsonVeiculo2);
Console.WriteLine(GetResultString<Veiculo>(nameof(veiculo2), veiculo2!));

Console.WriteLine();
var carroCorrida = new CarroCorrida()
{
    Codigo = "00003",
    Descricao = "Mercedes 2020",
    Hibrido = false
};
var jsonVeiculo3 = JsonSerializer.Serialize<Veiculo>(carroCorrida);
Console.WriteLine($"Antes da desserializacao: {jsonVeiculo3}");
var veiculo3 = JsonSerializer.Deserialize<Veiculo>(jsonVeiculo3);
Console.WriteLine(GetResultString<Veiculo>(nameof(veiculo3), veiculo3!));

Console.WriteLine();
Console.WriteLine("Termino dos testes com os tipos Veiculo, Carro e CarroCorrida...");