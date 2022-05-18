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
    public class PhotoDAO
    {
        private DBMySQLConnection connection;

        public PhotoDAO()
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

        public List<Photo> GetAllPhotos()
        {
            string sql = "select * from photos";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Photo> photos = ConvertDataTableToList<Photo>(dataTable);
            return photos;

        }

        public List<Photo> GetPhotosWithPost(int post_id)
        {
            string sql = $"select * from photos where post_id = {post_id}";
            DataTable dataTable = connection.executeRetrieveQuery(sql);

            List<Photo> photos = ConvertDataTableToList<Photo>(dataTable);
            return photos;

        }
        public void AddPhoto(int post_id, string photo_url)
        {
            string sql = $"INSERT INTO photos (post_id, photo_url) VALUES ({post_id},'{photo_url}');";
            connection.executeInsertQuery(sql);
        }
    }
}
