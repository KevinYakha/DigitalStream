using System.Data.SqlClient;

namespace Models
{
    //this class remains unchanged unless discussed together to avoid inconsistencies in database
    public class River
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //in cm
        public List<double> WaterLevel { get; set; }
        //in degrees c
        public List<double> Temperature { get; set; }
        //in mm
        public List<double> RainAmount { get; set; }
        public double FloodLevel { get; set; }
        public DateTime? LastUpdate { get; set; }  // ? added to make it nullable so DBNull-check is possible (River table in DB: LastUpdate nullable)
    }

    public class RiverData
    {
        public Guid RiverId { get; set; }
        //in cm
        public double WaterLevel { get; set; }
        //in degrees c
        public double Temperature { get; set; }
        //in mm
        public double RainAmount { get; set; }
        public DateTime DateTimeAdded { get; set; }
    }

    public class RiverCreator
    {
        Random rnd = new Random();
        public List<River> CreateRivers(int amountOfRivers)
        {
            List<River> riverList = new List<River>();

            for (int i = amountOfRivers; i > 0; i--)
            {
                River newRiver = new River();
                newRiver.Id = Guid.NewGuid();
                newRiver.Name = $"River Number {(riverList.Count()) + 1}";
                newRiver.WaterLevel = [];
                newRiver.Temperature = [];
                newRiver.RainAmount = [];
                newRiver.FloodLevel = rnd.Next(300, 600);

                //insert River into Database
                SqlConnection conn = new SqlConnection("connstring");
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT_River", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("Id", newRiver.Id);
                    cmd.Parameters.AddWithValue("Name", newRiver.Name);
                    cmd.Parameters.AddWithValue("FloodLevel", newRiver.FloodLevel);
                    cmd.Parameters.AddWithValue("LastUpdate", newRiver.LastUpdate);

                    var result = cmd.ExecuteNonQuery();

                    conn.Close();
                }
                catch (Exception e)
                {
                    conn.Close();
                    Console.WriteLine(e.Message);
                }

                riverList.Add(newRiver);
            }

            return riverList;
        }

        public River UpdateRiver(River river)
        {
            //create RiverData randomly for given river
            RiverData riverData = new RiverData();

            riverData.RiverId = river.Id;

            double WaterLevel = rnd.Next(150, 600);
            riverData.WaterLevel = WaterLevel;

            double Temperature = rnd.Next(0, 60);
            riverData.Temperature = Temperature;

            double RainAmount = rnd.Next(25, 600);
            riverData.RainAmount = RainAmount;

            DateTime DateTimeAdded = DateTime.Now;
            riverData.DateTimeAdded = DateTimeAdded;

            //insert data into database
            SqlConnection conn = new SqlConnection("connstring");
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT_RiverData", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("RiverId", riverData.RiverId);
                cmd.Parameters.AddWithValue("WaterLevel", riverData.WaterLevel);
                cmd.Parameters.AddWithValue("Temperature", riverData.Temperature);
                cmd.Parameters.AddWithValue("RainAmount", riverData.RainAmount);
                cmd.Parameters.AddWithValue("DateTimeAdded", riverData.DateTimeAdded);

                var result = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                conn.Close();
                Console.WriteLine(e.Message);
            }

            //update River obj
            river.WaterLevel.Add(WaterLevel);
            river.Temperature.Add(Temperature);
            river.RainAmount.Add(RainAmount);
            river.LastUpdate = DateTimeAdded;

            return river;
        }
    }
}
