using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MySql.Data.MySqlClient;

namespace SocialNetwork.Connection
{
    public class DBMySQLConnection
    {
        private MySqlConnection connection;
        public DBMySQLConnection()
        {
            connection = new MySqlConnection("server=localhost;user=root;database=social_media;port=3306;password=$Tnh210302");
        }
        private MySqlConnection Open()
        {
            if (connection.State == ConnectionState.Closed || connection.State == ConnectionState.Broken)
            {
                connection.Open();
            }
            return connection;
        }
        private MySqlConnection Close()
        {
            if (connection.State == ConnectionState.Open || connection.State == ConnectionState.Connecting)
            {
                connection.Close();
            }
            return connection;
        }
        public DataTable executeRetrieveQuery(String query)
        {
            using (MySqlCommand sqlCommand = new MySqlCommand(query, Open()))
            {
                sqlCommand.CommandType = CommandType.Text;
                try
                {
                    DataTable dataTable = new DataTable();
                    MySqlDataReader dataReader = sqlCommand.ExecuteReader();
                    dataTable.Load(dataReader);
                    return dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex));
                    return null;
                }
                finally
                {
                    Close();
                }
            }
        }
        public void executeInsertQuery(String query, MySqlParameter[] sqlParameter)
        {
            using (MySqlCommand sqlCommand = new MySqlCommand(query, Open()))
            {
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddRange(sqlParameter);
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Close();
                }
            }
        }
        public void executeInsertQuery(String query)
        {
            using (MySqlCommand sqlCommand = new MySqlCommand(query, Open()))
            {
                sqlCommand.CommandType = CommandType.Text;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Close();
                }
            }
        }
        public void executeUpdateQuery(String query, MySqlParameter[] sqlParameter)
        {
            using (MySqlCommand sqlCommand = new MySqlCommand(query, Open()))
            {
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Parameters.AddRange(sqlParameter);
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Close();
                }
            }
        }
        public void executeDeleteQuery(String query)
        {
            using (MySqlCommand sqlCommand = new MySqlCommand(query, Open()))
            {
                sqlCommand.CommandType = CommandType.Text;
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Close();
                }
            }
        }
    }
}
