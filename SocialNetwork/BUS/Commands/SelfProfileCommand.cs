using SocialNetwork.DAO;
using SocialNetwork.DTO;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SocialNetwork.BUS.Commands
{
    public class SelfProfileCommand
    {
        private User _user;
        private ICommand _editProfileCommand;
        private ICommand _changePasswordCommand;
        private SelfProfileChangePhotoCommands _changePhotoCommands;

        // props
        public ICommand EditProfileCommand { get { return _editProfileCommand ?? (_editProfileCommand = new EditProfileCommand(_user)); } }
        public ICommand ChangePasswordCommand { get { return _changePasswordCommand ?? (_changePasswordCommand = new ChangePasswordCommand(_user)); } }
        public SelfProfileChangePhotoCommands ChangePhotoCommands { get { return _changePhotoCommands ?? (_changePhotoCommands = new SelfProfileChangePhotoCommands(_user)); } }

        // ctor
        public SelfProfileCommand(User user)
        {
            _user = user;
        }
    }

    // COMMAND CLASS (single)
    public class RemoveCurrentPhotoCommand : BaseAbstractCommand
    {
        private User _user;
        public RemoveCurrentPhotoCommand(User user)
        {
            _user = user;
        }
        public override void Execute(object? parameter)
        {
            _user.Profile_photo_url = "/assets/images/profile_photo_url/default_user_image.png";
            // update database
        }
    }

    public class UploadPhotoCommand : BaseAbstractCommand
    {
        private User _user;
        public UploadPhotoCommand(User user)
        {
            _user = user;
        }
        public override void Execute(object? parameter)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.FileName = "Pictures"; // Default file name
            dialog.DefaultExt = ".png"; // Default file extension

            bool? result = dialog.ShowDialog();

            string filePath = string.Empty;
            if (result == true)
            {
                // Open document
                filePath = dialog.FileName;
            }
            _user.Profile_photo_url = filePath;
            // move file into /assets/images/profile_photo_url
            string fileName = Path.GetFileName(filePath);
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            File.Copy(filePath, projectDirectory + @"/assets/images/profile_photo_url/" + fileName, true);
            // update database
        }
    }

    public class EditProfileCommand : BaseAbstractCommand
    {
        private User _user;
        private UserDAO _userDAO;
        public EditProfileCommand(User user)
        {
            _user = user;
            _userDAO = new UserDAO();
        }
        public override void Execute(object? parameter)
        {
            var values = (object[])parameter;
            // check valid
            // update model
            _user.Name = values[0].ToString();
            _user.Username = values[1].ToString();
            _user.Bio = values[2].ToString();
            _user.Email = values[3].ToString();

            Debug.WriteLine($"{_user.Name}, {_user.Username}, {_user.Bio}, {_user.Email}");
            // update database
            _userDAO.EditProfileUser(_user);
        }
    }

    public class ChangePasswordCommand : BaseAbstractCommand
    {
        private User _user;
        private UserDAO _userDAO;
        public ChangePasswordCommand(User user)
        {
            _user = user;
            _userDAO = new UserDAO();
        }
        public override void Execute(object? parameter)
        {
            var values = (object[])parameter;
            var oldPasswordBox = (PasswordBox)values[0];
            var newPasswordBox = (PasswordBox)values[1];
            var confirmNewPasswordBox = (PasswordBox)values[2];
            var oldPassword = oldPasswordBox.Password;
            var newPassword = newPasswordBox.Password;
            var confirmNewPassword = confirmNewPasswordBox.Password;
            // check vaild
            if ((oldPassword == _user.Password) & (newPassword == confirmNewPassword))
            {
                // update model
                _user.Password = newPassword;
                // update database
                _userDAO.ChangePasswordUser(_user, newPassword);
                MessageBox.Show("Success!");
            }
            else
                MessageBox.Show("Password is not valid!");

        }
    }
}
