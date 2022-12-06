using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Final.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public int ConsumidorId { get; set; }
        public Consumidor Consumidor { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}