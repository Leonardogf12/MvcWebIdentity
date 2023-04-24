using System.ComponentModel.DataAnnotations;

namespace MvcWebIdentity.Entities
{
    public class Aluno
    {
        public int ALUNOID { get; set; }

        [Required,MaxLength(80, ErrorMessage ="Nome não pode exceder o limite de 80 caracteres.")]
        public string? ALU_NOME { get; set; }

        [EmailAddress]
        [Required, MaxLength(120)]
        public string? ALU_EMAIL { get; set; }

        public int ALU_IDADE { get; set; }
        
        [MaxLength(80)]
        public string? ALU_CURSO { get; set; }
    }
}
