using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using LojaRoupasMVCv6.Models.POJO;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class CategoriasDAO
    {






        //ADMINISTRADOR 

        public List<Categorias> listarCategorias()
        {
            List<Categorias> Retorno = new List<Categorias>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();

                string user = "uadmin";
                
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idCategoria, nomeCategoria from categorias ORDER BY nomeCategoria ASC";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Categorias objCat = new Categorias();
                    objCat.idCategoria = Reader.GetInt32(0);
                    objCat.nomeCategoria = Reader.GetString(1);

                    Retorno.Add(objCat);
                }

                objCon.FechaConexao();
            }

            catch { }

            return Retorno;
        }



        public string cadastrarCategoria(Categorias c)
        {
            string retorno = "0";

            string idAdmin = HttpContext.Current.Session["idAdmin"].ToString();

            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();

            string user = "uadmin";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "INSERT into categorias (nomeCategoria, idAdmin) VALUES('" + c.nomeCategoria + "', '" + idAdmin + "')";
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


        public Categorias GetCategorias(int id)
        {
            Categorias c = new Categorias();
            string idAdmin = HttpContext.Current.Session["idAdmin"].ToString();
            try
            {

                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();

                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "SELECT nomeCategoria FROM categorias WHERE idCategoria=" + id + " and idAdmin =" + idAdmin + "";
                MySqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    c.nomeCategoria = Reader.GetString(0);

                }
                command.Dispose();

                c.idCategoria = id;
                objCon.FechaConexao();
            }
            catch { }
            return c;
        }


        public string CategoriasDelete(int id)
        {
            string retorno = "0";
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();

                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "DELETE from categorias where idCategoria = " + id + "";
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
                retorno = "Não foi possível deletar a categoria desejada.";
            }


            return retorno;
        }



        public string CategoriasEdit(Categorias c)
        {
            string retorno = "0";


            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();

                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "UPDATE categorias set nomeCategoria = '" + c.nomeCategoria + "' where idCategoria ='" + c.idCategoria + "'";
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






    }
}