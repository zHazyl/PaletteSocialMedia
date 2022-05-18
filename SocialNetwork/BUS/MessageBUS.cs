using SocialNetwork.BUS.Commands;
using SocialNetwork.DAO;
using SocialNetwork.DTO;
using SocialNetwork.GUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BUS
{
    public class MessageBUS : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private ObservableCollection<ContactView> contacts;
        public ObservableCollection<ContactView> Contacts
        {
            get => contacts;
            set
            {
                contacts = value;
                OnPropertyChanged("Contacts");
            }
        }

        public User Self { get; set; }

        public MessageBUS(User self)
        {
            Self = self;
            LoadMess();
        }

        public MessageBUS(User self, Main main)
        {
            Self = self;
            Main = main;
            LoadMess();
        }

        public void LoadMess()
        {
            Contacts = new ObservableCollection<ContactView>();
            var messageDAO = new MessageDAO();
            var userDAO = new UserDAO();
            var userBUS = new UserBUS();
            var contactUsers = new ObservableCollection<User>(userDAO.GetAllUsersContact(Self.User_id));
            foreach (var contactUser in contactUsers)
            {
                var messages = messageDAO.GetMessagesWithContact(Self.User_id, contactUser.User_id);
                var messageViews = new ObservableCollection<MessageView>();
                foreach (var message in messages)
                {
                    if (message.Send_id == Self.User_id)
                    {
                        messageViews.Add(new MessageView(Self, message, "RightToLeft", "Right", "#7d4dcd", "White"));
                    }
                    else
                    {
                        messageViews.Add(new MessageView(contactUser, message, "LeftToRight", "Left", "#cfcfcf", "Black"));
                    }
                }
                Contacts.Add(new ContactView(contactUser, messageViews));
            }
        }


        private MessageCommand _displayMessageCommand;
        public MessageCommand DisplayMessageCommand
        {
            get
            {
                return _displayMessageCommand ?? (_displayMessageCommand = new MessageCommand(param => DisplayMessage(param)));
            }
            set { }
        }

        private LoadCommand _syncCommand;
        public LoadCommand SyncCommand
        {
            get
            {
                return _syncCommand ?? (_syncCommand = new LoadCommand(param => Sync(param)));
            }
            set { }
        }


        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnPropertyChanged("Message");
            }
        }

        public void Sync(object contactView)
        {
            var cv = (ContactView)contactView;
            var messageDAO = new MessageDAO();
            var messages = messageDAO.GetMessagesWithContact(Self.User_id, cv.User.User_id);
            var messageViews = new ObservableCollection<MessageView>();
            foreach (var message in messages)
            {
                if (message.Send_id == Self.User_id)
                {
                    messageViews.Add(new MessageView(Self, message, "RightToLeft", "Right", "#7d4dcd", "White"));
                }
                else
                {
                    messageViews.Add(new MessageView(cv.User, message, "LeftToRight", "Left", "#cfcfcf", "Black"));
                }
            }
            cv.Messages = messageViews;
        }

        public void DisplayMessage(object contactView)
        {
            var cv = (ContactView)contactView;
            if (Message == "" || Message == null)
            {
                Sync(cv);
                return;
            }
            var msg = new Message(Message, Self.User_id, cv.User.User_id);
            cv.Messages.Add(new MessageView(Self, msg, "RightToLeft", "Right", "#7d4dcd", "White"));
            var messageDAO = new MessageDAO();
            messageDAO.AddMessage(msg);
            Message = "";
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

        public void SearchTab(object o)
        {
            Main.SearchTab.IsSelected = true;
            Main.HomeTab.IsSelected = false;
        }
    }
}
