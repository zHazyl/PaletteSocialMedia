using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SocialNetwork.DTO
{
    public class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyNamme = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyNamme));
        }
    }
}
