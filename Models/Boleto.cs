using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto_Final.Models
{
    public class Boleto
    {
        public int Id { get; set; }
        public string codigo { get; set; }
        public bool confirma()
        {
            Random randNum = new Random();
            return randNum.Next(0, 100) <= 80;
        }
    }
}