using SocialNetwork.Connection;
using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAO
{
    public class MessageDAO
    {
        private DBMySQLConnection connection;

        public MessageDAO()
        {
            connection = new DBMySQLConnection();
        }

        private List<I> ConvertDataTableToList<I>(DataTable datatable) where I : class
        {
            List<I> lstRecord = new List<I>();
            try
            {
                List<string> columnsNames = new List<string>();
                foreach (DataColumn DataColumn in datatable.Columns)
                    columnsNames.Add(DataColumn.ColumnName);
                lstRecord = datatable.AsEnumerable().ToList().ConvertAll<I>(row => GetObject<I>(row, columnsNames));
                return lstRecord;
            }
            catch
            {
                return lstRecord;
            }

        }

        private I GetObject<I>(DataRow row, List<string> columnsName) where I : class
        {
            I obj = (I)Activator.CreateInstance(typeof(I));
            try
            {
                PropertyInfo[] Properties = typeof(I).GetProperties();
                foreach (PropertyInfo objProperty in Properties)
                {
                    string columnname = columnsName.Find(name => name.ToLower() == objProperty.Name.ToLower());
                    if (!string.IsNullOrEmpty(columnname))
                    {
                        object dbValue = row[columnname];
                        if (dbValue != DBNull.Value)
                        {
                            if (Nullable.GetUnderlyingType(objProperty.PropertyType) != null)
                            {
                                objProperty.SetValue(obj, Convert.ChangeType(dbValue, Type.GetType(Nullable.GetUnderlyingType(objProperty.PropertyType).ToString())), null);
                            }
                            else
                            {
                                objProperty.SetValue(obj, Convert.ChangeType(dbValue, Type.GetType(objProperty.PropertyType.ToString())), null);
                            }
                        }
                    }
                }
                return obj;
            }
            catch
            {
                return obj;
            }
        }

        public List<Message> GetAllMessages()
        {
            string sql = "select * from messages";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Message> messages = ConvertDataTableToList<Message>(dataTable);
            return messages;

        }

        public ObservableCollection<Message> GetMessagesWithRecieve_id(int recieve_id)
        {
            string sql = $"select * from messages where recieve_id = {recieve_id}";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Message> messages = ConvertDataTableToList<Message>(dataTable);
            var rs = new ObservableCollection<Message>(messages);
            return rs;

        }
        public ObservableCollection<Message> GetMessagesWithSend_id(int send_id)
        {
            string sql = $"select * from messages where recieve_id = {send_id}";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Message> messages = ConvertDataTableToList<Message>(dataTable);
            var rs = new ObservableCollection<Message>(messages);
            return rs;

        }

        public ObservableCollection<Message> GetMessagesWithSelf(int self_id)
        {
            string sql = $"select * from messages where recieve_id = {self_id} or send_id = {self_id}";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Message> messages = ConvertDataTableToList<Message>(dataTable);
            var rs = new ObservableCollection<Message>(messages);
            return rs;

        }

        public ObservableCollection<Message> GetMessagesWithContact(int self_id, int user_id)
        {
            string sql = $"select * from messages where (recieve_id = {user_id} and send_id = {self_id}) or (recieve_id = {self_id} and send_id = {user_id}) order by time";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Message> messages = ConvertDataTableToList<Message>(dataTable);
            var rs = new ObservableCollection<Message>(messages);
            return rs;

        }

        public void AddMessage(Message cmt)
        {
            string sql = $"INSERT INTO messages (message_text, send_id, recieve_id) VALUES ('{cmt.Message_text}', {cmt.Send_id}, {cmt.Recieve_id});";
            connection.executeInsertQuery(sql);
        }
    }
}
