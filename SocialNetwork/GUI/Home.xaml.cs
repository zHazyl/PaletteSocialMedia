using SocialNetwork.BUS;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.CompilerServices;
using SocialNetwork.DTO;

namespace SocialNetwork.GUI
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public static readonly DependencyProperty SelfProperty =
            DependencyProperty.Register("Self", typeof(PostBUS), typeof(UserControl), new FrameworkPropertyMetadata(null));
        public PostBUS Self
        {
            get { return (PostBUS)GetValue(SelfProperty); }
            set { SetValue(SelfProperty, value); }
        }
        public Home()
        {
            InitializeComponent();
            Loaded += (sender, args) =>
            {
                DataContext = Self;
            };
        }
    }
}
