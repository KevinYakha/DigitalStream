using System.Data.SqlClient;

namespace Visualizer
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = "DESKTOP-CUM9PGM",
                InitialCatalog = "RiverDatabase",
                IntegratedSecurity = true,
                Encrypt = true,
                TrustServerCertificate = true
            };
            Environment.SetEnvironmentVariable("DB_CONNECTION_STRING", builder.ConnectionString);
            chartViewModel = new();
            buttonsViewModel = new();
        }

        ChartViewModel chartViewModel;
        ButtonsViewModel buttonsViewModel;
    }
}
