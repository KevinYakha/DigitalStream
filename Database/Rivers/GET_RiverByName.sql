CREATE PROCEDURE [dbo].[GET_RiverByName]
@Name NVARCHAR(250)
 AS
BEGIN
    SELECT Id, Name, WaterLevel, Temperature, RainAmount, FloodLevel, LastUpdate
    FROM Rivers
     WHERE Name = @Name;
END