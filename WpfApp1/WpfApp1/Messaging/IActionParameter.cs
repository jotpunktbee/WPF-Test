using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Messaging
{
    interface IActionParameter
    {
        void ExecuteWithParameter(object parameter);
    }
}
