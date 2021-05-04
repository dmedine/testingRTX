using System.Collections.Generic;
using System.Diagnostics;
using testingRTX.model;

namespace testingRTX.viewModel
{
    public class MainViewModel
    {
        private readonly MouseListener _mouseListener;
        private readonly CoordsClient _coordsClient;
        private readonly Stopwatch _watch;
        public GraphController GraphController { get; set; }
        public MainViewModel()
        {
            int waitTimeMS = 10;
            _mouseListener = new MouseListener(waitTimeMS);
            
            _coordsClient = new CoordsClient()
            {
                OnNextEvent = UpdateGraphController
            };
            _coordsClient.Subscribe(_mouseListener);
            GraphController = new GraphController(waitTimeMS);
            _watch = new Stopwatch();
            _watch.Start();
            _mouseListener.Start();
        }

        public void UpdateGraphController(IReadOnlyList<double> coords)
        {
            var now = _watch.Elapsed;
            GraphController.Update(coords, now);
        }

        
    }
}
