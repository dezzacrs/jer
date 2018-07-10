using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaRoupasMVCv6.Models.POJO
{
    public class Vendas
    {
        public int idVenda { get; set; }
        public int idVendedor { get; set; }
        public int idCliente { get; set; }
        public decimal totalVenda { get; set; }
        public string dataVenda { get; set; }
        public string tipoVenda { get; set; }        
        public string clientes_nomeCliente { get; set; }
    }
}