﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Service
{
    public class WindowService : IWindowService
    {
        public void ShowWindow(object viewModel)
        {
            var genericWindow = new View.GenericWindow();
            genericWindow.Content = viewModel;
            genericWindow.Show();
        }
    }
}
