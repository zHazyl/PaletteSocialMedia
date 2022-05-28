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
        private string _comment;
        private LikeCommand _displayLikeCommand;
        private CommentCommand _displayCommentCommand;
        private LikeCommand _displayLikeCommentCommand;

        public SelfProfilePostCommand(User user, string comment)
        {
            _user = user;
            _comment = comment;
        }

        public User User { get => _user; }
        public string Comment { get => _comment; set => _comment = value; }
        public LikeCommand DisplayLikeCommand { get { return _displayLikeCommand ?? (_displayLikeCommand = new LikeCommand(param => DisplayLike(param))); } }
        public CommentCommand DisplayCommentCommand { get { return _displayCommentCommand ?? (_displayCommentCommand = new CommentCommand(param => DisplayComment(param))); } }
        public LikeCommand DisplayLikeCommentCommand { get { return _displayLikeCommentCommand ?? (_displayLikeCommentCommand = new LikeCommand(param => DisplayLikeComment(param))); } }

        public void DisplayComment(object values)
        {
            var selfProfilePost = (SelfProfilePostBUS)values;
            var vb = selfProfilePost.ViewPost;
            var cmt = new Comment(Comment, vb.Post.Post_id, _user.User_id);
            vb.Comment.Add(new CommentView(_user, cmt));
            vb.Comments++;
            var commentDAO = new CommentDAO();
            commentDAO.AddComment(cmt);
            Comment = "";
        }

        public void DisplayLike(object values)
        {
            var selfProfilePost = (SelfProfilePostBUS)values;
            var vb = selfProfilePost.ViewPost;
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
    }
}
