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
    public class SearchBUS : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChanged(string name) =>
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));


        private ObservableCollection<User> _resultSearch;
        public ObservableCollection<User> ResultSearch
        {
            get { return _resultSearch; }
            set
            {
                _resultSearch = value;
                OnPropertyChanged("ResultSearch");
            }
        }
        private string _searchTerm;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                _searchTerm = value;
                OnPropertyChanged("SearchTerm");
            }
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

        public User Self { get; set; }

        private UserDAO userDAO = new UserDAO();

        private LoadSearchCommand _loadSearchCommand;
        public LoadSearchCommand LoadSearchCommand
        {
            get
            {
                return _loadSearchCommand ?? (_loadSearchCommand = new LoadSearchCommand(param => LoadSearch()));
            }
            set { }
        }

        public SearchBUS()
        {
            SearchTerm = "";
            LoadSearch();
        }

        public SearchBUS(Main main)
        {
            Main = main;
            SearchTerm = "";
            LoadSearch();
        }

        public void LoadSearch()
        {
            ResultSearch = new ObservableCollection<User>();
            foreach (var user in userDAO.FindUser(SearchTerm))
            {
                ResultSearch.Add(user);
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
            Main.IsSelectedSearch = false;
            Main.IsSelectedSearch = true;
            Main.HomeTab.IsSelected = false;
            Main.ChatTab.IsSelected = false;
        }

    }
}
