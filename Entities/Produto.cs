using System.ComponentModel.DataAnnotations;

namespace MvcWebIdentity.Entities
{
    public class Produto
    {
        public int PRODUTOID { get; set; }

        [Required, MaxLength(80, ErrorMessage = "Nome não pode exceder 80 caracteres.")]
        public string? NOME { get; set; }

        public decimal PRECO { get; set; }
    }
}
