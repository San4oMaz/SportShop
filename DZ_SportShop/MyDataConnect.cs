using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DZ.Models;

namespace DZ
{
    class MyDataConnect
    {
        private SqlConnection connection = null;
        private SqlCommand command = null;
        public SqlConnection GetSqlConnection { get => connection; }
        public SqlCommand GetSqlCommand { get => command; }
        public MyDataConnect()
        {
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString);            
        }

        public void InitCommand(string comandString, CommandType comandType)
        {
            command = new SqlCommand(comandString, connection);
            command.CommandType = comandType;
        }

        public List<TypeGoods> GetAllTypeGoods()
        {
            List<TypeGoods> listTypes = new List<TypeGoods>();
            TypeGoods typeG = null; 
            command = new SqlCommand (@"Select * from TypeGoods", connection);
            using (SqlDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    typeG = new TypeGoods { Id = Convert.ToInt32(dataReader[0]), TypeName = dataReader[1].ToString() };
                    listTypes.Add(typeG);
                }
            }
            return listTypes;
        }

        public List<Managers> GetAllManagers()
        {
            List<Managers> listManagers = new List<Managers>();
            Managers manager = null;
            command = new SqlCommand(@"Select * from Managers", connection);
            using(SqlDataReader dataReader = command.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    manager = new Managers { Id = Convert.ToInt32(dataReader[0]), ManagerName = dataReader[1].ToString() };
                    listManagers.Add(manager);
                }
            }
            return listManagers;
        }
    }
}
