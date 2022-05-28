using SocialNetwork.BUS.Commands;
using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BUS
{
    public class SelfProfilePostBUS : BaseBUS
    {
        private User _user;
        private SelfProfilePostCommand _postCommands;
        private ViewPost _viewPost;
        private string _comment;

        public SelfProfilePostBUS(User user, ViewPost viewPost)
        {
            _user = user;
            _comment = "test";
            _postCommands = new SelfProfilePostCommand(user, _comment);
            _viewPost = viewPost;
        }

        public User User { get => _user; set => _user = value; }
        public SelfProfilePostCommand PostCommands { get => _postCommands; }
        public ViewPost ViewPost { get => _viewPost; set => _viewPost = value; }
        public string Comment { get => _comment; set { _comment = value; OnPropertyChanged("Comment"); Debug.WriteLine(_comment); } }
    }
}
