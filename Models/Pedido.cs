using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Final.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public virtual Consumidor Consumidor { get; set; }
        public virtual ICollection<Produto> Produtos { get; set; }
        public double getValorPedidos()
        {
            double total = 0;
            foreach (var item in Produtos)
            {
                total = total + item.ValorProduto;
            }
            return total;
        }
        public void incluiProduto(Produto prod)
        {
            Produtos.Add(prod);
        }
        public void excluiProduto(Produto prod)
        {
            Produtos.Remove(prod);
        }
    }
}