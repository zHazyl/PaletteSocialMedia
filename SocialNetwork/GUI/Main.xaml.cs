using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using SocialNetwork.BUS;
using Microsoft.Win32;
using System.IO;
using SocialNetwork.DAO;

namespace SocialNetwork.GUI
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Main : Window, INotifyPropertyChanged
    {
        public Main()
        {
            UserBUS userBUS = new UserBUS();
            var user = userBUS.GetUserByUsername("roses_are_rosie");
            Self = new PostBUS(user,this);
            Myself = user;
            Explore = new PostBUS(user,this, "explore");
            Mess = new MessageBUS(user, this);
            //username.Text = Myself.Username;
            //img.ImageSource = new BitmapImage(new Uri(Myself.Profile_photo_url));
            InitializeComponent();
        }
        public User Myself { get; set; }
        public Main(User self)
        {
            Myself = self;
            //username.Text = self.Username;
            //img.ImageSource = new BitmapImage(new Uri(self.Profile_photo_url));
            Self = new PostBUS(self);
            SearchUser = new SearchBUS();
            Explore = new PostBUS(self, "explore");
            Mess = new MessageBUS(self);
            Self = new PostBUS(self, this);
            Explore = new PostBUS(self, this, "explore");
            Mess = new MessageBUS(self,this);
            InitializeComponent();
        }

        public static readonly DependencyProperty SelfProperty =
        DependencyProperty.Register("Self", typeof(PostBUS), typeof(Main), new FrameworkPropertyMetadata(null));
        private PostBUS Self
        {
            get { return (PostBUS)GetValue(SelfProperty); }
            set { SetValue(SelfProperty, value); }
        }

        public static readonly DependencyProperty ExploreProperty =
DependencyProperty.Register("Explore", typeof(PostBUS), typeof(Main), new FrameworkPropertyMetadata(null));
        private PostBUS Explore
        {
            get { return (PostBUS)GetValue(ExploreProperty); }
            set { SetValue(ExploreProperty, value); }
        }

        public static readonly DependencyProperty SearchUserProperty =
DependencyProperty.Register("SearchUser", typeof(SearchBUS), typeof(Main), new FrameworkPropertyMetadata(null));
        private SearchBUS SearchUser
        {
            get { return (SearchBUS)GetValue(SearchUserProperty); }
            set { SetValue(SearchUserProperty, value); }
        }


        public static readonly DependencyProperty MessProperty =
DependencyProperty.Register("Mess", typeof(MessageBUS), typeof(Main), new FrameworkPropertyMetadata(null));
        private MessageBUS Mess
        {
            get { return (MessageBUS)GetValue(MessProperty); }
            set { SetValue(MessProperty, value); }
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized; 
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
                Application.Current.MainWindow.WindowState= WindowState.Normal;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        string url;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                var path = new Uri(op.FileName);
                image.Source = new BitmapImage(path);
                string destinationDirectory = @"C:\Users\tnh21\source\repos\SocialNetwork_ProjectWPF\SocialNetwork\assets\images\post\";
                url = System.IO.Path.Combine(destinationDirectory, System.IO.Path.GetFileName(path.ToString()));
                File.Copy(path.OriginalString, url, true);
                url = path.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var photoDAO = new PhotoDAO();
            var postDAO = new PostDAO();
            int post_id = postDAO.AddPost(Myself.User_id, status.Text);
            photoDAO.AddPhoto(post_id, url);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private bool _isSelectedHome;
        public bool IsSelectedHome
        {
            get => _isSelectedHome;
            set
            {
                _isSelectedHome = value;
                OnPropertyChanged("IsSelectedHome");
            }
        }
        private bool _isSelectedChat;
        public bool IsSelectedChat
        {
            get => _isSelectedChat;
            set
            {
                _isSelectedChat = value;
                OnPropertyChanged("IsSelectedChat");
            }
        }
        private bool _isSelectedExplore;
        public bool IsSelectedExplore
        {
            get => _isSelectedExplore;
            set
            {
                _isSelectedExplore = value;
                OnPropertyChanged("IsSelectedExplore");
            }
        }
        private bool _isSelectedProfile;
        public bool IsSelectedProfile
        {
            get => _isSelectedProfile;
            set
            {
                _isSelectedProfile = value;
                OnPropertyChanged("IsSelectedProfile");
            }
        }

        private bool _isSelectedSearch;
        public bool IsSelectedSearch
        {
            get => _isSelectedSearch;
            set
            {
                _isSelectedSearch = value;
                OnPropertyChanged("IsSelectedSearch");
            }
        }
    }
}
