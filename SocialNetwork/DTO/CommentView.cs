using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{

    public class CommentView : INotifyPropertyChanged
    {
        public User User { get; set; }
        public Comment Comment { get; set; }
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

        public CommentView(User user, Comment comment)
        {
            User = user;
            Comment = comment;
        }

        public CommentView(User user, Comment comment, bool isLiked, int likedCount) : this(user, comment)
        {
            IsLiked = isLiked;
            LikedCount = likedCount;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
