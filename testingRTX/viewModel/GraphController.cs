using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealTimeGraphX.WPF;
using RealTimeGraphX;
using RealTimeGraphX.DataPoints;
using System.Windows.Media;

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
            Controller.Range.MaximumX = TimeSpan.FromSeconds(waitTimeMS);
            Controller.Range.AutoY = true;
            Controller.Range.AutoYFallbackMode = GraphRangeAutoYFallBackMode.MinMax;
            Controller.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "Y",
                Stroke = Colors.Red,
                StrokeThickness = 5
            }) ;
            Controller.DataSeriesCollection.Add(new WpfGraphDataSeries()
            {
                Name = "X",
                Stroke = Colors.Red,
                StrokeThickness = 5
            });
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
