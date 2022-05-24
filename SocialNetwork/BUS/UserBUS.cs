using System.Collections.Generic;
using System.Windows;

using SocialNetwork.DAO;
using SocialNetwork.DTO;

namespace SocialNetwork.BUS;

internal class UserBUS
{
    private UserDAO userDAO = new UserDAO();
    public List<User> GetAllUsers()
    {
        return userDAO.GetAllUsers();
    }
    public User GetUserByUsername(string username)
    {
        foreach (User user in GetAllUsers())
        {
            if (user.Username == username || user.Phone == username || user.Email == username)
            {
                return user;
            }
        }
        return null;
    }

    public User GetUserByUser_id(int user_id)
    {
        foreach (User user in GetAllUsers())
        {
            if (user.User_id == user_id)
            {
                return user;
            }
        }
        return null;
    }
    public bool AddUser(User newUser)
    {
        userDAO.AddUser(newUser);
        return true;
    }
    public bool CheckLogin(string username, string password)
    {
        // Thực hiện lấy đối tượng người dùng bằng username
        User user = GetUserByUsername(username);

        #region Chuỗi lệnh kiểm tra đã nhập đầu vào hay chưa
        if (username == "")
        {
            MessageBox.Show("Please enter username!");
            return false;
        }
        else if (password == "")
        {
            MessageBox.Show("Please enter password!");
            return false;
        }
        #endregion

        #region Chuỗi lệnh kiểm tra xem tài khoản có hợp lệ hay không
        if (user == null)
        {
            MessageBox.Show("Account does not exist!");
            return false;
        }
        else
        {
            if (password != user.Password)
            {
                MessageBox.Show("Incorrect password!");
                return false;
            }
        }
        #endregion

        return true;
    }
    public bool CheckRegister(string name, string gender, string username, string phone, string email, string password, string repassword)
    {
        #region Chuỗi lệnh kiểm tra đã nhập đầu vào hay chưa
        if (username == "")
        {
            MessageBox.Show("Please enter username!");
            return false;
        }
        else if (name == "")
        {
            MessageBox.Show("Please enter full name!");
            return false;
        }
        else if (phone == "")
        {
            MessageBox.Show("Please enter phone!");
            return false;
        }
        else if (email == "")
        {
            MessageBox.Show("Please enter email!");
            return false;
        }
        else if (gender == "")
        {
            MessageBox.Show("Please enter gender!");
            return false;
        }
        else if (password == "")
        {
            MessageBox.Show("Please enter password!");
            return false;
        }
        else if (repassword != password)
        {
            MessageBox.Show("Repassword is incorrect!");
            return false;
        }
        #endregion

        #region Chuỗi lệnh kiểm tra xem thông tin người dùng đã tồn tại hay chưa
        List<User> users = userDAO.GetAllUsers();
        foreach (User user in users)
        {
            if (user.Username.Equals(username))
            {
                MessageBox.Show("Username already exists. Please enter another username!");
                return false;
            }
            if (user.Phone.Equals(phone))
            {
                MessageBox.Show("Phone already exists. Please enter another phone!");
                return false;
            }
            if (user.Email.Equals(email))
            {
                MessageBox.Show("Email already exists. Please enter another email!");
                return false;
            }
        }
        #endregion

        return true;
    }
    public List<User> FindUser(string searchTerm)
    {
        return userDAO.FindUser(searchTerm);
    }
}
