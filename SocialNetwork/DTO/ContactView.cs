using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DTO
{
    public class ContactView : INotifyPropertyChanged
    {
        public ContactView(User user, ObservableCollection<MessageView> messages)
        {
            User = user;
            Messages = messages;
        }

        public User User { get; set; }
        public ObservableCollection<MessageView> Messages { get; set; }
        public string LastMessage => Messages.Last().Message.Message_text;

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
