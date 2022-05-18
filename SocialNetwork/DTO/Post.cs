using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{
    public class Post : INotifyPropertyChanged
    {
        public Post(int post_id, int photo_id, int video_id, int user_id, string caption, string location, DateTime created_at)
        {
            Post_id = post_id;
            Photo_id = photo_id;
            Video_id = video_id;
            User_id = user_id;
            Caption = caption;
            Location = location;
            Created_at = created_at;
        }

        public Post()
        {
        }

        public int Post_id { get; set; }
        public int Photo_id { get; set; }
        public int Video_id { get; set; }
        public int User_id { get; set; }
        public string Caption { get; set; }
        public string Location { get; set; }
        public DateTime Created_at { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
