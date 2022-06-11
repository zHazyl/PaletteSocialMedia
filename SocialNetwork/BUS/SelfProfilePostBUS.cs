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

        public SelfProfilePostBUS(User user, ViewPost viewPost)
        {
            _user = user;
            _postCommands = new SelfProfilePostCommand(user);
            _viewPost = viewPost;
        }

        public User User { get => _user; set => _user = value; }
        public SelfProfilePostCommand PostCommands { get => _postCommands; }
        public ViewPost ViewPost { get => _viewPost; set => _viewPost = value; }
    }
}
