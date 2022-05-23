using SocialNetwork.BUS.Commands;
using SocialNetwork.DAO;
using SocialNetwork.DTO;
using SocialNetwork.GUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace SocialNetwork.BUS
{
    public class PostBUS : INotifyPropertyChanged
    {
        public User Self { get; set; }
        public PostDAO postDAO { get; } = new PostDAO();
        private ObservableCollection<ViewPost> viewposts;
        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged("Comment");
            }
        }
        public ObservableCollection<ViewPost> Viewposts
        {
            get
            {
                return this.viewposts;
            }

            set
            {
                this.viewposts = value;
                this.OnPropertyChanged("Viewposts");
            }
        }

        public PostBUS()
        {
            Comment = "";
            Self = new User();
            LoadViewPosts();
        }
        public PostBUS(User self)
        {
            Comment = "";
            Self = self;
            LoadViewPosts();
            DisplayLikeCommand = new LikeCommand(DisplayLike);
        }

        public PostBUS(User self, Main main)
        {
            Comment = "";
            Self = self;
            LoadViewPosts();
            DisplayLikeCommand = new LikeCommand(DisplayLike);
            Main = main;
        }

        public PostBUS(User self, string explore)
        {
            Comment = "";
            Self = self;
            LoadViewPosts(explore);
            DisplayLikeCommand = new LikeCommand(DisplayLike);
        }
        public PostBUS(User self, Main main, string feature)
        {
            Comment = "";
            Main = main;
            Self = self;
            if (feature == "explore")
            {
                LoadViewPosts(feature);
            }
            else
            {
                LoadViewPosts(feature);
            }
            DisplayLikeCommand = new LikeCommand(DisplayLike);
        }

        private Main _main;
        public Main Main
        {
            get { return _main; }
            set
            {
                _main = value;
                OnPropertyChanged("Main");
            }
        }

        private BaseCommand _searchTabCommand;
        public BaseCommand SearchTabCommand
        {
            get
            {
                return _searchTabCommand ?? (_searchTabCommand = new BaseCommand(param => SearchTab(param)));
            }
        }

        public void SearchTab(object user)
        {
            var rs = (User)user;
            Main.Result = rs;
            Main.SearchUser.ResultSearch.Clear();
            Main.ResultSearch = new PostBUS(rs, Main, "search");
            Main.SearchTab.IsSelected = true;
            Main.HomeTab.IsSelected = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public List<Post> GetAllPost()
        {
            return postDAO.GetAllPosts();
        }
        /* 
            UserPost = user_photo;
            Post_photo = post_photo;
            Likes = likes;
            user_liked
            Users_liked = users_liked;
            Comment = comment;
         */
        void LoadViewPosts(string feature = "follow")
        {
            List<Post> posts;
            if (feature == "follow")
            {
                posts = postDAO.GetAllFollowPosts(Self);// List<Post>
            }
            else if (feature == "explore")
            {
                posts = postDAO.GetAllNotFollowPosts(Self);// List<Post>
            }
            else
            {
                posts = postDAO.GetAllUserPosts(Main.Result);// List<Post>
            }
            var userDAO = new UserDAO();
            var photoDAO = new PhotoDAO();
            var post_likeDAO = new Post_likeDAO();
            var comment_likeDAO = new Comment_likeDAO();
            var commentDAO = new CommentDAO();
            Viewposts = new ObservableCollection<ViewPost>();
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
                    islikedcmt = commentlike.Any(usr => usr.User_id == Self.User_id);
                    comments.Add(new CommentView(userDAO.GetUserWithId(cmt.User_id), cmt, islikedcmt, comment_likeDAO.GetLikes(cmt.Comment_id)));
                }
                var postlike = post_likeDAO.GetPost_likesWithPost(post.Post_id);
                bool isliked = postlike.Any(usr => usr.User_id == Self.User_id);
                Viewposts.Add(new ViewPost(
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

        //void LoadViewExplorePosts()
        //{
        //    var posts = postDAO.GetAllNotFollowPosts(Self);// List<Post>
        //    var userDAO = new UserDAO();
        //    var photoDAO = new PhotoDAO();
        //    var post_likeDAO = new Post_likeDAO();
        //    var commentDAO = new CommentDAO();
        //    Viewposts = new ObservableCollection<ViewPost>();
        //    ObservableCollection<CommentView> comments;
        //    foreach (var post in posts)
        //    {
        //        comments = new ObservableCollection<CommentView>();
        //        var cmts = commentDAO.GetCommentsWithPost(post.Post_id); //List<Comment>
        //        foreach (var cmt in cmts)
        //        {
        //            comments.Add(new CommentView(userDAO.GetUserWithId(cmt.User_id), cmt));
        //        }
        //        var postlike = post_likeDAO.GetPost_likesWithPost(post.Post_id);
        //        bool isliked = postlike.Any(usr => usr.User_id == Self.User_id);
        //        Viewposts.Add(new ViewPost(
        //            post,
        //            userDAO.GetUserWithId(post.User_id),
        //            photoDAO.GetPhotosWithPost(post.Post_id),
        //            post_likeDAO.GetLikes(post.Post_id),
        //            isliked,
        //            commentDAO.GetComments(post.Post_id),
        //            postlike,
        //            comments
        //            ));
        //    }
        //}

        //void LoadViewProfilePosts()
        //{
        //    var posts = postDAO.GetAllUserPosts(Main.Result);// List<Post>
        //    var userDAO = new UserDAO();
        //    var photoDAO = new PhotoDAO();
        //    var post_likeDAO = new Post_likeDAO();
        //    var commentDAO = new CommentDAO();
        //    Viewposts = new ObservableCollection<ViewPost>();
        //    ObservableCollection<CommentView> comments;
        //    foreach (var post in posts)
        //    {
        //        comments = new ObservableCollection<CommentView>();
        //        var cmts = commentDAO.GetCommentsWithPost(post.Post_id); //List<Comment>
        //        foreach (var cmt in cmts)
        //        {
        //            comments.Add(new CommentView(userDAO.GetUserWithId(cmt.User_id), cmt));
        //        }
        //        var postlike = post_likeDAO.GetPost_likesWithPost(post.Post_id);
        //        bool isliked = postlike.Any(usr => usr.User_id == Self.User_id);
        //        Viewposts.Add(new ViewPost(
        //            post,
        //            userDAO.GetUserWithId(post.User_id),
        //            photoDAO.GetPhotosWithPost(post.Post_id),
        //            post_likeDAO.GetLikes(post.Post_id),
        //            isliked,
        //            commentDAO.GetComments(post.Post_id),
        //            postlike,
        //            comments
        //            ));
        //    }

        //}


        private LikeCommand _displayLikeCommand;
        public LikeCommand DisplayLikeCommand {
            get
            {
                return _displayLikeCommand ?? (_displayLikeCommand = new LikeCommand(param => DisplayLike(param)));
            }
            private set
            {

            }
        }

        private LoadCommand _loadPost;
        public LoadCommand LoadPost
        {
            get
            {
                return _loadPost ?? (_loadPost = new LoadCommand(param => LoadViewPosts()));
            }
        }

        private CommentCommand _displayCommentCommand;
        public CommentCommand DisplayCommentCommand
        {
            get
            {
                return _displayCommentCommand ?? (_displayCommentCommand = new CommentCommand(param => DisplayComment(param)));
            }
            set { }
        }

        public void DisplayComment(object viewPost)
        {
            var vb = (ViewPost)viewPost;
            var cmt = new Comment(Comment, vb.Post.Post_id, Self.User_id);
            vb.Comment.Add(new CommentView(Self, cmt));
            vb.Comments++;
            var commentDAO = new CommentDAO();
            commentDAO.AddComment(cmt);
            Comment = "";
        }

        public void DisplayLike(object viewPost)
        {
            var vb = (ViewPost)viewPost; 
            var post_likeDAO = new Post_likeDAO();
            if (vb.IsLiked)
            {
                vb.Likes++;
                post_likeDAO.AddLike(Self.User_id, vb.Post.Post_id);
            }
            else
            {
                vb.Likes--;
                post_likeDAO.DeleteLike(Self.User_id,vb.Post.Post_id);
            }
        }


        private LikeCommand _displayLikeCommentCommand;
        public LikeCommand DisplayLikeCommentCommand
        {
            get
            {
                return _displayLikeCommentCommand ?? (_displayLikeCommentCommand = new LikeCommand(param => DisplayLikeComment(param)));
            }
            private set
            {

            }
        }

        public void DisplayLikeComment(object commentView)
        {
            var cv = (CommentView)commentView;
            var comment_likeDAO = new Comment_likeDAO();
            if (cv.IsLiked)
            {
                cv.LikedCount++;
                comment_likeDAO.AddLike(Self.User_id, cv.Comment.Comment_id);
            }
            else
            {
                cv.LikedCount--;
                comment_likeDAO.DeleteLike(Self.User_id, cv.Comment.Comment_id);
            }
        }
    }
}
