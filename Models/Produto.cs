using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Final.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [StringLength(50, ErrorMessage = "Nome não pode ter mais que 50 caracteres.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Nome { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Valor do produto")]
        public double ValorProduto { get; set; }
    }
}