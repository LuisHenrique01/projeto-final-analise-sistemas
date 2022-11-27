using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Final.Models
{
    public class CartaoCredito
    {
        public int Id { get; set; }
        public string numero { get; set; }
        public bool autorizaDebito()
        {
            Random randNum = new Random();
            return randNum.Next(0, 100) <= 80;
        }
    }
}