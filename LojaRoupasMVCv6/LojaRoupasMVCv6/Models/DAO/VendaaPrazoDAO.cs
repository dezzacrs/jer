using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using LojaRoupasMVCv6.Models.POJO;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class VendaaPrazoDAO
    {



        public int verificarParcelas(string id)
        {
            int Retorno = 0;
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select parcelas from vendaaprazo WHERE idVendaaPrazo = " + id + " ";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    
                    if(Reader.GetInt32(0) == 0)
                    {
                        Retorno = 1;
                    }
                    else
                    {
                        Retorno = 0;
                    }

                }
                objCon.FechaConexao();
            }
            catch (Exception e)
            {
              
            }

            return Retorno;
        }



        public String atualizaVendaaprazo(int id)
        {
            string Retorno;
            try
            {

                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "UPDATE vendaaprazo set parcelas = parcelas -1 WHERE idVendaaPrazo ='" + id + "'";
                //command.CommandText = "UPDATE pedidos set status ='1' AND idVenda='" + idVenda + "' WHERE idVendedor ='" + vendedor + "' and status='0' and sessao ='" + sessao + "'";
                int numRowsUpdated = command.ExecuteNonQuery();



                if (numRowsUpdated == 0)
                {
                    Retorno = "0";
                }
                else
                {
                    Retorno = "1";
                }

                objCon.FechaConexao();
            }
            catch { Retorno = "Erro em atualizar vendaaprazo"; }
            return Retorno;


        }


        public string pegaridVendaaprazo(string idVenda)
        {
            string retorno = "0";

            Connection objCon = new Connection();
            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);

            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "SELECT idVendaaPrazo FROM vendaaprazo WHERE idVenda=" + idVenda + "";
            MySqlDataReader Reader = command.ExecuteReader();
            if (Reader.Read())
            {
                retorno = Reader.GetString(0);


            }

            command.Dispose();
            objCon.FechaConexao();
            return retorno;
        }





        public string pegaridVendaaprazo2(int idVenda)
        {
            string retorno = "0";

            Connection objCon = new Connection();
            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);

            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "SELECT idVendaaPrazo FROM vendaaprazo WHERE idVenda=" + idVenda + "";
            MySqlDataReader Reader = command.ExecuteReader();
            if (Reader.Read())
            {
                retorno = Reader.GetString(0);


            }
            command.Dispose();
            objCon.FechaConexao();
            return retorno;
        }




        public String vendaaprazo(string idVenda, int parcelas)
        {
           
            string Retorno = "0";


            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = new MySqlCommand("insert into vendaaprazo (idVenda, parcelas) VALUES ('" + idVenda + "', "+ parcelas +")", Banco);
            command.ExecuteNonQuery();


            MySqlCommand command2 = Banco.CreateCommand();
            command2.CommandText = "Select idVendaaprazo from vendaaprazo where idVenda = '" + idVenda+ "'";
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