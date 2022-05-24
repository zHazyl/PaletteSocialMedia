using SocialNetwork.DAO;
using SocialNetwork.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BUS.Commands
{
    public class SelfProfilePostCommand
    {
        private User _user;
        private ObservableCollection<ViewPost> _viewPosts;
        private string _comment;
        private LikeCommand _displayLikeCommand;
        private LoadCommand _loadPost;
        private CommentCommand _displayCommentCommand;
        private LikeCommand _displayLikeCommentCommand;

        public SelfProfilePostCommand(User user, ObservableCollection<ViewPost> viewPosts, string comment)
        {
            _user = user;
            _viewPosts = viewPosts;
            _comment = comment;
        }

        public User User { get => _user; }
        public string Comment { get => _comment; set => _comment = value; }
        public LikeCommand DisplayLikeCommand { get { return _displayLikeCommand ?? (_displayLikeCommand = new LikeCommand(param => DisplayLike(param))); } }
        public LoadCommand LoadPost { get { return _loadPost ?? (_loadPost = new LoadCommand(param => LoadViewPosts())); } }
        public CommentCommand DisplayCommentCommand { get { return _displayCommentCommand ?? (_displayCommentCommand = new CommentCommand(param => DisplayComment(param))); } }
        public LikeCommand DisplayLikeCommentCommand { get { return _displayLikeCommentCommand ?? (_displayLikeCommentCommand = new LikeCommand(param => DisplayLikeComment(param))); } }

        public void DisplayComment(object viewPost)
        {
            var vb = (ViewPost)viewPost;
            var cmt = new Comment(Comment, vb.Post.Post_id, _user.User_id);
            vb.Comment.Add(new CommentView(_user, cmt));
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
                post_likeDAO.AddLike(_user.User_id, vb.Post.Post_id);
            }
            else
            {
                vb.Likes--;
                post_likeDAO.DeleteLike(_user.User_id, vb.Post.Post_id);
            }
        }

        public void DisplayLikeComment(object commentView)
        {
            var cv = (CommentView)commentView;
            var comment_likeDAO = new Comment_likeDAO();
            if (cv.IsLiked)
            {
                cv.LikedCount++;
                comment_likeDAO.AddLike(_user.User_id, cv.Comment.Comment_id);
            }
            else
            {
                cv.LikedCount--;
                comment_likeDAO.DeleteLike(_user.User_id, cv.Comment.Comment_id);
            }
        }

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
