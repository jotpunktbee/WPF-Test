using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Messaging
{
    public class WeakReferenceAction
    {
        private WeakReference _target;
        private Action _action;

        public WeakReference Target
        {
            get
            {
                return _target;
            }
        }

        public WeakReferenceAction(object target, Action action)
        {
            _target = new WeakReference(target);
            _action = action;
        }

        public void Execute()
        {
            if (_action != null && _target != null && _target.IsAlive)
            {
                _action.Invoke();
            }
        }

        public void Unload()
        {
            _target = null;
            _action = null;
        }
    }

    public class WeakReferenceAction<T> : WeakReferenceAction, IActionParameter
    {
        private Action<T> _action;

        public Action<T> Action
        {
            get
            {
                return _action;
            }
        }

        public WeakReferenceAction(object target, Action<T> action)
            : base(target, null)
        {
            _action = action;
        }

        public new void Execute()
        {
            if (_action != null && Target != null && Target.IsAlive)
            {
                _action(default(T));
            }
        }

        public void Execute(T parameter)
        {
            if (_action != null && Target != null && Target.IsAlive)
            {
                _action(parameter);
            }
        }

        public void ExecuteWithParameter(object parameter)
        {
            Execute((T)parameter);
        }
    }
}
