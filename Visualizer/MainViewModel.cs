using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Models;

namespace Visualizer
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            chartViewModel = new();
            buttonsViewModel = new();
        }

        ChartViewModel chartViewModel;
        ButtonsViewModel buttonsViewModel;
    }
}
