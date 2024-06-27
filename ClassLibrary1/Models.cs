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
}
