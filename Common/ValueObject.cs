using Common.Interfaces;
using System.ComponentModel;

namespace Common
{
    public class ValueObject : IValueUpdatable, INotifyPropertyChanged
    {
        public string value;
        public string Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
