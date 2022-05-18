using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{
    public class ViewPost : INotifyPropertyChanged
    {
        public ViewPost(User user_photo, List<Photo> post_photo, int likes, List<User> users_liked, ObservableCollection<CommentView> comment)
        {
            UserPost = user_photo;
            Post_photo = post_photo;
            Likes = likes;
            Users_liked = users_liked;
            Comment = comment;
        }

        public ViewPost()
        {
            UserPost = new User();
            Post_photo = new List<Photo>();
            Likes = 0;
            Users_liked = new List<User>();
            Comment = new ObservableCollection<CommentView>();
        }

        public ViewPost(User userPost, List<Photo> post_photo, int likes, int comments, List<User> users_liked, ObservableCollection<CommentView> comment)
        {
            UserPost = userPost;
            Post_photo = post_photo;
            Likes = likes;
            Comments = comments;
            Users_liked = users_liked;
            Comment = comment;
        }

        public ViewPost(Post post, User userPost, List<Photo> post_photo, int likes, bool isLiked, int comments, List<User> users_liked, ObservableCollection<CommentView> comment)
        {
            Post = post;
            UserPost = userPost;
            Post_photo = post_photo;
            Likes = likes;
            IsLiked = isLiked;
            Comments = comments;
            Users_liked = users_liked;
            Comment = comment;
        }

        public ViewPost(Post post, User userPost, List<Photo> post_photo, int likes, int comments, List<User> users_liked, ObservableCollection<CommentView> comment)
        {
            Post = post;
            UserPost = userPost;
            Post_photo = post_photo;
            Likes = likes;
            Comments = comments;
            Users_liked = users_liked;
            Comment = comment;
        }

        public Post Post { get; set; }
        public User UserPost { get; set; }
        public List<Photo> Post_photo { get; set; }
        private int likes;
        public int Likes 
        { 
            get { return likes; }
            set 
            {  
                likes = value;
                OnPropertyChanged("Likes");
            }
        }
        private bool isLiked;
        public bool IsLiked
        {
            get { return isLiked; }
            set 
            { 
                isLiked = value;
                OnPropertyChanged("IsLiked");
            }
        }
        public int _comments;
        public int Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
                OnPropertyChanged("Comments");
            }
        }
        public List<User> Users_liked { get; set; }
        public ObservableCollection<CommentView> Comment { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
