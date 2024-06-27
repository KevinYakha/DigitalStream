using System.Data.SqlClient;

namespace Visualizer
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = "WIN-5EEDLGC0U7J",
                InitialCatalog = "RiverData",
                IntegratedSecurity = true,
                Encrypt = true,
                TrustServerCertificate = true
            };
            Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", builder.ConnectionString);

            chartViewModel = new ChartViewModel();
            buttonsViewModel = new ButtonsViewModel();
        }

        public ChartViewModel chartViewModel { get; private set; }
        public ButtonsViewModel buttonsViewModel { get; private set; }
    }
}
