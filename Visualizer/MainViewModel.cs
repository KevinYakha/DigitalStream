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
            rivers = riverCreator.CreateRivers(numberOfRivers);

            for (int j = 0; j < 30; j++)
            {
                for (int i = 0; i < numberOfRivers; i++)
                {
                    riverCreator.UpdateRiver(rivers[i]);
                }
            }

            for (int i = 0; i < rivers[0].WaterLevel.Count; i++)
            {
                dateTimes.Add(DateTime.Now.AddMinutes(-15 * dateTimes.Count));
            }
            dateTimes.Reverse();

            // --------------- Data ---------------

            List<DataPoint> waterLevelDataPoints = [];
            for (int i = 0; i < rivers[0].WaterLevel.Count; i++)
            {
                waterLevelDataPoints.Add(new(DateTimeAxis.ToDouble(dateTimes[i]), rivers[0].WaterLevel[i]));
            }

            List<DataPoint> temperatureDataPoints = [];
            for (int i = 0; i < rivers[0].Temperature.Count; i++)
            {
                temperatureDataPoints.Add(new(DateTimeAxis.ToDouble(dateTimes[i]), rivers[0].Temperature[i]));
            }

            List<DataPoint> floodWarningDataPoints =
            [
                new(DateTimeAxis.ToDouble(dateTimes[0]), rivers[0].FloodLevel),
                new(DateTimeAxis.ToDouble(dateTimes[^1]), rivers[0].FloodLevel)
            ];

            List<BarItem> rainAmountDataPoints = [];
            for (int i = 0; i < rivers[0].WaterLevel.Count; i++)
            {
                rainAmountDataPoints.Add(new(rivers[0].RainAmount[i]));
            }

            // --------------- Axes ---------------

            // -------- X-Axis Labels --------
            DateTimeAxis dateTimeAxis = new()
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                StringFormat = "dd/MM HH:mm",
                Key = "xDate",
                Title = "DateTime"
            };

            CategoryAxis rainDateTimeAxis = new()
            {
                Position = AxisPosition.Bottom,
                ItemsSource = dateTimes,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                StringFormat = "dd/MM HH:mm",
                IsAxisVisible = false,
                Key = "yBar"
            };

            // -------- Y-Axis Labels --------
            LinearAxis waterLevelAxis = new()
            {
                Position = AxisPosition.Left,
                AxislineColor = OxyColors.Blue,
                TextColor = OxyColors.Blue,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                StringFormat = "0.00 cm",
                Title = "Water level in cm",
                Key = "yWaterLevel"
            };

            LinearAxis temperatureAxis = new()
            {
                Position = AxisPosition.Left,
                PositionTier = 1,
                AxislineColor = OxyColors.Green,
                TextColor = OxyColors.Green,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                StringFormat = "0.00 \u00b0C",
                Title = "Temperature in \u00b0C",
                Key = "yTemperature"
            };

            LinearAxis rainAmountAxis = new()
            {
                Position = AxisPosition.Right,
                MinimumPadding = 0.06,
                MaximumPadding = 0.06,
                ExtraGridlines = [0d],
                AxislineColor = OxyColors.LightBlue,
                TextColor = OxyColors.LightBlue,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                StringFormat = "0.00 mm/m\u00b2",
                Title = "Rain amount in mm/m\u00b2",
                Key = "xRainAmount"
            };

            // -------------- Series --------------

            TwoColorLineSeries waterLevelLine = new()
            {
                ItemsSource = waterLevelDataPoints,
                Limit = rivers[0].FloodLevel,
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
                LabelPlacement = LabelPlacement.Inside,
                LabelFormatString = "{0:.00} mm/m\u00b2",
                XAxisKey = "xRainAmount",
                YAxisKey = "yBar"
            };



            riverPlot = new PlotModel { Title = rivers[0].Name };
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

        int numberOfRivers = 5;
        RiverCreator riverCreator = new RiverCreator();
        List<River> rivers = new List<River>();

        List<DateTime> dateTimes = new();
    }
}
