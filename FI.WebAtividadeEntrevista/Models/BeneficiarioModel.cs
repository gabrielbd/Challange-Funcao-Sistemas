

using System.ComponentModel.DataAnnotations;
using WebAtividadeEntrevista.Utils;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }
        public long IdCliente { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [CPF(ErrorMessage = "CPF inválido")]
        public string CPF { get; set; }
    }
}