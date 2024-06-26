

public class Services
{

	//get river details
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


			// get riverDataList from GetRiverData and fill the lists of river object
			List<RiverData> riverDataList = await GetRiverData(river.Id);

			river.WaterLevel = new List<double>(); foreach (var i in riverDataList) { river.WaterLevel.Add(i.WaterLevel); };
			river.Temperature = new List<double>(); foreach (var i in riverDataList) { river.Temperature.Add(i.Temperature); };
			river.RainAmount = new List<double>(); foreach (var i in riverDataList) { river.RainAmount.Add(i.RainAmount); };

			return river;
		}
		catch (Exception e)
		{
			await conn.CloseAsync();
			return null;
		}

	}


	//get river data entries from RiverData table
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



	}
}