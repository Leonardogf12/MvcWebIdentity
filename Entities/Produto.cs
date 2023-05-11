using System.ComponentModel.DataAnnotations;

namespace MvcWebIdentity.Entities;

public class Produto
{
    public int ProdutoId { get; set; }

    //[Required, MaxLength(80, ErrorMessage = "Nome não pode exceder 80 caracteres.")]
    public string? Nome { get; set; }

    public decimal Preco { get; set; }
}
