using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LojaRoupasMVCv6.Models.POJO;
using MySql.Data.MySqlClient;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class AdminDAO
    {

        public List<Admin> AdminList()
        {
            List<Admin> Retorno = new List<Admin>();
            try
            {
                Connection objCon = new Connection();

                MySqlConnection Banco = new MySqlConnection();
                string user = "uvendedor";
                Banco = objCon.AbreConexao(user);
                MySqlCommand command = Banco.CreateCommand();
                command.CommandText = "select idAdmin, nomeAdmin from admin";
                MySqlDataReader Reader = command.ExecuteReader();
                while (Reader.Read())
                {
                    Admin objAdm = new Admin();
                    objAdm.idAdmin = Reader.GetInt32(0);
                    objAdm.nomeAdmin = Reader.GetString(1);

                    Retorno.Add(objAdm);
                }

                objCon.FechaConexao();
            }


            catch { }

            return Retorno;
        }


        public string verificaAdmin(Admin a)
        {
            string retorno = "0";

            Connection objCon = new Connection();

            MySqlConnection Banco = new MySqlConnection();
            string user = "uadmin";
            Banco = objCon.AbreConexao(user);
            MySqlCommand command = Banco.CreateCommand();
            command.CommandText = "Select * from admin where idAdmin = '" + a.idAdmin + "' and senhaAdmin='" + a.senhaAdmin + "'";
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



    }
}