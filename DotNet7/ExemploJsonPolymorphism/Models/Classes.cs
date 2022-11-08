using System.Text.Json.Serialization;

namespace ExemploJsonPolymorphism.Models;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "$class")]
[JsonDerivedType(typeof(Pessoa), typeDiscriminator: "baseclassPessoa")]
[JsonDerivedType(typeof(Aluno), typeDiscriminator: "aluno")]
public class Pessoa
{
    public string? CPF { get; set; }
    public string? Nome { get; set; }
}

public class Aluno : Pessoa
{
    public string? Curso { get; set; }
}