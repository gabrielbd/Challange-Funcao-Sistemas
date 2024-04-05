using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebAtividadeEntrevista.Utils;

namespace WebAtividadeEntrevista.Models
{
    public class ClienteModel
    {
        public long Id { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public string Cidade { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        [Required]
        [MaxLength(2)]
        public string Estado { get; set; }

        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Nacionalidade { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        public string Telefone { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [CPF(ErrorMessage = "CPF inválido")]
        public string CPF { get; set; }
    }
}