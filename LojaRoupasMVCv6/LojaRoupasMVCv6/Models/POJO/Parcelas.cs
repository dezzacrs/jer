using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaRoupasMVCv6.Models.POJO
{
    public class Parcelas
    {

        public int idParcela { get; set; }
        public int idPagamento { get; set; }
        public int numParcela { get; set; }
        public string dtvencParcela { get; set; }
        public int statusParcela { get; set; }
        public int idVendaaPrazo { get; set; }
        public decimal valorParcela { get; set; }

    }
}