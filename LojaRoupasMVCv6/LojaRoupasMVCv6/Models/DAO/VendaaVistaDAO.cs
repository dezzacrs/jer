using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class VendaaVistaDAO
    {


        public String vendaavista(string idVenda, string formapagVenda)
        {

            string Retorno = "0";


            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);

            MySqlCommand command = new MySqlCommand("insert into vendaavista (formapagVenda, idVenda) VALUES ('" + formapagVenda + "','"+ idVenda +"')", Banco);
            command.ExecuteNonQuery();


            MySqlCommand command2 = Banco.CreateCommand();
            command2.CommandText = "Select idVendaavista from vendaavista where idVenda = '" + idVenda + "'";
            MySqlDataReader Reader = command2.ExecuteReader();

            Reader.Read();

            if (Reader.HasRows)
            {
                Retorno = Reader.GetInt32(0).ToString();

            }
            else { Retorno = "Erro"; }


            objCon.FechaConexao();

            return Retorno;



        }


    }
}