using SocialNetwork.BUS.Commands;
using SocialNetwork.DAO;
using SocialNetwork.DTO;
using SocialNetwork.GUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace SocialNetwork.BUS
{
    public class SelfProfileBUS : BaseBUS
    {
        private PostDAO _postDAO;
        private User _user;
        private ObservableCollection<ViewPost> _viewPosts; // do not break it
        private ObservableCollection<SelfProfilePostBUS> _selfProfilePosts;
        private SelfProfileCommand _commands;
        private ICommand _reloadCommand;
        //private SelfProfilePostCommand _postCommand;


        // prop
        public User User { get => _user; set => _user = value; }
        public ObservableCollection<ViewPost> ViewPosts { get => _viewPosts; }
        public SelfProfileCommand Commands { get => _commands; }
        public ObservableCollection<SelfProfilePostBUS> SelfProfilePosts { get => _selfProfilePosts; }
        public ICommand ReloadCommand { get { return _reloadCommand ?? (_reloadCommand = new ReloadCommand(_user, _viewPosts, _selfProfilePosts, LoadViewPosts)); } }
        //public SelfProfilePostCommand PostCommand { get => _postCommand; }


        // ctor
        public SelfProfileBUS(User user)
        {
            _user = user;
            _postDAO = new PostDAO();
            _viewPosts = new ObservableCollection<ViewPost>();
            _commands = new SelfProfileCommand(_user);
            //_postCommand = new SelfProfilePostCommand(User, ViewPosts, Comment);

            LoadViewPosts();

            // load viewPosts into selfProfilePosts
            _selfProfilePosts = new ObservableCollection<SelfProfilePostBUS>();
            foreach (var post in _viewPosts)
            {
                _selfProfilePosts.Add(new SelfProfilePostBUS(_user, post));
            }
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
                this._viewPosts.Add(new ViewPost(
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

    public class ReloadCommand : BaseAbstractCommand
    {
        private User _user;
        private ObservableCollection<ViewPost> _viewPosts;
        private ObservableCollection<SelfProfilePostBUS> _selfProfilePosts;
        private Action _loadViewPosts;

        public ReloadCommand(User user, ObservableCollection<ViewPost> viewPosts, ObservableCollection<SelfProfilePostBUS> selfProfilePosts, Action LoadViewPosts)
        {
            _user = user;
            _viewPosts = viewPosts;
            _loadViewPosts = LoadViewPosts;
            _selfProfilePosts = selfProfilePosts;
        }

        public override void Execute(object? parameter)
        {
            _viewPosts.Clear();
            _selfProfilePosts.Clear();

            _loadViewPosts.Invoke();

            // load viewPosts into selfProfilePosts
            foreach (var post in _viewPosts)
            {
                _selfProfilePosts.Add(new SelfProfilePostBUS(_user, post));
            }

            Debug.WriteLine(_viewPosts.Count);
        }
    }
}
