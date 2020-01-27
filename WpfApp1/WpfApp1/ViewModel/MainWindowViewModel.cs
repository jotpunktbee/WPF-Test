using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private ICommand button1Command;
        private ICommand toggleExecuteCommand;

        private bool canExecute = true;

        public string ButtonContent
        {
            get
            {
                return "Click me!";
            }
        }

        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }
            set
            {
                if (this.canExecute == value)
                {
                    return;
                }
                this.canExecute = value;
            }
        }

        public ICommand ToggleExecuteCommand
        {
            get
            {
                return toggleExecuteCommand;
            }
            set
            {
                toggleExecuteCommand = value;
            }
        }

        public ICommand Button1Command
        {
            get
            {
                return button1Command;
            }
            set
            {
                button1Command = value;
            }
        }

        private object _selectedTemplate;
        public object SelectedTemplate
        {
            get
            {
                return _selectedTemplate;
            }
            set
            {
                _selectedTemplate = value;
                OnPropertyChanged("SelectedTemplate");
            }
        }

        public MainWindowViewModel()
        {
            Button1Command = new RelayCommand(ShowMessage, param => this.canExecute);
            ToggleExecuteCommand = new RelayCommand(ChangeCanExecute);
        }

        public void ShowMessage(object obj)
        {
            SelectedTemplate = new UserControl1ViewModel();
            MessageBox.Show(obj.ToString());
        }

        public void ChangeCanExecute(object obj)
        {
            canExecute = !canExecute;
        }
    }
}
