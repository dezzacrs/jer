using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using LojaRoupasMVCv6.Models.POJO;
using PagedList;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class ProdutosDAO
    {


        public string atualizaEstoque (int qtd, string id)
        {
            string retorno = "0";


            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "UPDATE produtos set qtdestoqueProduto = qtdestoqueProduto -" + qtd + " where idProduto ='" + id + "'";
                int numRowsUpdated = command.ExecuteNonQuery();

                if (numRowsUpdated != 0)
                {
                    retorno = "Alterado com sucesso";
                }
                else
                {
                    retorno = "Erro";
                }
                objCon.FechaConexao();
            }
            catch (Exception e)
            {
                retorno = e.InnerException + e.Message + e.Source + e.StackTrace;
            }


            return retorno;
        }




        public string verificaProduto(Produtos p)
        {
            string retorno = "0";

            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "Select * from produtos where idProduto = '" + p.idProduto + "'";
            MySqlDataReader Reader = command.ExecuteReader();

            Reader.Read();

            if (Reader.HasRows)
            {
                retorno = "1";
            }
            else { retorno = "Produto inexistente"; }


            objCon.FechaConexao();
            return retorno;
        }



         public List<Produtos> MostrarProdutos()
        {
            List<Produtos> Retorno = new List<Produtos>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idProduto, descricaoProduto, idCategoria, precoProduto, marcaProduto, qtdestoqueProduto, tamanhoProduto from produtos";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Produtos objP = new Produtos();
                    objP.idProduto = Reader.GetString(0);
                    objP.descricaoProduto = Reader.GetString(1);
                    objP.idCategoria = Reader.GetInt32(2);
                    objP.precoProduto = Reader.GetDecimal(3);
                    objP.marcaProduto = Reader.GetString(4);
                    objP.qtdestoqueProduto = Reader.GetInt32(5);
                    objP.tamanhoProduto = Reader.GetString(6);
                    Retorno.Add(objP);
                }

                objCon.FechaConexao();

            }
            catch (Exception e)
            {
                Produtos objP = new Produtos();
                objP.idProduto = "0";
                objP.descricaoProduto = e.Message + e.InnerException + e.Source;
                Retorno.Add(objP);
            }
            return Retorno;
        }

    


        public List<Produtos> BuscarProduto(string buscar)
        {
            List<Produtos> Retorno = new List<Produtos>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idProduto, descricaoProduto, precoProduto, marcaProduto, qtdestoqueProduto, tamanhoProduto, marcaProduto, idCategoria from produtos where descricaoProduto like '%" + buscar + "%'";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Produtos objP = new Produtos();
                    objP.idProduto = Reader.GetString(0);
                    objP.descricaoProduto = Reader.GetString(1);
                    objP.precoProduto = Reader.GetDecimal(2);
                    objP.marcaProduto = Reader.GetString(3);
                    objP.qtdestoqueProduto = Reader.GetInt32(4);
                    objP.tamanhoProduto = Reader.GetString(5);
                    objP.marcaProduto = Reader.GetString(6);
                    objP.idCategoria = Reader.GetInt32(7);

                    Retorno.Add(objP);
                }

                objCon.FechaConexao();
            }

            catch { }

            return Retorno;
        }


        public Produtos pegarDados(string idProduto)
        {
            Produtos Retorno = new Produtos();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select descricaoProduto, precoProduto, tamanhoProduto from produtos where idProduto = '" + idProduto + "'";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    
                   
                    Retorno.descricaoProduto = Reader.GetString(0);
                    Retorno.precoProduto = Reader.GetDecimal(1);
                    Retorno.tamanhoProduto = Reader.GetString(2);

                    
                }



                objCon.FechaConexao();
            }

            catch {  }

            return Retorno;
        }








        //ADMINISTRADOR 

        public List<Produtos> listarProdutos()
        {
            List<Produtos> Retorno = new List<Produtos>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idProduto, descricaoProduto, idCategoria, precoProduto, marcaProduto, qtdestoqueProduto, tamanhoProduto from produtos ORDER BY descricaoProduto ASC";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Produtos objPro = new Produtos();
                    objPro.idProduto = Reader.GetString(0);
                    objPro.descricaoProduto = Reader.GetString(1);
                    objPro.idCategoria = Reader.GetInt32(2);
                    objPro.precoProduto = Reader.GetDecimal(3);
                    objPro.marcaProduto = Reader.GetString(4);
                    objPro.qtdestoqueProduto = Reader.GetInt32(5);
                    objPro.tamanhoProduto = Reader.GetString(6);

                    Retorno.Add(objPro);
                }

                objCon.FechaConexao();
            }

            catch { }

            return Retorno;
        }



        public string cadastrarProduto(Produtos p)
        {
            string retorno = "0";

            string idAdmin = HttpContext.Current.Session["idAdmin"].ToString();

            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uadmin";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "INSERT into produtos (idProduto, descricaoProduto, idCategoria, precoProduto, marcaProduto, qtdestoqueProduto, tamanhoProduto, idAdmin) VALUES('" + p.idProduto + "', '" + p.descricaoProduto + "', '" + p.idCategoria + "', REPLACE( REPLACE( '" + p.precoProduto + "', '.' ,'' ), ',', '.' ), '" + p.marcaProduto + "', '" + p.qtdestoqueProduto + "', '" + p.tamanhoProduto + "', '" + idAdmin + "')";
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


        public Produtos GetProdutos(string id)
        {
            Produtos p = new Produtos();
            string idAdmin = HttpContext.Current.Session["idAdmin"].ToString();
            try
            {

                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "SELECT descricaoProduto, idCategoria, precoProduto, marcaProduto, qtdestoqueProduto, tamanhoProduto FROM produtos WHERE idProduto=" + id + " and idAdmin =" + idAdmin + "";
                MySqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    p.descricaoProduto = Reader.GetString(0);                   
                    p.idCategoria = Reader.GetInt32(1);
                    p.precoProduto = Reader.GetDecimal(2);
                    p.marcaProduto = Reader.GetString(3);
                    p.qtdestoqueProduto = Reader.GetInt32(4);
                    p.tamanhoProduto = Reader.GetString(5);


                }
                command.Dispose();

                p.idProduto = id;
                objCon.FechaConexao();

            }
            catch { }
            return p;
        }



        public string ProdutosDelete(string id)
        {
            string retorno = "0";
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "DELETE from produtos where idProduto = '" + id + "'";
                int numRowsUpdated = command.ExecuteNonQuery();

                if (numRowsUpdated != 0)
                {
                    retorno = "Deletado com sucesso";
                }
                else
                {
                    retorno = "Erro";
                }
                objCon.FechaConexao();
            }
            catch
            {
                retorno = "Não foi possível deletar o produto desejado.";
            }


            return retorno;
        }



        public string ProdutosEdit(Produtos p)
        {
            string retorno = "0";


            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "UPDATE produtos set descricaoProduto = '" + p.descricaoProduto + "', idCategoria = '" + p.idCategoria + "', precoProduto=REPLACE( '" + p.precoProduto + "', ',' ,'.' ), marcaProduto='" + p.marcaProduto + "', qtdestoqueProduto = '" + p.qtdestoqueProduto + "', tamanhoProduto = '" + p.tamanhoProduto + "' where idProduto ='" + p.idProduto + "'";
                int numRowsUpdated = command.ExecuteNonQuery();

                if (numRowsUpdated != 0)
                {
                    retorno = "Alterado com sucesso";
                }
                else
                {
                    retorno = "Erro";
                }

                objCon.FechaConexao();


            }
            catch(Exception e)
            {
                retorno = e.InnerException + e.Message + e.Source + e.StackTrace;
            }


            return retorno;
        }



        public IPagedList<Produtos> orderProdBuscar(string buscar, int pageIndex, int pageSize)
        {



            IPagedList<Produtos> ProdPage = null;
            List<Produtos> objProd = new List<Produtos>();
            objProd = BuscarProduto(buscar);
            ProdPage = objProd.ToPagedList(pageIndex, pageSize);

            return ProdPage;
        }


        public IPagedList<Produtos> orderProd(int pageIndex, int pageSize)
        {



            IPagedList<Produtos> ProdPage = null;
            List<Produtos> objProd = new List<Produtos>();
            objProd = listarProdutos();
            ProdPage = objProd.ToPagedList(pageIndex, pageSize);

            return ProdPage;
        }



    }
}