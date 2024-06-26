using OxyPlot;
using OxyPlot.Series;

namespace Visualizer
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            this.riverPlot = new PlotModel { Title = "Example" };
            this.riverPlot.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
        }

        public PlotModel riverPlot {  get; private set; }
    }
}
