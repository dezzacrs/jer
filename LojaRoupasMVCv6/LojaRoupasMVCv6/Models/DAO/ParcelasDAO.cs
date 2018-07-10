using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using LojaRoupasMVCv6.Models.POJO;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class ParcelasDAO
    {



        public List<Parcelas> Vencimentos()
        {
            List<Parcelas> listP = new List<Parcelas>();

            string dataHoje = DateTime.Now.ToString("dd/MM/yyyy");
            string idVendedor = HttpContext.Current.Session["idVendedor"].ToString();

            try
            {


                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "SELECT idParcela, dtvencParcela, valorParcela FROM parcelas WHERE statusParcela = 0 and dtvencParcela LIKE '%" + dataHoje + "%'";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Parcelas p = new Parcelas();
                    p.idParcela = Reader.GetInt32(0);
                    p.dtvencParcela = Reader.GetString(1);
                    p.valorParcela = Reader.GetDecimal(2);

                    listP.Add(p);
                }
                command.Dispose();
                objCon.FechaConexao();

            }
            catch { }
            return listP;
        }




        public int selecionaidVendaaPrazo(int idParcela)
        {
            int Retorno = 0;
            try
            {
                
                Connection objCon = new Connection();
                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "SELECT idVendaaPrazo FROM parcelas WHERE idParcela=" + idParcela + "";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {

                    Retorno = Reader.GetInt32(0);
                }
                command.Dispose();
                objCon.FechaConexao();
            }
            catch { }
            return Retorno;
        }




        public string criarParcela(Parcelas p)
        {
            {
                string retorno = "0";


                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "INSERT into parcelas (numParcela, dtvencParcela, statusParcela, idVendaaPrazo, valorParcela) VALUES('" + p.numParcela + "', '" + p.dtvencParcela + "', '" + p.statusParcela + "', '" + p.idVendaaPrazo + "', REPLACE( '" + p.valorParcela + "', ',' ,'.' ) )";
                int numRowsUpdated = command.ExecuteNonQuery();

                if (numRowsUpdated != 0)
                {
                    retorno = "1";
                }
                else
                {
                    retorno = "0";
                }
                objCon.FechaConexao();
                return retorno;
            }
        }




            public List<Parcelas> listarParcelas(string id)
        {
            List<Parcelas> Retorno = new List<Parcelas>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idParcela, numParcela, dtvencParcela, valorParcela from parcelas WHERE idVendaaPrazo = "+ id+" and statusParcela = 0";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Parcelas objP = new Parcelas();
                    objP.idParcela = Reader.GetInt32(0);
                    objP.numParcela = Reader.GetInt32(1);
                    objP.dtvencParcela = Reader.GetString(2);
                    objP.valorParcela = Reader.GetDecimal(3);

                    Retorno.Add(objP);
                }

                objCon.FechaConexao();
            }
            catch (Exception e)
            {
                Parcelas objP = new Parcelas();
                objP.idParcela = 0;
                objP.dtvencParcela = e.Message + e.InnerException + e.Source;
                Retorno.Add(objP);
            }
            return Retorno;
        }



            public String pagarParcela(Parcelas p)
            {
                string Retorno;
                try
                {
                    
                    Connection objCon = new Connection();

                    MySqlConnection Banco = new MySqlConnection();
                    string user = "uvendedor";
                    Banco = objCon.AbreConexao(user);
                    MySqlCommand command = Banco.CreateCommand();
                    command.CommandText = "UPDATE parcelas set statusParcela ='1', idPagamento='" + p.idPagamento + "' WHERE idParcela ='" + p.idParcela + "'";
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
                catch { Retorno = "Erro em realizar pagamento"; }
                return Retorno;


            }


            public Parcelas GetParcelas(int id)
            {
                Parcelas p = new Parcelas();
                try
                {

                    Connection objCon = new Connection();

                    MySqlConnection Banco = new MySqlConnection();
                    string user = "uvendedor";
                    Banco = objCon.AbreConexao(user);
                    MySqlCommand command = Banco.CreateCommand();
                    command.CommandText = "SELECT numParcela, valorParcela, dtvencParcela FROM parcelas WHERE idPagamento=" + id + "";
                    MySqlDataReader Reader = command.ExecuteReader();
                    while (Reader.Read())
                    {
                        
                        p.numParcela = Reader.GetInt32(0);
                        p.valorParcela = Reader.GetDecimal(1);
                        p.dtvencParcela = Reader.GetString(2);
                        

                    }
                    command.Dispose();
                    objCon.FechaConexao();

                }
                catch { }
                return p;
            }


        }
    }
