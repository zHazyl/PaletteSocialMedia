using System;

namespace SocialNetwork.DTO
{
    public class User : BaseModel
    {
        // ctor
        public User()
        {
            User_id = 0;
            Name = "";
            Username = "";
            Phone = "";
            Email = "";
            Password = "";
            Gender = "";
            Profile_photo_url = "";
            Bio = "";
        }
        public User(string name, string username, string phone, string email, string password, string gender)
        {
            Name = name;
            Username = username;
            Phone = phone;
            Email = email;
            Password = password;
            Gender = gender;
        }
        public User(int user_id, string name, string username, string phone, string email, string password, string gender, string profile_photo_url, string bio, DateTime created_at)
        {
            User_id = user_id;
            Name = name;
            Username = username;
            Phone = phone;
            Email = email;
            Password = password;
            Gender = gender;
            Profile_photo_url = profile_photo_url;
            Bio = bio;
        }

        //
        private int _user_id;
        private string _name;
        private string _username;
        private string _phone;
        private string _email;
        private string _password;
        private string _gender;
        private string _profile_photo_url;
        private string _bio;
        private DateTime _created_at;
        // prop
        public int User_id { get => _user_id; set => _user_id = value; }
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }
        public string Username
        {
            get => _username;
            set { _username = value; OnPropertyChanged(); }
        }
        public string Phone
        {
            get => _phone;
            set { _phone = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }
        public string Password { get; set; }
        public string Gender
        {
            get => _gender;
            set { _gender = value; OnPropertyChanged(); }
        }
        public string Profile_photo_url
        {
            get => _profile_photo_url;
            set { _profile_photo_url = value; OnPropertyChanged(); }
        }
        public string Bio
        {
            get => _bio;
            set { _bio = value; OnPropertyChanged(); }
        }
        public DateTime Created_at { get; set; }
    }
}
