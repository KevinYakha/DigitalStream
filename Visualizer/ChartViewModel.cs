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
            riverHandler.UpdateRiver();
            riverCount = rivers.Count;
            UpdateChart(0);
        }

        public static void UpdateChart(int riverIndex)
        {
            rivers = riverHandler.UpdateRiver();
            riverCount = rivers.Count;
            riverPlot.Title = rivers[riverIndex].Name;

            UpdateDataPoints(riverIndex);
            UpdateAxes(riverIndex);
            UpdateSeries(riverIndex);

            riverPlot.InvalidatePlot(true);
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

            waterLevelDataPoints.Clear();
            for (int i = 0; i < rivers[riverIndex].WaterLevel.Count; i++)
            {
                waterLevelDataPoints.Add(new(DateTimeAxis.ToDouble(dateTimes[i]), rivers[riverIndex].WaterLevel[i]));
            }

            temperatureDataPoints.Clear();
            for (int i = 0; i < rivers[riverIndex].Temperature.Count; i++)
            {
                temperatureDataPoints.Add(new(DateTimeAxis.ToDouble(dateTimes[i]), rivers[riverIndex].Temperature[i]));
            }

            floodWarningDataPoints.Clear();
            floodWarningDataPoints.Add(new(DateTimeAxis.ToDouble(dateTimes[0]), rivers[riverIndex].FloodLevel));
            floodWarningDataPoints.Add(new(DateTimeAxis.ToDouble(dateTimes[^1]), rivers[riverIndex].FloodLevel));

            rainAmountDataPoints.Clear();
            for (int i = 0; i < rivers[riverIndex].WaterLevel.Count; i++)
            {
                rainAmountDataPoints.Add(new(rivers[riverIndex].RainAmount[i]));
            }
        }

        private static void UpdateAxes(int riverIndex)
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

            riverPlot.Series.Add(rainAmountColumns);
            riverPlot.Series.Add(floodWarningLine);
            riverPlot.Series.Add(waterLevelLine);
            riverPlot.Series.Add(temperatureLine);
        }
        
        private static PlotModel riverPlot = new();
        private static RiverHandler riverHandler = new();
        private static List<River> rivers = [];
        private static int numberOfRivers = 5;
        private static List<DateTime> dateTimes = [];
        private static List<DataPoint> waterLevelDataPoints = [];
        private static List<DataPoint> temperatureDataPoints = [];
        private static List<DataPoint> floodWarningDataPoints = [];
        private static List<BarItem> rainAmountDataPoints = [];

        public static int riverCount = rivers.Count;
    }
}
