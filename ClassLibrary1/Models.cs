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


    //the class below is just an example and needs to be changed
    public class DataUpdater
    {
        //logic for updating WaterLevel, Temperature, RainAmount every 15 min in river object
        private River river;
        public DataUpdater(River river)
        {
            this.river = river;
        }

        public void Update()
        { }
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
                newRiver.WaterLevel.Add(0);
                newRiver.Temperature.Add(0);
                newRiver.RainAmount.Add(0);
                newRiver.FloodLevel = rnd.Next(300, 600);

                //insert River into Database

                riverList.Add(newRiver);
            }

            return riverList;
        }

        public void UpdateRiver(River river)
        {
            //create RiverData randomly for given river
            Guid riverID = river.Id;
            double WaterLevel = rnd.Next(150, 600);
            double Temperature = rnd.Next(0, 60);
            double RainAmount = rnd.Next(25, 600);
            DateTime DateTimeAdded = DateTime.Now;

            //insert data into database
            //-----------------------------

            //update River obj
            river.WaterLevel.Add(WaterLevel);
            river.Temperature.Add(Temperature);
            river.RainAmount.Add(RainAmount);
            river.LastUpdate = DateTimeAdded;
        }
    }
}
