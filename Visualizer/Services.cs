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
                        river.FloodLevel = await reader.IsDBNullAsync(6) ? 0 : reader.GetDouble(5);
                        river.LastUpdate = await reader.IsDBNullAsync(7) ? null : reader.GetDateTime(6);
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
            RiverData riverData = new RiverData
            {
                RiverId = riverId
            };


            SqlConnection conn = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")); // placeholder

            try
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("GET_RiverDataByRiverId", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

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



            public async Task<List<River>> GetAllRivers()
            {
                var rivers = new List<River>;
                SqlConnection conn = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")); // placeholder

                try
                {
                    await conn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("GET_AllRivers", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

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
                            FloodLevel = await reader.IsDBNullAsync(6) ? 0 : reader.GetDouble(5),
                            LastUpdate = await reader.IsDBNullAsync(7) ? null : reader.GetDateTime(6)
                        });

                    }
                    await conn.CloseAsync();


                    // get allRiverDataList from GetRiverData and create the lists of river object
                    List<RiverData> allRiverDataList = await GetRiverData();
                    if (allRiverDataList != null)
                    {
                        foreach (river in rivers)
                        {
                            var filteredData = allRiverDataList.Where(i => i.RiverId == river.Id).ToList();
                            river.WaterLevel = filteredData.Select(i => i.WaterLevel).ToList();
                            river.Temperature = filteredData.Select(i => i.Temperature).ToList();
                            river.RainAmount = filteredData.Select(i => i.RainAmount).ToList();
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
            public async Task<List<RiverData>> GetAllRiverData()
            {

                SqlConnection conn = new SqlConnection(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")); // placeholder

                try
                {
                    await conn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("GET_RiverDataByRiverId", conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    if (!reader.HasRows)
                    {
                        return null;
                    }

                    var allRiverDataList = new List<RiverData>();
                    while (await reader.ReadAsync())
                    {
                        riverDataList.Add(new RiverData()
                        {
                            RiverId = reader.GetGuid(0),
                            WaterLevel = reader.GetDouble(1),
                            Temperature = reader.GetDouble(2),
                            RainAmount = reader.GetDouble(3),
                            DateTimeAdded = reader.GetDateTime(4)
                        });

                    }
                    await conn.CloseAsync();

                    return allRiverDataList;
                }
                catch (Exception e)
                {
                    await conn.CloseAsync();
                    return null;
                }
            }
        }
    }
