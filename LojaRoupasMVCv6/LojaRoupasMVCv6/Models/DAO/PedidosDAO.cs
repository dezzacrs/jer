using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LojaRoupasMVCv6.Models.POJO;
using MySql.Data.MySqlClient;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class PedidosDAO
    {



        /*public string selecionarEstoque(int idVenda)
        {

            string Retorno;
            try
            {

                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();

                Banco = objCon.AbreConexao();
                MySqlCommand command = Banco.CreateCommand();

                command.CommandText = "select * from pedidos pe, produtos pr where pe.idProduto = pr.idProduto and pe.sessao='" + sessao + "' and pe.status=0 and pe.idVendedor='" + vendedor + "'";

                MySqlDataReader Reader = command.ExecuteReader();

                Reader.Read();

                if (Reader.HasRows)
                {
                    Retorno = Reader.GetString(0).ToString();

                }
                else { Retorno = "Erro"; }


            }
            catch { Retorno = "0"; }

            return Retorno;


        }*/

        
        public String adicionarPedido(Pedidos p)
        {
            string Retorno = "0";
            try
            {
                string sessao = HttpContext.Current.Session.ToString();
                string data = DateTime.Now.ToString("dd/MM/yyyy HH:mm");




                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = new MySqlCommand("insert into pedidos (status,idProduto,qtdProduto,sessao,dataPedido,idVendedor) VALUES ('0','" + p.idProduto + "','" + p.qtdProduto + "','" + HttpContext.Current.Session.SessionID + "','" + data + "','" + p.idVendedor + "')", Banco);
                command.ExecuteNonQuery();
                Retorno = "1";

                objCon.FechaConexao();
                
            }
            catch (Exception e)
            {
                Retorno= e.StackTrace + e.Message + e.InnerException + e.Source;
            }
            return Retorno;
        }




        public String cancelarPedido(Pedidos p)
        {
            string Retorno;
            try
            {
                string sessao = HttpContext.Current.Session.SessionID.ToString();
                string vendedor = HttpContext.Current.Session["idVendedor"].ToString();
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "delete FROM pedidos WHERE idVendedor =" + vendedor + " and status ='0' and sessao ='" + sessao + "'";
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
            catch { Retorno = "Erro em deletar pedido"; }
            return Retorno;


        }







        public String deletarProduto(int id = 0)
        {
            string Retorno;
            try
            {
                
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "delete FROM pedidos WHERE idPedido =" + id + "";
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
            catch { Retorno = "Erro em deletar pedido"; }
            return Retorno;


        }



         /*public string adicionarPedido(Pedidos p)
         {

             string sessao = HttpContext.Current.Session.ToString();
             string data = DateTime.Now.ToString("dd/MM/yyyy hh:mm");




             Connection objCon = new Connection();

             MySqlConnection Banco = new MySqlConnection();

             Banco = objCon.AbreConexao();
             MySqlCommand command = new MySqlCommand("insert into pedidos (status,idProduto,qtdProduto,sessao,dataPedido,idVendedor) VALUES ('0','" + p.idProduto + "','" + p.qtdProduto + "','" + HttpContext.Current.Session.SessionID + "','" + data + "','" + p.idVendedor + "')", Banco);
             command.ExecuteNonQuery();


             return Retorno;


         }*/



         public List<Pedidos> MostrarPedidos()
         {
             List<Pedidos> Retorno = new List<Pedidos>();
             try
             {

                 string sessao = HttpContext.Current.Session.SessionID.ToString();
                 string vendedor = HttpContext.Current.Session["idVendedor"].ToString();
                 Connection objCon = new Connection();

                 MySqlConnection Banco = new MySqlConnection();
                 string user = "uvendedor";
                 Banco = objCon.AbreConexao(user);
                 MySqlCommand command = Banco.CreateCommand();
                 command.CommandText = "select idPedido, qtdProduto, idProduto FROM pedidos WHERE idVendedor ='" + vendedor + "' and sessao ='" + sessao + "' and status='0'";
                 //command.CommandText = "select qtdProduto, idProduto FROM pedidos WHERE sessao =" + HttpContext.Current.Session.SessionID + "";
                 MySqlDataReader Reader = command.ExecuteReader();
                 while (Reader.Read())
                 {
                     Pedidos objP = new Pedidos();

                     objP.idPedido = Reader.GetInt32(0);
                     objP.qtdProduto = Reader.GetInt32(1);
                     objP.idProduto = Reader.GetString(2);


                     //----Get the products data
                     ProdutosDAO objPrD = new ProdutosDAO();
                     Produtos objPr = new Produtos();
                     objPr = objPrD.pegarDados(Reader.GetString(2));


                     objP.produtos_descricaoProduto = objPr.descricaoProduto;
                     objP.produtos_precoProduto = objPr.precoProduto;
                     objP.produtos_tamanhoProduto = objPr.tamanhoProduto;

                     
                     Retorno.Add(objP);
          
                 }

                 objCon.FechaConexao();

             }
             catch (Exception e)
             {
                
             }
             return Retorno;
         }





         public string somarPedido()
         {
             string Retorno;
             try
             {

                 string sessao = HttpContext.Current.Session.SessionID.ToString();
                 string vendedor = HttpContext.Current.Session["idVendedor"].ToString();

                 Connection objCon = new Connection();

                 MySqlConnection Banco = new MySqlConnection();
                 string user = "uvendedor";
                 Banco = objCon.AbreConexao(user);
                 MySqlCommand command = Banco.CreateCommand();

                 command.CommandText = "select sum(pr.precoProduto*pe.qtdProduto) from pedidos pe, produtos pr where pe.idProduto = pr.idProduto and pe.sessao='"+ sessao +"' and pe.status=0 and pe.idVendedor='"+ vendedor +"'";

                MySqlDataReader Reader = command.ExecuteReader();

                Reader.Read();

                if (Reader.HasRows)
                {
                    Retorno = Reader.GetString(0).ToString();

                }
                else { Retorno = "Erro"; }

                objCon.FechaConexao();
                 }
             catch { Retorno = "0"; }

             return Retorno;


         }
     



         public String atualizaStatus(string idVenda)
         {
             string Retorno;
             try
             {
                 string sessao = HttpContext.Current.Session.SessionID.ToString();
                 string vendedor = HttpContext.Current.Session["idVendedor"].ToString();

                 Connection objCon = new Connection();

                 MySqlConnection Banco = new MySqlConnection();
                 string user = "uvendedor";
                 Banco = objCon.AbreConexao(user);
                 MySqlCommand command = Banco.CreateCommand();
                 command.CommandText = "UPDATE pedidos set status ='1', idVenda='" + idVenda + "' WHERE sessao ='" + sessao + "' AND idVendedor ='"+ vendedor +"' AND status='0'";
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
             catch { Retorno = "Erro em deletar pedido"; }
             return Retorno;


         }



         public List<Pedidos> GetPedidos(string id)
         {
             List<Pedidos> listapedidos = new List<Pedidos>();
             try
             {

                 Connection objCon = new Connection();

                 MySqlConnection Banco = new MySqlConnection();
                 string user = "uvendedor";
                 Banco = objCon.AbreConexao(user);
                 MySqlCommand command = Banco.CreateCommand();
                 command.CommandText = "SELECT idProduto, qtdProduto FROM pedidos WHERE idVenda=" + id + "";
                 MySqlDataReader Reader = command.ExecuteReader();
                 while (Reader.Read())
                 {
                     Pedidos p = new Pedidos();
                     p.idProduto = Reader.GetString(0);
                     p.qtdProduto = Reader.GetInt32(1);


                     //----Get the products data
                     ProdutosDAO objPrD = new ProdutosDAO();
                     Produtos objPr = new Produtos();
                     objPr = objPrD.pegarDados(Reader.GetString(0));


                     p.produtos_descricaoProduto = objPr.descricaoProduto;
                     p.produtos_precoProduto = objPr.precoProduto;
                     p.produtos_tamanhoProduto = objPr.tamanhoProduto;

                     listapedidos.Add(p);
                 }
                 command.Dispose();
                 objCon.FechaConexao();

             }
             catch { }
             return listapedidos;
         }




	}

    }
