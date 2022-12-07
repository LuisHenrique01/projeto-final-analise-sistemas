using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Final.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public virtual Pedido Pedido { get; set; }
        private double _valor;
        [DisplayFormat(DataFormatString = "{0:0,0.00}", ApplyFormatInEditMode = true)]
        public double Valor
        {
            get => _valor;
            set
            {
                _valor = value;
            }
        }
        public virtual CartaoCredito? CartaoCredito { get; set; }
        public virtual Boleto? Boleto { get; set; }
        public bool confirmar()
        {
            if (CartaoCredito == null)
            {
                return Boleto.confirma();
            }
            return CartaoCredito.autorizaDebito();
        }
    }
}