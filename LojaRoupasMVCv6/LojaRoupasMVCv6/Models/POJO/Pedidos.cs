using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaRoupasMVCv6.Models.POJO
{
    public class Pedidos
    {
        public int idPedido { get; set; }
        public string status { get; set; }
        public string idProduto { get; set; }
        public int qtdProduto { get; set; }
        public string sessao { get; set; }
        public string dataPedido { get; set; }
        public int idVendedor { get; set; }

        public string produtos_descricaoProduto { get; set; }
        public string produtos_tamanhoProduto { get; set; }
        public decimal produtos_precoProduto { get; set; }
    }
}