using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Final.Models
{
    public class Usuario
    {
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Digite um e-mail válido.")]
        public string Email { get; set; }
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Tamanho mínimo da senha é 6 dígitos e máximo 20.")]
        public string Senha { get; set; }
        //public bool IsAdmin { get; set; } = false;
    }
}