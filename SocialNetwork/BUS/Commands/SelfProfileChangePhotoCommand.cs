using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SocialNetwork.BUS.Commands
{
    public class SelfProfileChangePhotoCommands
    {
        private User _user;
        private ICommand _uploadPhotoCommand;
        private ICommand _removeCurrentPhotoCommand;

        public SelfProfileChangePhotoCommands(User user)
        {
            _user = user;
        }

        public ICommand UploadPhotoCommand { get { return _uploadPhotoCommand ?? (_uploadPhotoCommand = new UploadPhotoCommand(_user)); } }
        public ICommand RemoveCurrentPhotoCommand { get { return _removeCurrentPhotoCommand ?? (_removeCurrentPhotoCommand = new RemoveCurrentPhotoCommand(_user)); } }
    }
}
