using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    public class SuccessViewModel
    {
        private ICommand successCommand;

        public ICommand SuccessCommand
        {
            get
            {
                return successCommand;
            }
            set
            {
                successCommand = value;
            }
        }

        public SuccessViewModel(EventAggregator eventAggregator)
        {
            eventAggregator.Subscribe(this);
        }
    }
}
