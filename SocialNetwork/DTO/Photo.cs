using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{
    public class Photo
    {
        public Photo(int id, string url, int post_id, DateTime created_at, float size)
        {
            Photo_id = id;
            Photo_url = url;
            Post_id = post_id;
            Created_at = created_at;
            Size = size;
        }

        public Photo()
        {

        }

        public int Photo_id { get; set; }
        public string Photo_url { get; set; }
        public int Post_id { get; set; }
        public DateTime Created_at { get; set; }
        public float Size { get; set; }


    }
}
