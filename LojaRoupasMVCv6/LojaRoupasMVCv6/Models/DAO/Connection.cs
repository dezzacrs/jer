using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace LojaRoupasMVCv6.Models.DAO
{
    public class Connection
    {

        MySqlConnection Banco;
        
        public MySqlConnection AbreConexao(string user)
        {

            Banco = new MySqlConnection("server=servidor;database=lojaroupas;uid=lojaroupas;pwd=senha;");
            Banco.Open();
            return Banco;


        }



        //conexão com usuarios diferentes
        
        /*public MySqlConnection AbreConexao(string user)
        {
            string pwd;
            if (user.Equals("uadmin"))
            {
                pwd = "senhaadmin";
            }
            else
            {
                pwd = "senhaoutra";
            }
            Banco = new MySqlConnection("server=localhost;database=lojaroupas;uid="+user+";pwd="+pwd+"");
            Banco.Open();
            return Banco;


        }*/


        public void FechaConexao()
        {

            Banco.Close();
        }

    }
}