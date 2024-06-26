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
        public DateTime LastUpdate { get; set; }
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


    //the class below is just an example and needs to be changed
    /*public class DataUpdater
    {
        //logic for updating WaterLevel, Temperature, RainAmount every 15 min in river object
        private River river;
        public DataUpdater(River river) 
        {
            this.river = river;
        }

        public void Update() 
        { }*/

}
