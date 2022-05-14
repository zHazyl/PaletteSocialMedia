using System;

namespace SocialNetwork.DTO
{
    public class User
    {
        public User()
        {

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

        public int User_id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Profile_photo_url { get; set; }
        public string Bio { get; set; }
        public DateTime Created_at { get; set; }
    }
}
