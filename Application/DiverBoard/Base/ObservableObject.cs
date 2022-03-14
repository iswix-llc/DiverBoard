using DiverBoard.Services;
using System.ComponentModel;
using System.Windows;

namespace DiverBoard.Base
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
                if (DataService.Instance.Trip != null)
                {
                    DataService.Instance.Save();
                }
            }
        }
    }
}
