using MySql.Data.MySqlClient;
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
    public class Post_likeDAO
    {
        private DBMySQLConnection connection;

        public Post_likeDAO()
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

        public List<Post_like> GetAllPost_likes()
        {
            string sql = "select * from post_likes";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Post_like> post_likes = ConvertDataTableToList<Post_like>(dataTable);
            return post_likes;

        }

        public List<User> GetPost_likesWithPost(int post_id)
        {
            string sql = $"select * from post_likes where post_id = {post_id}";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Post_like> post_likes = ConvertDataTableToList<Post_like>(dataTable);
            var users = new List<User>();
            UserDAO userDAO = new UserDAO();
            foreach (Post_like post in post_likes)
            {
                users.Add(userDAO.GetUserWithId(post.User_id));
            }
            return users;
        }

        public int GetLikes(int post_id)
        {
            string sql = $"SELECT COUNT(user_id) AS Likes FROM post_likes Where (post_id = {post_id});";
            DataTable dataTable = connection.executeRetrieveQuery(sql);
            return Int32.Parse(dataTable.Rows[0]["Likes"].ToString());
        }

        public void DeleteLike(int user_id, int post_id)
        {
            string sql = $"DELETE FROM post_likes WHERE post_id = {post_id} and user_id = {user_id}; ";
            connection.executeDeleteQuery(sql);
        }
        public void AddLike(int user_id, int post_id)
        {
            string sql = $"INSERT INTO post_likes (user_id, post_id) VALUES ({user_id}, {post_id});";
            connection.executeInsertQuery(sql);
        }
    }
}
