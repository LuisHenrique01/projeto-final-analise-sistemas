using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Final.Models
{
    public class Consumidor : Usuario
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Nome não pode ter mais que 50 caracteres.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Nome { get; set; }
        public Carrinho? Carrinho { get; set; }
    }
}