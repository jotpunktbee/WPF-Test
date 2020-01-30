using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    public class Window1ViewModel : ViewModelBase
    {
        private string _information;
        public string Information
        {
            get
            {
                return _information;
            }
            set
            {
                _information = value;
                OnPropertyChanged("Information");
            }
        }

        public Window1ViewModel()
        {
            Messaging.Messenger.Instance.Register<string>(this, Notify);
            Information = "Text";
        }

        public void Notify(string message)
        {
            Information = message;
        }
    }
}
