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
        if (newUser.Username == null)
        {
            MessageBox.Show("Vui lòng nhập tên người dùng!");
            return false;
        }
        if (newUser.Name == null)
        {
            MessageBox.Show("Vui lòng nhập tên đầy đủ!");
            return false;
        }
        if (newUser.Phone == null)
        {
            MessageBox.Show("Vui lòng nhập số điện thoại!");
            return false;
        }
        if (newUser.Email == null)
        {
            MessageBox.Show("Vui lòng nhập email!");
            return false;
        }
        if (newUser.Password == null)
        {
            MessageBox.Show("Vui lòng nhập mật khẩu!");
            return false;
        }

        List<User> users = userDAO.GetAllUsers();
        foreach (User user in users)
        {
            if (user.Username.Equals(newUser.Username))
            {
                MessageBox.Show("Tên người dùng đã tồn tại");
                return false;
            }
            if (user.Phone.Equals(newUser.Phone))
            {
                MessageBox.Show("Số điện thoại đã tồn tại!");
                return false;
            }
            if (user.Email.Equals(newUser.Email))
            {
                MessageBox.Show("Email đã tồn tại!");
                return false;
            }
        }
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
            MessageBox.Show("Vui lòng nhập tên người dùng!");
            return false;
        }
        else if (password == "")
        {
            MessageBox.Show("Vui lòng nhập mật khẩu!");
            return false;
        }
        #endregion

        #region Chuỗi lệnh kiểm tra xem tài khoản có hợp lệ hay không
        if (user == null)
        {
            MessageBox.Show("Tài khoản không tồn tại!");
            return false;
        }
        else
        {
            if (password != user.Password)
            {
                MessageBox.Show("Mật khẩu không đúng!");
                return false;
            }
        }
        #endregion

        return true;
    }
    public bool CheckRegister(string name, string gender, string username, string phone, string email, string password, string repassword)
    {
        #region Chuỗi lệnh kiểm tra đã nhập đầu vào hay chưa
        if (name == "")
        {
            MessageBox.Show("Vui lòng nhập họ và tên!");
            return false;
        }
        else if (gender == "")
        {
            MessageBox.Show("Vui lòng chọn giới tính!");
            return false;
        }
        else if (username == "")
        {
            MessageBox.Show("Vui lòng nhập tên người dùng!");
            return false;
        }
        else if (phone == "")
        {
            MessageBox.Show("Vui lòng nhập số điện thoại!");
            return false;
        }
        else if (email == "")
        {
            MessageBox.Show("Vui lòng nhập email!");
            return false;
        }
        else if (password == "")
        {
            MessageBox.Show("Vui lòng nhập mật khẩu!");
            return false;
        }
        else if (repassword == "")
        {
            MessageBox.Show("Vui lòng nhập lại mật khẩu!");
            return false;
        }
        #endregion

        #region Chuỗi lệnh kiểm tra xem thông tin người dùng đã tồn tại hay chưa
        if (GetUserByUsername(username) != null)
        {
            MessageBox.Show("Tên người dùng đã tồn tại!");
            return false;
        }
        else if (GetUserByUsername(phone) != null)
        {
            MessageBox.Show("Số điện thoại đã tồn tại!");
            return false;
        }
        else if (GetUserByUsername(email) != null)
        {
            MessageBox.Show("Email đã tồn tại!");
            return false;
        }
        #endregion

        return true;
    }
}
