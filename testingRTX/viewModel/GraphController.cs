using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeGraphX.WPF;
using RealTimeGraphX;
using RealTimeGraphX.DataPoints;

namespace testingRTX.viewModel
{
    public class GraphController
    {
        public WpfGraphController<TimeSpanDataPoint, DoubleDataPoint> Controller { get; set; }
        public GraphController(int waitTimeMS)
        {
            Controller = new WpfGraphController<TimeSpanDataPoint, DoubleDataPoint>();
            Controller.Range.MinimumY = 0;
            Controller.Range.MaximumY = 1;
            Controller.Range.MaximumX = TimeSpan.FromSeconds(10);
            Controller.Range.AutoY = true;
            Controller.Range.AutoYFallbackMode = GraphRangeAutoYFallBackMode.MinMax;
        }
        public void Update(IReadOnlyList<double> coords, TimeSpan time)
        {
            if (coords.Count != 2) return;
            List<DoubleDataPoint> yy = new List<DoubleDataPoint>()
            {
                coords[0],
                coords[1]
            };
            List<TimeSpanDataPoint> xx = new List<TimeSpanDataPoint>()
            {
                time,
                time
            };
            Controller.PushData(xx, yy);
        }
    }
}
