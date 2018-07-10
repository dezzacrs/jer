using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LojaRoupasMVCv6.Models.POJO;
using MySql.Data.MySqlClient;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class PagamentoDAO
    {



        public Pagamento GetPagamento(int idPagamento)
        {
            Pagamento p = new Pagamento();
                      
            try
            {

                Connection objCon = new Connection();
                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "SELECT idParcela, dataPagamento, idVendedor FROM pagamento WHERE idPagamento=" + idPagamento + "";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                   
                    p.idParcela = Reader.GetInt32(0);
                    p.dataPagamento = Reader.GetString(1);
                    p.idVendedor = Reader.GetInt32(2);
                }
                command.Dispose();

                p.idPagamento = idPagamento;

                objCon.FechaConexao();
            }
            catch { }
            return p;
        }




        public int realizaPagamento(int idParcela)
        {
            int retorno = 0;

            string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            string vendedor = HttpContext.Current.Session["idVendedor"].ToString();

            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "INSERT into pagamento (idParcela, dataPagamento, idVendedor) VALUES('" + idParcela + "', '" + data + "', '" + vendedor + "')";
            int numRowsUpdated = command.ExecuteNonQuery();


            if (numRowsUpdated != 0)
            {
                MySqlCommand command2 = Banco.CreateCommand();
                command2.CommandText = "Select idPagamento from pagamento where dataPagamento = '" + data + "'";
                MySqlDataReader Reader = command2.ExecuteReader();

                Reader.Read();

               retorno = Reader.GetInt32(0);

               
            }
            else
            {
                retorno = 0;
            }
            objCon.FechaConexao();

            return retorno;
        }


    }
}