using OxyPlot;
using OxyPlot.Series;

using Models;
using OxyPlot.Axes;

namespace Visualizer
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            // testing data
            for (int i = 0; i < river.WaterLevel.Count; i++)
            {
                dateTimes.Add(DateTime.Now.AddMinutes(-15 * dateTimes.Count));
            }
            dateTimes.Reverse();

            // --------------- Data ---------------

            List<DataPoint> waterLevelDataPoints = [];
            for (int i = 0; i < river.WaterLevel.Count; i++)
            {
                waterLevelDataPoints.Add(new(DateTimeAxis.ToDouble(dateTimes[i]), river.WaterLevel[i]));
            }

            List<DataPoint> temperatureDataPoints = [];
            for (int i = 0; i < river.Temperature.Count; i++)
            {
                temperatureDataPoints.Add(new(DateTimeAxis.ToDouble(dateTimes[i]), river.Temperature[i]));
            }

            List<DataPoint> floodWarningDataPoints =
            [
                new(DateTimeAxis.ToDouble(dateTimes[0]), river.FloodLevel),
                new(DateTimeAxis.ToDouble(dateTimes[^1]), river.FloodLevel)
            ];

            List<BarItem> rainAmountDataPoints = [];
            for (int i = 0; i < river.WaterLevel.Count; i++)
            {
                rainAmountDataPoints.Add(new(river.RainAmount[i]));
            }

            // --------------- Axes ---------------

            // -------- X-Axis Labels --------
            DateTimeAxis dateTimeAxis = new()
            {
                 Position = AxisPosition.Bottom,
                 StringFormat = "dd/MM HH:mm",
                 Key = "xDate",
                 Title = "DateTime"
            };

            CategoryAxis rainDateTimeAxis = new()
            {
                 Position = AxisPosition.Bottom,
                 IsAxisVisible = false,
                 Key = "yBar"
            };

            // -------- Y-Axis Labels --------
            LinearAxis waterLevelAxis = new()
            {
                Position = AxisPosition.Left,
                Minimum = 250,
                Maximum = 350,
                StringFormat = "0.00 cm",
                Title = "Water level in cm",
                Key = "yWaterLevel"
            };

            LinearAxis temperatureAxis = new()
            {
                Position = AxisPosition.Left,
                Minimum = 10,
                Maximum = 30,
                StringFormat = "0.00 \u00b0C",
                Title = "Water level in \u00b0C",
                Key = "yTemperature"
            };

            LinearAxis rainAmountAxis = new()
            {
                Position = AxisPosition.Right,
                Maximum = 60,
                MinimumPadding = 0.06,
                MaximumPadding = 0.06,
                ExtraGridlines = [0d],
                StringFormat = "0.00 mm/m\u00b2",
                Title = "Rain amount in mm/m\u00b2",
                Key = "xRainAmount"
            };

            // -------------- Series --------------

            TwoColorLineSeries waterLevelLine = new()
            {
                ItemsSource = waterLevelDataPoints,
                Limit = river.FloodLevel,
                Color = OxyColors.Blue,
                Color2 = OxyColors.Red,
                XAxisKey = "xDate",
                YAxisKey = "yWaterLevel"
            };

            LineSeries floodWarningLine = new()
            {
                ItemsSource = floodWarningDataPoints,
                LineStyle = LineStyle.Dash,
                Color = OxyColors.DarkRed,
                BrokenLineColor = OxyColors.Automatic,
                XAxisKey = "xDate",
                YAxisKey = "yWaterLevel"
            };

            LineSeries temperatureLine = new()
            {
                ItemsSource = temperatureDataPoints,
                Color = OxyColors.Green,
                XAxisKey = "xDate",
                YAxisKey = "yTemperature"
            };

            BarSeries rainAmountColumns = new()
            {
                ItemsSource = rainAmountDataPoints,
                FillColor = OxyColors.LightBlue,
                XAxisKey = "xRainAmount",
                YAxisKey = "yBar"
            };



            riverPlot = new PlotModel { Title = "Example" };
            // -------- X-Axis Labels --------
            riverPlot.Axes.Add(rainDateTimeAxis);
            riverPlot.Axes.Add(dateTimeAxis);

            // -------- Y-Axis Labels --------
            riverPlot.Axes.Add(rainAmountAxis);
            riverPlot.Axes.Add(waterLevelAxis);
            riverPlot.Axes.Add(temperatureAxis);

            // --------- Plotted Data ---------
            riverPlot.Series.Add(rainAmountColumns);
            riverPlot.Series.Add(floodWarningLine);
            riverPlot.Series.Add(waterLevelLine);
            riverPlot.Series.Add(temperatureLine);
        }

        public PlotModel riverPlot {  get; private set; }

        // temporary testing data
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
