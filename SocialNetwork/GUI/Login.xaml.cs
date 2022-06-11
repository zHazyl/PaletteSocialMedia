using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

using SocialNetwork.BUS;
using SocialNetwork.DAO;
using SocialNetwork.DTO;

namespace SocialNetwork.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        private UserBUS userBUS = new UserBUS();
        public Login()
        {
            InitializeComponent();
        }
        public Login(string username, string password)
        {
            InitializeComponent();
            txtBoxUsername.Text = username;
            passwordBox.Password = password;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool isLoginSuccess = userBUS.CheckLogin(txtBoxUsername.Text, passwordBox.Password);
            if (isLoginSuccess)
            {
                UserBUS userBUS = new UserBUS();
                User user = userBUS.GetUserByUsername(txtBoxUsername.Text);
                UserDAO userDAO = new UserDAO();
                user.Followers = userDAO.CountUserFollowers(user.User_id);
                user.Following = userDAO.CountUserFollowing(user.User_id);
                user.Count_posts = userDAO.CountUserPosts(user.User_id);

                // Chuyển hướng sang trang chủ
                Main main = new Main(user);
                this.Close();
                main.ShowDialog();
                //
            }
        }

        private void btnSignUp_MouseDown(object sender, MouseEventArgs e)
        {
            SignUp formSignUp = new SignUp();
            this.Close();
            formSignUp.ShowDialog();
        }
    }
}