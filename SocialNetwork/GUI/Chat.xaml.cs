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

namespace SocialNetwork.GUI
{
    /// <summary>
    /// Interaction logic for Chat.xaml
    /// </summary>
    public partial class Chat : UserControl
    {
        public static readonly DependencyProperty MessProperty =
            DependencyProperty.Register("Mess", typeof(MessageBUS), typeof(UserControl), new FrameworkPropertyMetadata(null));
        public MessageBUS Mess
        {
            get { return (MessageBUS)GetValue(MessProperty); }
            set { SetValue(MessProperty, value); }
        }
        public Chat()
        {
            InitializeComponent();
            Loaded += (sender, args) =>
            {
                DataContext = Mess;
            };
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
