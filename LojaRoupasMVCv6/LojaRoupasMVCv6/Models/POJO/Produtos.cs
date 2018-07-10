using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaRoupasMVCv6.Models.POJO
{
    public class Produtos
    {
        public string idProduto { get; set; }
        public string descricaoProduto { get; set; }
        public int idCategoria { get; set; }
        public decimal precoProduto { get; set; }
        public string marcaProduto { get; set; }
        public int qtdestoqueProduto { get; set; }
        public string tamanhoProduto { get; set; }
        public int codigoAdmin { get; set; }
        
    }
}