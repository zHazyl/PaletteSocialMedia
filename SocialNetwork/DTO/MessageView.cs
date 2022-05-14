using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{
    public class MessageView : INotifyPropertyChanged
    {
        public User User { get; set; }
        public Message Message { get; set; }
        public string Flow { get; set; }
        public string Horizontal { get; set; }
        public string Background { get; set; }
        public string Foreground { get; set; }
        private bool _isLiked;
        public bool IsLiked
        {
            get { return _isLiked; }
            set
            {
                _isLiked = value;
                OnPropertyChanged("IsLiked");
            }
        }
        private int _likedCount;
        public int LikedCount
        {
            get { return _likedCount; }
            set
            {
                _likedCount = value;
                OnPropertyChanged("LikedCount");
            }
        }

        public MessageView(User user, Message message)
        {
            User = user;
            Message = message;
        }

        public MessageView(User user, Message message, string flow, string horizontal, string background, string foreground) : this(user, message)
        {
            Flow = flow;
            Horizontal = horizontal;
            Background = background;
            Foreground = foreground;
        }

        public MessageView(User user, Message message, bool isLiked, int likedCount) : this(user, message)
        {
            IsLiked = isLiked;
            LikedCount = likedCount;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
