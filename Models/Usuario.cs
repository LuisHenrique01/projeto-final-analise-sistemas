using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Final.Models
{
    public class Usuario
    {
        [RegularExpression(@"/^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i")]
        public string Email { get; set; }
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Tamanho mínimo da senha é 6 dígitos e máximo 20.")]
        public string Senha { get; set; }
    }
}