using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

using SocialNetwork.BUS;
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
        public Login(string user, string password)
        {
            InitializeComponent();
            txtBoxUser.Text = user;
            txtBoxPassword.Text = password;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool isLoginSuccess = userBUS.CheckLogin(txtBoxUser.Text, txtBoxPassword.Text);
            if (isLoginSuccess)
            {
                UserBUS userBUS = new UserBUS();
                User user = userBUS.GetUserByUsername(txtBoxUser.Text);
                //MessageBox.Show("Đăng nhập thành công!");
                // Viết đoạn code chuyển sang trang chủ ở dưới đây
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