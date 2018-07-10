using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaRoupasMVCv6.Models.POJO
{
    public class Pagamento 
    {
        //public int idCliente { get; set; }
        public int idParcela { get; set; }
        public int idPagamento { get; set; }
        public string dataPagamento { get; set; }
        public int idVendedor { get; set; }

    }
}