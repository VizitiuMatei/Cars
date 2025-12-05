using System.Collections.Generic;
using CarsApi.Observer;

namespace Cars.Observers
{
    public class CarEventPublisher
    {
        private readonly List<ICarCreatedListener> _listeners = new();

        public void Subscribe(ICarCreatedListener listener)
        {
            _listeners.Add(listener);
        }

        public void Unsubscribe(ICarCreatedListener listener)
        {
            _listeners.Remove(listener);
        }

        public void NotifyCarCreated()
        {
            foreach (var listener in _listeners)
            {
                listener.OnCarCreated();
            }
        }
    }
}