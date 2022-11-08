using System.Text.Json.Serialization;

namespace ExemploJsonPolymorphism.Models;

[JsonDerivedType(typeof(Veiculo), typeDiscriminator: 0)]
[JsonDerivedType(typeof(Carro), typeDiscriminator: 1)]
[JsonPolymorphic(UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]
public class Veiculo
{
    public string? Codigo { get; set; }
    public string? Descricao { get; set; }
}

public class Carro : Veiculo
{
    public bool Hibrido { get; set; }
}

public class CarroCorrida : Carro
{
}