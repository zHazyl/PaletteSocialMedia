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
    public class CommentDAO
    {
        private DBMySQLConnection connection;

        public CommentDAO()
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

        public List<Comment> GetAllComments()
        {
            string sql = "select * from comments";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Comment> comments = ConvertDataTableToList<Comment>(dataTable);
            return comments;

        }

        public List<Comment> GetCommentsWithPost(int post_id)
        {
            string sql = $"select * from comments where post_id = {post_id}";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Comment> comments = ConvertDataTableToList<Comment>(dataTable);
            return comments;

        }

        public int GetComments(int post_id)
        {
            string sql = $"SELECT COUNT(user_id) AS Comments FROM comments Where (post_id = {post_id});";
            DataTable dataTable = connection.executeRetrieveQuery(sql);
            return Int32.Parse(dataTable.Rows[0]["Comments"].ToString());
        }

        public void AddComment(Comment cmt)
        {
            string sql = $"INSERT INTO comments (comment_text, user_id, post_id) VALUES ('{cmt.Comment_text}', {cmt.User_id}, {cmt.Post_id});";
            connection.executeInsertQuery(sql);
        }
    }
}
