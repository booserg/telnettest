using Common.Interfaces;

namespace TelnetServerApp.ViewModels
{
    public class ValueViewModel
    {
        public IValueUpdatable ValueObject { get; private set; }

        public ValueViewModel(IValueUpdatable obj)
        {
            ValueObject = obj;
        }
    }
}
