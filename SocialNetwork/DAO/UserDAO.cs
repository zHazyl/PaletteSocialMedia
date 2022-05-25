using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using MySql.Data.MySqlClient;
using SocialNetwork.Connection;
using SocialNetwork.DTO;

namespace SocialNetwork.DAO
{
    internal class UserDAO
    {
        private DBMySQLConnection connection;
        public UserDAO()
        {
            connection = new DBMySQLConnection();
        }
        #region *** Convert DT to List<Object> ***

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
        #endregion
        public List<User> GetAllUsers()
        {

            string sql = "select * from users";
            DataTable dataTable = connection.executeRetrieveQuery(sql);
            List<User> userList = ConvertDataTableToList<User>(dataTable);
            return userList;
        }

        public List<User> GetAllUsersContact(int user_id)
        {

            string sql = $"select * from users where (user_id in (select user2 from contacts where user1 = {user_id})) or  (user_id in (select user1 from contacts where user2 = {user_id})) order by created_at ASC limit 8;";
            DataTable dataTable = connection.executeRetrieveQuery(sql);
            List<User> userList = ConvertDataTableToList<User>(dataTable);
            return userList;
        }
        public bool IsUsersFollow(int follower_id, int followee_id)
        {

            string sql = $"SELECT COUNT(followee_id) AS isfollow FROM follows where follower_id = {follower_id} and followee_id = {followee_id};";
            DataTable dataTable = connection.executeRetrieveQuery(sql);
            if (Int32.Parse(dataTable.Rows[0]["isfollow"].ToString()) == 0)
            {
                return false;
            }
            return true;
        }

        public bool IsUsersContact(int user1, int user2)
        {

            string sql = $"SELECT COUNT(user2) AS iscontact FROM contacts where (user1 = {user1} and user2 = {user2}) or (user2 = {user1} and user1 = {user2});";
            DataTable dataTable = connection.executeRetrieveQuery(sql);
            if (Int32.Parse(dataTable.Rows[0]["iscontact"].ToString()) == 0)
            {
                return false;
            }
            return true;
        }

        public User GetUserWithId(int user_id)
        {
            return GetAllUsers().Find(user => user.User_id == user_id);
        }
        public List<User> FindUser(string searchTerm)
        {
            List<User> userList = new List<User>();
            foreach(User user in GetAllUsers())
            {
                if (user.Username.Contains(searchTerm) == true || user.Name.Contains(searchTerm) == true)
                {
                    userList.Add(user);
                }
            }
            return userList;
        }
        public void AddUser(User newUser)
        {
            string sql = "insert into users(name, username, phone, email, password, gender) values(@name, @username, @phone, @email, @password, @gender)";
            MySqlParameter[] mySqlParameters = new MySqlParameter[6];
            mySqlParameters[0] = new MySqlParameter("@name", SqlDbType.NVarChar);
            mySqlParameters[0].Value = Convert.ToString(newUser.Name);
            mySqlParameters[1] = new MySqlParameter("@username", SqlDbType.NVarChar);
            mySqlParameters[1].Value = Convert.ToString(newUser.Username);
            mySqlParameters[2] = new MySqlParameter("@phone", SqlDbType.NVarChar);
            mySqlParameters[2].Value = Convert.ToString(newUser.Phone);
            mySqlParameters[3] = new MySqlParameter("@email", SqlDbType.NVarChar);
            mySqlParameters[3].Value = Convert.ToString(newUser.Email);
            mySqlParameters[4] = new MySqlParameter("@password", SqlDbType.NVarChar);
            mySqlParameters[4].Value = Convert.ToString(newUser.Password);
            mySqlParameters[5] = new MySqlParameter("@gender", SqlDbType.NVarChar);
            mySqlParameters[5].Value = Convert.ToString(newUser.Gender);
            connection.executeInsertQuery(sql, mySqlParameters);
        }

        public void EditProfileUser(User user)
        {
            string sql = $"update users set name='{user.Name}', username='{user.Username}', bio='{user.Bio}', email='{user.Email}' where user_id={user.User_id}";
            connection.executeUpdateQuery(sql);
        }

        public void ChangePasswordUser(User user, string password)
        {
            string sql = $"update users set password='{password}' where user_id={user.User_id}";
            connection.executeUpdateQuery(sql);
        }

        public void AddFollowUser(User follower, User followee)
        {
            string sql = $"insert into follows(follower_id,followee_id) values ({follower.User_id},{followee.User_id});";
            connection.executeInsertQuery(sql);
        }

        public void DeleteFollowUser(User follower, User followee)
        {
            string sql = $"DELETE FROM follows WHERE follower_id = {follower.User_id} and followee_id = {followee.User_id};";
            connection.executeInsertQuery(sql);
        }

        public void AddContactUser(User user1, User user2)
        {
            string sql = $"insert into contacts(user1,user2) values ({user1.User_id},{user2.User_id});";
            connection.executeInsertQuery(sql);
        }
    }
}
