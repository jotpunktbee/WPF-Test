using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModel
{
    public interface IListen { }
    public interface IListen<T> : IListen
    {
        void Handle(T obj);
    }

    public class EventAggregator
    {
        private List<IListen> _subscribers = new List<IListen>();

        public void Subscribe(IListen model)
        {
            _subscribers.Add(model);
        }

        public void Unsubscribe(IListen model)
        {
            _subscribers.Remove(model);
        }

        public void Publish<T>(T message)
        {
            foreach (var item in _subscribers.OfType<IListen<T>>())
            {
                item.Handle(message);
            }
        }
    }
}
