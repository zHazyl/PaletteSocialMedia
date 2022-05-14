using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{
    public class Message
    {
        public Message(int message_id, string message_text, int send_id, int recieve_id, DateTime time)
        {
            Message_id = message_id;
            Message_text = message_text;
            Send_id = send_id;
            Recieve_id = recieve_id;
            Created_at = time;
        }

        public Message()
        {

        }
        public Message(string message_text, int send_id, int recieve_id)
        {
            Message_text = message_text;
            Send_id = send_id;
            Recieve_id = recieve_id;
        }

        public int Message_id { get; set; }
        public string Message_text { get; set; }
        public int Send_id { get; set; }
        public int Recieve_id { get; set; }
        public DateTime Created_at { get; set; }
    }
}
