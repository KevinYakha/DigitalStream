using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Visualizer
{
    public class Services
    {


        public async Task<River> GetRiver(string name)
        {
            River river = new River
            {
                Name = name,
            };

            SqlConnection conn = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")); // placeholder

            try
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("GET_RiverByName", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    return null;
                }

                while (await reader.ReadAsync())
                {
                    {
                        river.Id = reader.GetGuid(0);
                        river.FloodLevel = await reader.IsDBNullAsync(2) ? 0 : reader.GetDouble(5);
                        river.LastUpdate = await reader.IsDBNullAsync(3) ? null : reader.GetDateTime(6);
                    };

                }
                await conn.CloseAsync();


                // get riverDataList from GetRiverData and create the lists of river object
                List<RiverData> riverDataList = await GetRiverData(river.Id);
                if (riverDataList != null)
                {
                    river.WaterLevel = riverDataList.Select(i => i.WaterLevel).ToList();
                    river.Temperature = riverDataList.Select(i => i.Temperature).ToList();
                    river.RainAmount = riverDataList.Select(i => i.RainAmount).ToList();
                }

                return river;
            }
            catch (Exception e)
            {
                await conn.CloseAsync();
                return null;
            }

        }


        //get specific river data entries from RiverData table
        public async Task<List<RiverData>> GetRiverData(Guid riverId)
        {
           
            SqlConnection conn = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")); // placeholder

            try
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("GET_RiverDataByRiverId", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(parameterName: "@RiverId", riverId);

                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (!reader.HasRows)
                {
                    return null;
                }

                var riverDataList = new List<RiverData>();
                while (await reader.ReadAsync())
                {
                    riverDataList.Add(new RiverData()
                    {
                        RiverId = riverId,
                        WaterLevel = reader.GetDouble(1),
                        Temperature = reader.GetDouble(2),
                        RainAmount = reader.GetDouble(3),
                        DateTimeAdded = reader.GetDateTime(4)
                    });

                }
                await conn.CloseAsync();

                return riverDataList;
            }
            catch (Exception e)
            {
                await conn.CloseAsync();
                return null;
            }
        }


            public async Task<List<River>> GetAllRivers()
            {
            var rivers = new List<River>();
                SqlConnection conn = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")); // placeholder

                try
                {
                    await conn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("GET_AllRivers", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        return null;
                    }


                    while (await reader.ReadAsync())
                    {
                        rivers.Add(new River()
                        {
                            Id = reader.GetGuid(0),
                            Name = reader.GetString(1),
                            FloodLevel = await reader.IsDBNullAsync(2) ? 0 : reader.GetDouble(2),
                            LastUpdate = await reader.IsDBNullAsync(3) ? null : reader.GetDateTime(3)
                        });

                    }
                    await conn.CloseAsync();


                // get riverDataList from GetRiverData and create the lists of each river object
                foreach (River river in rivers)
                {
                    List<RiverData> riverDataList = await GetRiverData(river.Id);
                    if (riverDataList != null)
                    {
                        river.WaterLevel = riverDataList.Select(i => i.WaterLevel).ToList();
                        river.Temperature = riverDataList.Select(i => i.Temperature).ToList();
                        river.RainAmount = riverDataList.Select(i => i.RainAmount).ToList();
                    }
                }

                    return rivers;
                }
                catch (Exception e)
                {
                    await conn.CloseAsync();
                    return null;
                }

            }


            // get all entries from RiverData-table
            public List<RiverData> GetAllRiverData(List<River> rivers)
            {

                SqlConnection conn = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")); // placeholder

                try
                {
                    conn.Open();
                    var allRiverDataList = new List<RiverData>();
                for (int i = 0; i < rivers.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand("GET_RiverDataByRiverId", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("RiverId", rivers[i].Id);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    reader.Read();
                        allRiverDataList.Add(new RiverData()
                        {
                            RiverId = reader.GetGuid(0),
                            WaterLevel = reader.GetDouble(1),
                            Temperature = reader.GetDouble(2),
                            RainAmount = reader.GetDouble(3),
                            DateTimeAdded = reader.GetDateTime(4)
                        });
                    reader.Close();

                }
                    conn.Close();

                    return allRiverDataList;
                }
                catch (Exception e)
                {
                    conn.Close();
                    return null;
                }
            }
        }

    public class RiverHandler
    {
        Random rnd = new Random();
        public async Task<List<River>> CreateRivers(int amountOfRivers)
        {
            List<River> riverList = new List<River>();

            for (int i = amountOfRivers; i > 0; i--)
            {
                River newRiver = new River();
                newRiver.Id = Guid.NewGuid();
                newRiver.Name = $"River Number {(riverList.Count()) + 1}";
                newRiver.FloodLevel = rnd.Next(300, 600);
                newRiver.LastUpdate = DateTime.Now;

                //insert River into Database
                SqlConnection conn = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
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
                var restult = UpdateRiver(newRiver);
                riverList.Add(newRiver);
            }

            return riverList;
        }

        public async Task<List<River>> UpdateRiver(River river)
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
            SqlConnection conn = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING"));
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

            Services newService = new Services();
            List<River> allRivers = await newService.GetAllRivers();

            return allRivers;
        }
    }
}
