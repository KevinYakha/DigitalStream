using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Models;

namespace Visualizer
{
    class ChartViewModel
    {
        public ChartViewModel()
        {
            // Retrieves all rivers and displays the first in the list
            riverCount = 0;

            services = new();
            riverHandler = new();

            dateTimes = new();
            rivers = new();

            rainAmountDataPoints = new();
            floodWarningDataPoints = new();
            temperatureDataPoints = new();
            waterLevelDataPoints = new();

            riverPlot = new();

            UpdateChart(0);
        }

        public static async void UpdateChart(int riverIndex)
        {
            // Uncomment to populate Database
            /*
            if (rivers.Count != 0)
            {
                await riverHandler.CreateRivers(3);
                for (int i = 0; i < rivers.Count; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        await riverHandler.UpdateRiver(rivers[i]);
                    }
                }
            }
            */

            rivers = await services.GetAllRivers();
            riverCount = rivers.Count;
            riverPlot.Title = rivers[riverIndex].Name;

            UpdateDataPoints(riverIndex);
            UpdateAxes();
            UpdateSeries(riverIndex);

            riverPlot.InvalidatePlot(true);

            riverPlot.Title = rivers[0].Name;
        }

        public static void GenerateDataPoint(int riverIndex)
        {
            Task.Run(async () => await riverHandler.UpdateRiver(rivers[riverIndex]));
        }

        private static void UpdateDataPoints(int riverIndex)
        {
            dateTimes.Clear();
            int highestListCount = rivers[riverIndex].WaterLevel.Count;
            if (highestListCount < rivers[riverIndex].Temperature.Count)
            {
                highestListCount = rivers[riverIndex].Temperature.Count;
            }
            if (highestListCount < rivers[riverIndex].RainAmount.Count)
            {
                highestListCount = rivers[riverIndex].RainAmount.Count;
            }

            for (int i = 0; i < highestListCount; i++)
            {
                dateTimes.Add(DateTime.Now.AddMinutes(-15 * dateTimes.Count));
            }
            dateTimes.Reverse();

            waterLevelDataPoints = new();
            for (int i = 0; i < rivers[riverIndex].WaterLevel.Count; i++)
            {
                waterLevelDataPoints.Add(new(DateTimeAxis.ToDouble(dateTimes[i]), rivers[riverIndex].WaterLevel[i]));
            }

            temperatureDataPoints = new();
            for (int i = 0; i < rivers[riverIndex].Temperature.Count; i++)
            {
                temperatureDataPoints.Add(new(DateTimeAxis.ToDouble(dateTimes[i]), rivers[riverIndex].Temperature[i]));
            }

            floodWarningDataPoints = new()
            {
                new(DateTimeAxis.ToDouble(dateTimes[0]), rivers[riverIndex].FloodLevel),
                new(DateTimeAxis.ToDouble(dateTimes[^1]), rivers[riverIndex].FloodLevel),
            };

            rainAmountDataPoints = new();
            for (int i = 0; i < rivers[riverIndex].WaterLevel.Count; i++)
            {
                rainAmountDataPoints.Add(new(rivers[riverIndex].RainAmount[i]));
            }
        }

        private static void UpdateAxes()
        {
            riverPlot.Axes.Clear();

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

            // -------- X-Axis Labels --------
            riverPlot.Axes.Add(rainDateTimeAxis);
            riverPlot.Axes.Add(dateTimeAxis);

            // -------- Y-Axis Labels --------
            riverPlot.Axes.Add(rainAmountAxis);
            riverPlot.Axes.Add(waterLevelAxis);
            riverPlot.Axes.Add(temperatureAxis);
        }

        private static void UpdateSeries(int riverIndex)
        {
            riverPlot.Series.Clear();

            TwoColorLineSeries waterLevelLine = new()
            {
                ItemsSource = waterLevelDataPoints,
                Limit = rivers[riverIndex].FloodLevel,
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

            // --------- Plotted Data ---------
            riverPlot.Series.Add(rainAmountColumns);
            riverPlot.Series.Add(floodWarningLine);
            riverPlot.Series.Add(waterLevelLine);
            riverPlot.Series.Add(temperatureLine);
        }

        public static PlotModel riverPlot { get; private set; }
        public static int riverCount { get; private set; }

        private static Services services { get; set; }
        private static RiverHandler riverHandler { get; set; }

        private static List<DateTime> dateTimes { get; set; }
        private static List<River> rivers { get; set; }

        private static List<BarItem> rainAmountDataPoints { get; set; }
        private static List<DataPoint> floodWarningDataPoints { get; set; }
        private static List<DataPoint> temperatureDataPoints { get; set; }
        private static List<DataPoint> waterLevelDataPoints { get; set; }
    }
}
