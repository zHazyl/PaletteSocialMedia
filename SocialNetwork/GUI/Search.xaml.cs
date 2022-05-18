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
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : UserControl
    {
        public static readonly DependencyProperty SearchUserProperty =
            DependencyProperty.Register("SearchUser", typeof(SearchBUS), typeof(UserControl), new FrameworkPropertyMetadata(null));
        public SearchBUS SearchUser
        {
            get { return (SearchBUS)GetValue(SearchUserProperty); }
            set { SetValue(SearchUserProperty, value); }
        }
        public Search()
        {
            InitializeComponent();
            Loaded += (sender, args) =>
            {
                DataContext = SearchUser;
            };
        }
    }
}
