using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace testingRTX.model
{
    public class MouseListener : IObservable<IReadOnlyList<double>>
    {
        private readonly List<IObserver<IReadOnlyList<double>>> _observers;
        private readonly List<double> _mouseCoords;
        private readonly int _waitTimeMS;

        public void Fire(IReadOnlyList<double> datum)
        {
            foreach (var observer in _observers)
                // if f'd throw
                //else
                observer.OnNext(datum);
        }

        public MouseListener(int waitTimeMS)
        {
            _observers = new List<IObserver<IReadOnlyList<double>>>();
            _mouseCoords = new List<double>();
            _waitTimeMS = waitTimeMS;
        }

        public void Start()
        {
            Task.Run(() => ListenAsync());
        }

        private async Task ListenAsync()
        {
            while(true)
            {
                _mouseCoords.Clear();
                var y = Cursor.Position.Y;
                var x = Cursor.Position.X;
                _mouseCoords.Add(y);
                _mouseCoords.Add(x);
                Fire(_mouseCoords);
                await Task.Delay(_waitTimeMS);
            }
        }
            
        public void Deconstruct()
        {
            foreach (var observer in _observers.ToArray())
                if (_observers.Contains(observer))
                    observer.OnCompleted();
            _observers.Clear();
        }

        public IDisposable Subscribe(IObserver<IReadOnlyList<double>> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<IReadOnlyList<double>>> _observers;
            private readonly IObserver<IReadOnlyList<double>> _observer;

            public Unsubscriber(List<IObserver<IReadOnlyList<double>>> observers, IObserver<IReadOnlyList<double>> observer)
            {
                _observer = observer;
                _observers = observers;
            }
            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }
    }
}

