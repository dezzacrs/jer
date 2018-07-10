using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using LojaRoupasMVCv6.Models.POJO;
using PagedList;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class ClientesDAO
    {

        public string selecionanomeCliente(int idCliente)
        {
            string retorno = "0";

            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "Select nomeCliente from clientes where idCliente = '" + idCliente + "'";
            MySqlDataReader Reader = command.ExecuteReader();


            Reader.Read();

            retorno = Reader.GetString(0);



            objCon.FechaConexao();

            return retorno;
        }



        public List<Clientes> BuscarCliente(string buscar)
        {
            List<Clientes> Retorno = new List<Clientes>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idCliente, nomeCliente, rgCliente, cpfCliente, telefoneCliente, enderecoCliente, dtnascCliente from clientes where nomeCliente like '%" + buscar + "%' and idCliente > 1";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Clientes objC = new Clientes();
                    objC.idCliente = Reader.GetInt32(0);
                    objC.nomeCliente = Reader.GetString(1);
                    objC.rgCliente = Reader.GetString(2);
                    objC.cpfCliente = Reader.GetString(3);
                    objC.telefoneCliente = Reader.GetString(4);
                    objC.enderecoCliente = Reader.GetString(5);
                    objC.dtnascCliente = Reader.GetString(6);
                    
                    Retorno.Add(objC);
                }

                objCon.FechaConexao();
            }

            catch { }

            return Retorno;
        }






        public List<Clientes> MostrarCliente()
        {
            List<Clientes> Retorno = new List<Clientes>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idCliente, nomeCliente, rgCliente, cpfCliente, telefoneCliente, enderecoCliente, dtnascCliente from clientes WHERE idCliente > 1";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Clientes objC = new Clientes();
                    objC.idCliente = Reader.GetInt32(0);
                    objC.nomeCliente = Reader.GetString(1);
                    objC.rgCliente = Reader.GetString(2);
                    objC.cpfCliente = Reader.GetString(3);
                    objC.telefoneCliente = Reader.GetString(4);
                    objC.enderecoCliente = Reader.GetString(5);
                    objC.dtnascCliente = Reader.GetString(6);
                    Retorno.Add(objC);
                }

                objCon.FechaConexao();
            }
            catch (Exception e)
            {
                Clientes objC = new Clientes();
                objC.idCliente = 0;
                objC.nomeCliente = e.Message + e.InnerException + e.Source;
                Retorno.Add(objC);
            }
            return Retorno;
        }


        public string selecionanomeCliente(string idCliente)
        {
            string retorno = "0";

            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uvendedor";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "Select nomeCliente from clientes where idCliente = " + idCliente + "";

            MySqlDataReader Reader = command.ExecuteReader();
            if (Reader.Read())
            {
                
                retorno = Reader.GetString(0);

            }
            command.Dispose();

            objCon.FechaConexao();

            return retorno;
        }





        //ADMINISTRADOR 

        public List<Clientes> listarClientes()
        {
            List<Clientes> Retorno = new List<Clientes>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idCliente, nomeCliente, rgCliente, cpfCliente, telefoneCliente, enderecoCliente, dtnascCliente from clientes ORDER BY nomeCliente ASC";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Clientes objCli = new Clientes();
                    objCli.idCliente = Reader.GetInt32(0);
                    objCli.nomeCliente = Reader.GetString(1);
                    objCli.rgCliente = Reader.GetString(2);
                    objCli.cpfCliente = Reader.GetString(3);
                    objCli.telefoneCliente = Reader.GetString(4);
                    objCli.enderecoCliente = Reader.GetString(5);
                    objCli.dtnascCliente = Reader.GetString(6);

                    Retorno.Add(objCli);
                }

                objCon.FechaConexao();
            }

            catch { }

            return Retorno;
        }



        public string cadastrarCliente(Clientes c)
        {

            try
            {

            string retorno = "0";
            string datateste = c.dtnascCliente;
            string idAdmin = HttpContext.Current.Session["idAdmin"].ToString();

            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uadmin";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "INSERT into clientes (nomeCliente, rgCliente, cpfCliente, telefoneCliente, enderecoCliente, dtnascCliente, idAdmin) VALUES('" + c.nomeCliente + "', '" + c.rgCliente + "', '" + c.cpfCliente + "', '" + c.telefoneCliente + "', '" + c.enderecoCliente + "', '" + c.dtnascCliente + "', '" + idAdmin + "')";
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
            catch
            {
                string retorno = "0";
                return retorno;
            }
        }


        public Clientes GetClientes(int id)
        {
            Clientes c = new Clientes();
            string idAdmin = HttpContext.Current.Session["idAdmin"].ToString();
            try
            {

                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "SELECT nomeCliente, rgCliente, cpfCliente, telefoneCliente, enderecoCliente, dtnascCliente FROM clientes WHERE idCliente=" + id + " and idAdmin =" + idAdmin + "";
                MySqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    c.nomeCliente = Reader.GetString(0);
                    c.rgCliente = Reader.GetString(1);
                    c.cpfCliente = Reader.GetString(2);
                    c.telefoneCliente = Reader.GetString(3);
                    c.enderecoCliente = Reader.GetString(4);
                    c.dtnascCliente = Reader.GetString(5);

                  
                }
                command.Dispose();

                c.idCliente = id;
                objCon.FechaConexao();
            }
            catch { }
            return c;
        }


        public string ClientesDelete(int id)
        {
            string retorno = "0";
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "DELETE from clientes where idCliente = " + id + "";
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
                retorno = "Não foi possível deletar o cliente desejado.";
            }


            return retorno;
        }



        public string ClientesEdit(Clientes c)
        {
            string retorno = "0";


            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uadmin";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "UPDATE clientes set nomeCliente = '" + c.nomeCliente + "', rgCliente = '" + c.rgCliente + "', cpfCliente = '" + c.cpfCliente + "', telefoneCliente='" + c.telefoneCliente + "', enderecoCliente='" + c.enderecoCliente + "', dtnascCliente = '" + c.dtnascCliente + "' where idCliente =" + c.idCliente + "";
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




        public IPagedList<Clientes> orderCliBuscar(string buscar, int pageIndex, int pageSize)
        {



            IPagedList<Clientes> CliPage = null;
            List<Clientes> objCli = new List<Clientes>();
            objCli = BuscarCliente(buscar);
            CliPage = objCli.ToPagedList(pageIndex, pageSize);

            return CliPage;
        }


        public IPagedList<Clientes> orderCli(int pageIndex, int pageSize)
        {



            IPagedList<Clientes> CliPage = null;
            List<Clientes> objCli = new List<Clientes>();
            objCli = listarClientes();
            CliPage = objCli.ToPagedList(pageIndex, pageSize);

            return CliPage;
        }





    }
}