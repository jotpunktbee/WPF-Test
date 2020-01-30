using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp1.Messaging;
using WpfApp1.Service;
using WpfApp1.View;
using WpfApp1.ViewModel.Util;

namespace WpfApp1.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private RelayCommand button1Command;
        private RelayCommand toggleExecuteCommand;

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

        public RelayCommand ToggleExecuteCommand
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

        public RelayCommand Button1Command
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

        public string Message { get; set; }

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
            Message = "Das ist mein Text!";
            button1Command = new RelayCommand(SendMessage);
            toggleExecuteCommand = new RelayCommand(ShowWindow);
        }

        private void ShowWindow(object obj)
        {
            var genericWindow = new WindowService();
            genericWindow.ShowWindow(new Window1ViewModel());
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

        private void SendMessage(object message)
        {
            Messenger.Instance.Send<string>(Message);
        }
    }
}
