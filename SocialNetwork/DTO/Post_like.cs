using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{
    public class Post_like
    {
        public Post_like(int user_id, int post_id, DateTime created_at)
        {
            User_id = user_id;
            Post_id = post_id;
            Created_at = created_at;
        }

        public Post_like()
        {

        }

        public int User_id { get; set; }
        public int Post_id { get; set; }
        public DateTime Created_at { get; set; }
    }
}
