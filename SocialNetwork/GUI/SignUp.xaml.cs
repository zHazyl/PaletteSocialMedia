using System.Windows;
using System.Windows.Input;

using SocialNetwork.BUS;
using SocialNetwork.DTO;

namespace SocialNetwork.GUI
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        UserBUS userBUS = new UserBUS();
        public SignUp()
        {
            InitializeComponent();
        }
        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string name = txtBoxName.Text;
            string username = txtBoxUsername.Text;
            string phone = txtBoxPhone.Text;
            string email = txtBoxEmail.Text;
            string gender = txtBoxGender.Text;
            string password = passwordBox.Password.ToString();
            string repassword = repasswordBox.Password.ToString();

            bool isRegisterSuccess = userBUS.CheckRegister(name, gender, username, phone, email, password, repassword);

            if (isRegisterSuccess)
            {
                MessageBox.Show("Sign up successfully! Press ok to login...");
                User user = new User(name, username, phone, email, password, gender);
                userBUS.AddUser(user);

                Login formLogin = new Login(username, password);
                this.Close();
                formLogin.ShowDialog();
            }

        }
        private void btnLogin_MouseDown(object sender, MouseEventArgs e)
        {
            Login formLogin = new Login();
            this.Close();
            formLogin.ShowDialog();
        }
    }
}
