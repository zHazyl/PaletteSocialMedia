using SocialNetwork.Connection;
using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAO
{
    public class PostDAO
    {
        private DBMySQLConnection connection;

        public PostDAO()
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

        public List<Post> GetAllPosts()
        {
            string sql = "select * from post order by created_at DESC limit 20;";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Post> posts = ConvertDataTableToList<Post>(dataTable);
            return posts;
            
        }

        public List<Post> GetAllUserPosts(User user)
        {
            string sql = $"select * from post where user_id = {user.User_id} order by created_at DESC;";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Post> posts = ConvertDataTableToList<Post>(dataTable);
            return posts;

        }

        public List<Post> GetAllFollowPosts(User user)
        {
            string sql = $"select * from post where user_id in (select followee_id from follows where follower_id = {user.User_id}) and user_id != {user.User_id} order by created_at DESC limit 20;";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Post> posts = ConvertDataTableToList<Post>(dataTable);
            return posts;
        }

        public List<Post> GetAllNotFollowPosts(User user)
        {
            string sql = $"select * from post where user_id not in (select followee_id from follows where follower_id = {user.User_id}) and user_id != {user.User_id} order by created_at DESC limit 20;";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Post> posts = ConvertDataTableToList<Post>(dataTable);
            return posts;
        }

        public int AddPost(int user_id, string caption)
        {
            string sql = $"INSERT INTO post (user_id, caption) VALUES ({user_id},'{caption}');";
            connection.executeInsertQuery(sql);
            sql = $"select max(post_id) as post_id from post";
            DataTable dataTable = connection.executeRetrieveQuery(sql);
            return (int)dataTable.Rows[0][0];
        }

    }
}
