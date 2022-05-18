using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{
    public class Comment_like
    {
        public Comment_like(int user_id, int comment_id, DateTime created_at)
        {
            User_id = user_id;
            Comment_id = comment_id;
            Created_at = created_at;
        }

        public Comment_like()
        {

        }

        public int User_id { get; set; }
        public int Comment_id { get; set; }
        public DateTime Created_at { get; set; }
    }
}
