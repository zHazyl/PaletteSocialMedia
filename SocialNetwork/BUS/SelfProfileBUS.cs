using SocialNetwork.BUS.Commands;
using SocialNetwork.DAO;
using SocialNetwork.DTO;
using SocialNetwork.GUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace SocialNetwork.BUS
{
    public class SelfProfileBUS : BaseBUS
    {
        private PostDAO _postDAO;
        private User _user;
        private ObservableCollection<ViewPost> _viewPosts;
        private SelfProfileCommand _commands;
        private SelfProfilePostCommand _postCommand;
        private string _comment;

        // prop
        public User User { get => _user; set => _user = value; }
        public ObservableCollection<ViewPost> ViewPosts { get => _viewPosts; }
        public SelfProfileCommand Commands { get => _commands; }
        public SelfProfilePostCommand PostCommand { get => _postCommand; }
        public string Comment { get => _comment; set { _comment = value; OnPropertyChanged("Comment"); } }


        // ctor
        public SelfProfileBUS(User user)
        {
            _user = user;
            _postDAO = new PostDAO();
            _viewPosts = new ObservableCollection<ViewPost>();
            _commands = new SelfProfileCommand(_user);
            _postCommand = new SelfProfilePostCommand(User, ViewPosts, Comment);

            LoadViewPosts();
        }

        // func
        void LoadViewPosts()
        {
            List<Post> posts;
            PostDAO postDAO = new PostDAO();

            posts = postDAO.GetAllUserPosts(User);// List<Post>

            var userDAO = new UserDAO();
            var photoDAO = new PhotoDAO();
            var post_likeDAO = new Post_likeDAO();
            var comment_likeDAO = new Comment_likeDAO();
            var commentDAO = new CommentDAO();
            _viewPosts = new ObservableCollection<ViewPost>();
            ObservableCollection<CommentView> comments;
            foreach (var post in posts)
            {
                comments = new ObservableCollection<CommentView>();
                var cmts = commentDAO.GetCommentsWithPost(post.Post_id); //List<Comment>

                List<User> commentlike;
                bool islikedcmt = true;
                foreach (var cmt in cmts)
                {
                    commentlike = comment_likeDAO.GetComment_likesWithComment(cmt.Comment_id);
                    islikedcmt = commentlike.Any(usr => usr.User_id == _user.User_id);
                    comments.Add(new CommentView(userDAO.GetUserWithId(cmt.User_id), cmt, islikedcmt, comment_likeDAO.GetLikes(cmt.Comment_id)));
                }
                var postlike = post_likeDAO.GetPost_likesWithPost(post.Post_id);
                bool isliked = postlike.Any(usr => usr.User_id == _user.User_id);
                _viewPosts.Add(new ViewPost(
                    post,
                    userDAO.GetUserWithId(post.User_id),
                    photoDAO.GetPhotosWithPost(post.Post_id),
                    post_likeDAO.GetLikes(post.Post_id),
                    isliked,
                    commentDAO.GetComments(post.Post_id),
                    postlike,
                    comments
                    ));
            }
        }
    }
}
