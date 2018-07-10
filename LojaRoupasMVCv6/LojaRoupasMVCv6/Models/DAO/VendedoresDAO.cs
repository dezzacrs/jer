using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using LojaRoupasMVCv6.Models.POJO;
using PagedList;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class VendedoresDAO
    {


        public List<Vendedores> vendedoresListar()
        {
            List<Vendedores> Retorno = new List<Vendedores>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idVendedor, nomeVendedor, telefoneVendedor from vendedores ORDER BY nomeVendedor ASC";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Vendedores objVen = new Vendedores();
                    objVen.idVendedor = Reader.GetInt32(0);
                    objVen.nomeVendedor = Reader.GetString(1);
                    objVen.telefoneVendedor = Reader.GetString(2);

                    Retorno.Add(objVen);
                }

                objCon.FechaConexao();
            }

            catch { }

            
            return Retorno;
        }




        public string md5(string senha)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(senha);
            byte[] hash = md5.ComputeHash(inputBytes);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }




        public List<Vendedores> BuscarVendedores(string buscar)
        {
            List<Vendedores> Retorno = new List<Vendedores>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idVendedor, nomeVendedor, telefoneVendedor from vendedores where nomeVendedor like '%" + buscar + "%'";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Vendedores objV = new Vendedores();
                    objV.idVendedor = Reader.GetInt32(0);
                    objV.nomeVendedor = Reader.GetString(1);
                    objV.telefoneVendedor = Reader.GetString(2);

                    Retorno.Add(objV);
                }
                objCon.FechaConexao();
            }

            catch { }

            return Retorno;
        }





        public string verificaVendedor(Vendedores v)
        {
            string retorno = "0";

            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "Select * from vendedores where idVendedor = '" + v.idVendedor + "' and senhaVendedor='" + md5(v.senhaVendedor) + "'";
            MySqlDataReader Reader = command.ExecuteReader();

            Reader.Read();

            if (Reader.HasRows)
            {
                retorno = "1";
            }
            else { retorno = "Login ou Senha Incorretos"; }

            objCon.FechaConexao();

            return retorno;
        }




        public string selecionanomeVendedor(Vendedores v)
        {
            string retorno = "0";

            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "Select nomeVendedor from vendedores where idVendedor = '" + v.idVendedor + "'";
            MySqlDataReader Reader = command.ExecuteReader();

            Reader.Read();
                        
                retorno = Reader.GetString(0);

                objCon.FechaConexao();

            return retorno;
        }







        //ADMINISTRADOR 

        public IPagedList<Vendedores> orderVendBuscar(string buscar, int pageIndex, int pageSize)
        {



            IPagedList<Vendedores> VendPage = null;
            List<Vendedores> objVend = new List<Vendedores>();
            objVend = BuscarVendedores(buscar);
            VendPage = objVend.ToPagedList(pageIndex, pageSize);

            return VendPage;
        }


        public IPagedList<Vendedores> orderVend(int pageIndex, int pageSize)
        {

            

            IPagedList<Vendedores> VendPage = null;
            List<Vendedores> objVend = new List<Vendedores>();
            objVend = listarVendedores();
            VendPage = objVend.ToPagedList(pageIndex, pageSize);

            return VendPage;
        }



        public List<Vendedores> listarVendedores()
        {
            List<Vendedores> Retorno = new List<Vendedores>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idVendedor, nomeVendedor, senhaVendedor, telefoneVendedor from vendedores ORDER BY nomeVendedor ASC";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Vendedores objVen = new Vendedores();
                    objVen.idVendedor = Reader.GetInt32(0);
                    objVen.nomeVendedor = Reader.GetString(1);
                    objVen.senhaVendedor = Reader.GetString(2);
                    objVen.telefoneVendedor = Reader.GetString(3);

                    Retorno.Add(objVen);
                }

                objCon.FechaConexao();
            }

            catch { }


            return Retorno;
        }



        public string cadastrarVendedor(Vendedores v)
        {
            string retorno = "0";

            string idAdmin = HttpContext.Current.Session["idAdmin"].ToString();

           Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uadmin";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "INSERT into vendedores (nomeVendedor, senhaVendedor, telefoneVendedor, idAdmin) VALUES('" + v.nomeVendedor + "', '" + md5(v.senhaVendedor) + "', '" + v.telefoneVendedor + "', '" + idAdmin + "')";
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


        public Vendedores GetVendedores(int id)
        {
            Vendedores v = new Vendedores();
            string idAdmin = HttpContext.Current.Session["idAdmin"].ToString();
            try
            {

                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "SELECT nomeVendedor, telefoneVendedor FROM vendedores WHERE idVendedor="+id+" and idAdmin ="+idAdmin+"";
                MySqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                   
                    v.nomeVendedor = Reader.GetString(0);
                    v.telefoneVendedor = Reader.GetString(1);
                }
                command.Dispose();

                v.idVendedor = id;

                objCon.FechaConexao();

            }
            catch { }
            return v;
        }


        public string VendedoresDelete(int id)
        {
            string retorno = "0";
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "DELETE from vendedores where idVendedor = " + id + "";
               int numRowsUpdated = command.ExecuteNonQuery();

                if(numRowsUpdated != 0)
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
                retorno = "Não foi possível deletar o vendedor desejada.";
            }
            

            return retorno;
        }



        public string VendedoresEdit(Vendedores v)
        {
            string retorno = "0";


            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "UPDATE vendedores set nomeVendedor = '" + v.nomeVendedor + "', telefoneVendedor='" + v.telefoneVendedor + "', senhaVendedor='" + md5(v.senhaVendedor) + "' where idVendedor=" + v.idVendedor + "";
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
            catch
            {

            }


            return retorno;
        }


    }
}