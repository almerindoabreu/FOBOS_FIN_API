using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FOBOS_API.Data
{
    public class DataConnection
    {

        private MySqlConnection Con;
        private string stringConnection = "Server=localhost; Port=3306; Uid=root; Pwd=; DataBase=fobos; Convert Zero Datetime=true";

        public DataConnection()
        {
            Con = new MySqlConnection(stringConnection);
        }

        public void AbrirConexao()
        {
            try
            {
                if(Con == null)
                {
                    Con.Open();
                }
            }

            catch (Exception ex)
            {
                Con.Close();

                throw new Exception("ERRO AO REALIZAR A CONEXAO MYSQL. CHAME O SUPORTE. ERRO: " + ex.Message);
            }
        }

        public void FecharConexao()
        {
            try
            {
                if (Con != null && Con.State == System.Data.ConnectionState.Open)
                {
                    Con.Close();
                }

            }
            catch (Exception ex)
            {

                throw new Exception("ERRO AO FECHAR A CONEXAO MYSQL. ERRO: " + ex.Message);
            }

        }

        public MySqlConnection getSQLConnection()
        {
            return Con;
        }

    }
}
