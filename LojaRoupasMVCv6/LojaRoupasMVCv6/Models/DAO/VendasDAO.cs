using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LojaRoupasMVCv6.Models.POJO;
using MySql.Data.MySqlClient;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class VendasDAO
    {




        public List<Vendas> VendasDiariasAPrazo()
        {
            List<Vendas> listV = new List<Vendas>();

            string dataHoje = DateTime.Now.ToString("dd/MM/yyyy");
            string idVendedor = HttpContext.Current.Session["idVendedor"].ToString();

            try
            {


                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "SELECT idVenda, idCliente, totalVenda FROM vendas WHERE idVendedor=" + idVendedor + " and tipoVenda = 'p' and dataVenda LIKE '%" + dataHoje + "%'";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Vendas v = new Vendas();
                    v.idVenda = Reader.GetInt32(0);
                    v.idCliente = Reader.GetInt32(1);
                    v.totalVenda = Reader.GetDecimal(2);


                    ClientesDAO objCl = new ClientesDAO();
                    v.clientes_nomeCliente = objCl.selecionanomeCliente(Reader.GetInt32(1));
                    
                    listV.Add(v);
                }
                command.Dispose();
                objCon.FechaConexao();

            }
            catch { }
            return listV;
        }


        public List<Vendas> VendasDiariasAVista()
        {
            List<Vendas> listV = new List<Vendas>();

            string dataHoje = DateTime.Now.ToString("dd/MM/yyyy");
            string idVendedor = HttpContext.Current.Session["idVendedor"].ToString();

            try
            {


                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "SELECT idVenda, idCliente, totalVenda FROM vendas WHERE idVendedor=" + idVendedor + " and tipoVenda = 'v' and dataVenda LIKE '%" + dataHoje + "%'";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Vendas v = new Vendas();
                    v.idVenda = Reader.GetInt32(0);
                    v.idCliente = Reader.GetInt32(1);
                    v.totalVenda = Reader.GetDecimal(2);

                    listV.Add(v);
                }
                command.Dispose();
                objCon.FechaConexao();

            }
            catch { }
            return listV;
        }





        public List<Vendas> VendasDiarias()
        {
            List<Vendas> listV = new List<Vendas>();

            string dataHoje = DateTime.Now.ToString("dd/MM/yyyy");
            string idVendedor = HttpContext.Current.Session["idVendedor"].ToString();

            try
            {


                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "SELECT idVenda, idCliente, totalVenda, tipoVenda FROM vendas WHERE idVendedor="+idVendedor+" and dataVenda LIKE '%"+ dataHoje + "%'";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Vendas v = new Vendas();
                    v.idVenda = Reader.GetInt32(0);
                    v.idCliente = Reader.GetInt32(1);
                    v.totalVenda = Reader.GetDecimal(2);
                    v.tipoVenda = Reader.GetString(3);
                    listV.Add(v);
                }
                command.Dispose();
                objCon.FechaConexao();

            }
            catch { }
            return listV;
        }




        public decimal pegartotalVenda (string id)
        {
            decimal retorno = '0';

            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "Select totalVenda from vendas where idVenda = '" + id + "'";
            MySqlDataReader Reader = command.ExecuteReader();

            Reader.Read();

            retorno = Reader.GetDecimal(0);

            objCon.FechaConexao();
            return retorno;

        }



        public String vendaAvista(Vendas v)
        {

            

            string idVendedor = HttpContext.Current.Session["idVendedor"].ToString();
            string dataVenda = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            PedidosDAO objpe = new PedidosDAO();
            string totalVenda = objpe.somarPedido();


            string Retorno = "0";


            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = new MySqlCommand("insert into vendas (idVendedor,idCliente,totalVenda,dataVenda,tipoVenda) VALUES ('" + idVendedor + "','1',REPLACE( REPLACE( '" + totalVenda + "', '.' ,'' ), ',', '.' ),'" + dataVenda + "','v')", Banco);
            command.ExecuteNonQuery();


            MySqlCommand command2 = Banco.CreateCommand();
            command2.CommandText = "Select idVenda from vendas where idVendedor = '" + idVendedor + "' and dataVenda='" + dataVenda + "'";
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





        public String vendaAprazo(Vendas v)
        {

            string idVendedor = HttpContext.Current.Session["idVendedor"].ToString();
            string dataVenda = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            PedidosDAO objpe = new PedidosDAO();
            string totalVenda = objpe.somarPedido();
                
            string Retorno = "0";


            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = new MySqlCommand("insert into vendas (idVendedor,idCliente,totalVenda,dataVenda,tipoVenda) VALUES ('" + idVendedor + "','" + v.idCliente + "',REPLACE( REPLACE( '" + totalVenda + "', '.' ,'' ), ',', '.' ),'" + dataVenda + "','p')", Banco);
            command.ExecuteNonQuery();


            MySqlCommand command2 = Banco.CreateCommand();
            command2.CommandText = "Select idVenda from vendas where idVendedor = '" + idVendedor + "' and dataVenda='" + dataVenda + "'";
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




        public List<Vendas> listarVendasaprazo(string id)
        {
            List<Vendas> Retorno = new List<Vendas>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idVenda, idVendedor, dataVenda, totalVenda from vendas WHERE idCliente = "+ id+" ";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Vendas objV = new Vendas();
                    objV.idVenda = Reader.GetInt32(0);
                    objV.idVendedor = Reader.GetInt32(1);
                    objV.dataVenda = Reader.GetString(2);
                    objV.totalVenda = Reader.GetDecimal(3);

                    Retorno.Add(objV);
                }

                objCon.FechaConexao();

            }
            catch (Exception e)
            {
                Vendas objV = new Vendas();
                objV.idCliente = 0;
                objV.dataVenda = e.Message + e.InnerException + e.Source;
                Retorno.Add(objV);
            }


            return Retorno;
        }




        public List<Vendas> listaridvendas(string id)
        {
            List<Vendas> Retorno = new List<Vendas>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idVenda from vendas WHERE idCliente = " + id + " ";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Vendas objV = new Vendas();
                    objV.idVenda = Reader.GetInt32(0);

                    Retorno.Add(objV);
                }
                objCon.FechaConexao();

            }
            catch (Exception e)
            {
                Vendas objV = new Vendas();
                objV.idCliente = 0;
                objV.dataVenda = e.Message + e.InnerException + e.Source;
                Retorno.Add(objV);
            }
            return Retorno;
        }



        public Vendas GetVendas(string id)
        {
            Vendas v = new Vendas();
            try
            {

                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "SELECT idVendedor, idCliente, totalVenda, dataVenda FROM vendas WHERE idVenda=" + id + "";
                MySqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    v.idVendedor = Reader.GetInt32(0);
                    v.idCliente = Reader.GetInt32(1);
                    v.totalVenda = Reader.GetDecimal(2);
                    v.dataVenda = Reader.GetString(3);

                }
                command.Dispose();

                v.idVenda = Int32.Parse(id);
                objCon.FechaConexao();
            }
            catch { }
            return v;
        }



    }
}