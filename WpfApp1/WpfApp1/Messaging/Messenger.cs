using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WpfApp1.Messaging
{
    public class Messenger : IMessenger
    {
        private static Messenger _instance;
        private static object _lockObject = new object();

        private Dictionary<Type, List<ActionIdentifier>> _references = new Dictionary<Type, List<ActionIdentifier>>();
        private Messenger() { }

        public static Messenger Instance
        {
            get
            {
                lock (_lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new Messenger();
                    }

                    return _instance;
                }
            }
        }

        public void Register<TNotification>(object recipient, Action<TNotification> action)
        {
            Register<TNotification>(recipient, null, action);
        }

        public void Register<TNotification>(object recipient, string identCode, Action<TNotification> action)
        {
            Type messageType = typeof(TNotification);

            if (!_references.ContainsKey(messageType))
            {
                _references.Add(messageType, new List<ActionIdentifier>());
            }

            var actionIdentifier = new ActionIdentifier();
            actionIdentifier.Action = new WeakReferenceAction<TNotification>(recipient, action);
            actionIdentifier.IdentificationCode = identCode;

            _references[messageType].Add(actionIdentifier);
        }

        public void Send<TNotification>(TNotification notification)
        {
            Type type = typeof(TNotification);
            List<ActionIdentifier> typeActionIdentifiers = _references[type];
            foreach (ActionIdentifier actionIdentifier in typeActionIdentifiers)
            {
                IActionParameter actionParameter = actionIdentifier.Action as IActionParameter;
                if (actionParameter != null)
                {
                    actionParameter.ExecuteWithParameter(notification);
                }
                else
                {
                    actionIdentifier.Action.Execute();
                }
            }
        }

        public void Send<TNotification>(TNotification notification, string identCode)
        {
            Type type = typeof(TNotification);
            List<ActionIdentifier> typeActionIdentifiers = _references[type];
            foreach (ActionIdentifier actionIdentifier in typeActionIdentifiers)
            {
                if (actionIdentifier.IdentificationCode == identCode)
                {
                    IActionParameter actionParameter = actionIdentifier.Action as IActionParameter;
                    if (actionParameter != null)
                    {
                        actionParameter.ExecuteWithParameter(notification);
                    }
                    else
                    {
                        actionIdentifier.Action.Execute();
                    }
                }
            }
        }

        public void Unregister<TNotification>(object recipient)
        {
            Unregister<TNotification>(recipient, null);
        }

        public void Unregister<TNotification>(object recipient, string identCode)
        {
            bool lockTaken = false;

            try
            {
                Monitor.Enter(_references, ref lockTaken);
                foreach (Type targetType in _references.Keys)
                {
                    foreach (ActionIdentifier wra in _references[targetType])
                    {
                        if (wra.Action != null && wra.Action.Target != null && wra.Action.Target.Target == recipient)
                        {
                            if (String.IsNullOrEmpty(identCode) || (!String.IsNullOrEmpty(identCode) && !String.IsNullOrEmpty(wra.IdentificationCode) && wra.IdentificationCode.Equals(identCode)))
                            {
                                wra.Action.Unload();
                            }
                        }
                    }
                }
            }
            finally
            {
                if (lockTaken)
                {
                    Monitor.Exit(_references);
                }
            }
        }
    }
}
