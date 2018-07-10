using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace LojaRoupasMVCv6.Models.POJO
{
    public class Vendedores
    {

        [Required(ErrorMessage = "Por favor digite o seu código", AllowEmptyStrings = false)]
        public int idVendedor { get; set; }
        public string nomeVendedor { get; set; }
        [Required(ErrorMessage = "Por favor digite sua senha", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string senhaVendedor { get; set; }
        public string telefoneVendedor { get; set; }
    }
}