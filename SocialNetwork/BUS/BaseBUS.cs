using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SocialNetwork.BUS
{
    public class BaseBUS : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyNamme = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyNamme));
        }
    }
}
