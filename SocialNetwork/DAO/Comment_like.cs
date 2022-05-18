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
    public class Comment_likeDAO
    {
        private DBMySQLConnection connection;

        public Comment_likeDAO()
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

        public List<Comment_like> GetAllComment_likes()
        {
            string sql = "select * from comment_likes";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Comment_like> comment_likes = ConvertDataTableToList<Comment_like>(dataTable);
            return comment_likes;

        }

        public List<User> GetComment_likesWithComment(int comment_id)
        {
            string sql = $"select * from comment_likes where comment_id = {comment_id}";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Comment_like> comment_likes = ConvertDataTableToList<Comment_like>(dataTable);
            var users = new List<User>();
            UserDAO userDAO = new UserDAO();
            foreach (Comment_like comment in comment_likes)
            {
                users.Add(userDAO.GetUserWithId(comment.User_id));
            }
            return users;
        }

        public int GetLikes(int comment_id)
        {
            string sql = $"SELECT COUNT(user_id) AS Likes FROM comment_likes Where (comment_id = {comment_id});";
            DataTable dataTable = connection.executeRetrieveQuery(sql);
            return Int32.Parse(dataTable.Rows[0]["Likes"].ToString());
        }

        public void DeleteLike(int user_id, int comment_id)
        {
            string sql = $"DELETE FROM comment_likes WHERE comment_id = {comment_id} and user_id = {user_id}; ";
            connection.executeDeleteQuery(sql);
        }
        public void AddLike(int user_id, int comment_id)
        {
            string sql = $"INSERT INTO comment_likes (user_id, comment_id) VALUES ({user_id}, {comment_id});";
            connection.executeInsertQuery(sql);
        }
    }
}
