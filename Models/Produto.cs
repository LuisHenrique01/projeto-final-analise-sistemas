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
        public string Nome { get; set; }
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Valor do produto")]
        [DisplayFormat(DataFormatString = "{0:0,0.00}", ApplyFormatInEditMode = true)]
        public double ValorProduto { get; set; }
    }
}