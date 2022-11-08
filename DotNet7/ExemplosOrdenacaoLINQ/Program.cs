using System.Text.Json;

Console.WriteLine("**** Melhorias na ordenacao com LINQ em .NET 7 ****");

static void PrintResults(string descritivo,
    IEnumerable<string> tecnologias,
    IEnumerable<int> copasFutebol,
    IEnumerable<double> precos)
{
    Console.WriteLine();
    Console.WriteLine($"**** {descritivo} ****");
    Console.WriteLine($"{nameof(tecnologias)} = {JsonSerializer.Serialize(tecnologias)}");
    Console.WriteLine($"{nameof(copasFutebol)} = {JsonSerializer.Serialize(copasFutebol)}");
    Console.WriteLine($"{nameof(precos)} = {JsonSerializer.Serialize(precos)}");
};

var tecnologias = new string[] { "C#", "Visual Studio Code", "ASP.NET Core", "Microsoft Azure" };
var copasFutebol = new int[] { 1970, 1962, 2002, 1958, 1994 };
var precos = new double[] { 5.70, 5.62, 5.50, 5.88, 5.68 };

PrintResults("Ordem original", tecnologias, copasFutebol, precos);
PrintResults("Order", tecnologias.Order(),
    copasFutebol.Order(), precos.Order());
PrintResults("OrderDescending", tecnologias.OrderDescending(),
    copasFutebol.OrderDescending(), precos.OrderDescending());