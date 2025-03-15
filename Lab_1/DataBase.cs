using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1
{
    internal class DataBase
    {
        SqlConnection _connection = new SqlConnection(@"Data Source=ALEX-DESKTOP;Initial Catalog=TravelAgency;Integrated Security=True");
        public SqlConnection GetConnection() => _connection;
        public void OpenConnection()
        {
            if(_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }           
        }
        public void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}
