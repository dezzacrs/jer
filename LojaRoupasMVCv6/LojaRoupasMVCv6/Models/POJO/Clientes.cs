using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaRoupasMVCv6.Models.POJO
{
    public class Clientes
    {
        public int idCliente { get; set; }
        public string nomeCliente { get; set; }
        public string rgCliente { get; set; }
        public string cpfCliente { get; set; }
        public string telefoneCliente { get; set; }
        public string enderecoCliente { get; set; }
        public string dtnascCliente { get; set; }
        public int codigoAdmin { get; set; }

    }
}