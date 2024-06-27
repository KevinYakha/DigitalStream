using System.Data.SqlClient;

namespace Visualizer
{
    internal class MainViewModel
    {
        public MainViewModel()
        {
            SqlConnectionStringBuilder builder = new()
            {
                DataSource = "WIN-5NF47SRRT0I",
                InitialCatalog = "RiverData",
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
