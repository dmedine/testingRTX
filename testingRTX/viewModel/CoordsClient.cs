using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testingRTX.viewModel
{
    public class CoordsClient : IObserver<IReadOnlyList<double>>
    {
        public delegate void Callback(IReadOnlyList<double> coords);
        public Callback OnNextEvent { get; set; } = null;
        private IDisposable _unsubscriber;

        public virtual void Subscribe(IObservable<IReadOnlyList<double>> server)
        {
            _unsubscriber = server.Subscribe(this);
        }
        // IObserver
        public virtual void OnCompleted()
        {
            Unsubscribe();
        }

        public virtual void OnError(Exception e)
        {

        }

        public virtual void OnNext(IReadOnlyList<double> coords)
        {
            OnNextEvent(coords);
        }

        public virtual void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }
    }
}

