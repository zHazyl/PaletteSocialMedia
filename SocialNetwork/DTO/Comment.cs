using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{
    public class Comment
    {
        public Comment(int comment_id, string comment_text, int post_id, int user_id, DateTime created_at)
        {
            Comment_id = comment_id;
            Comment_text = comment_text;
            Post_id = post_id;
            User_id = user_id;
            Created_at = created_at;
        }

        public Comment()
        {

        }
        public Comment(string comment_text, int post_id, int user_id)
        {
            Comment_text = comment_text;
            Post_id = post_id;
            User_id = user_id;
        }

        public int Comment_id { get; set; }
        public string Comment_text { get; set; }
        public int Post_id { get; set; }
        public int User_id { get; set; }
        public DateTime Created_at { get; set; }
    }
}
