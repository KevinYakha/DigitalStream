using OxyPlot;
using OxyPlot.Series;

using Models;
using Accessibility;
using OxyPlot.Axes;

namespace Visualizer
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            for (int i = 0; i < river.WaterLevel.Count - 1; i++)
            {
                dateTimes.Add(DateTime.Now.AddMinutes(-15 * dateTimes.Count));
            }
            dateTimes.Reverse();

            List<DataPoint> data = new();
            for (int i = 0; i < river.WaterLevel.Count - 1; i++)
            {
                data.Add(new(DateTimeAxis.ToDouble(dateTimes[i]), river.WaterLevel[i]));
            }

            DateTimeAxis dateTimeAxis = new()
            {
                 Position = AxisPosition.Bottom,
                 StringFormat = "dd/MM HH:mm",
                 Title = "DateTime"
            };

            LinearAxis waterLevelAxis = new()
            {
                Position = AxisPosition.Left,
                StringFormat = "0.00 cm",
                Title = "Water level in cm"
            };

            riverPlot = new PlotModel { Title = "Example" };
            riverPlot.Axes.Add(dateTimeAxis);
            riverPlot.Axes.Add(waterLevelAxis);
            riverPlot.Series.Add(new LineSeries() { ItemsSource = data });
        }

        public PlotModel riverPlot {  get; private set; }
        List<DateTime> dateTimes = new();
        River river = new()
        {
            Id = new Guid(),
            Name = "River One",
            WaterLevel = new()
            {
                300, 315, 317, 330, 320, 316
            },
            RainAmount = new()
            {
                24, 12, 34, 3, 8, 7
            },
            Temperature = new()
            {
                18, 17, 15, 22, 24, 25
            },
            FloodLevel = 325,
            LastUpdate = DateTime.Today
        };
    }
}
