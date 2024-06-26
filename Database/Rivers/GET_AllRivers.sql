CREATE PROCEDURE [dbo].[GET_AllRivers]
 AS
BEGIN
    SELECT Id, Name, WaterLevel, Temperature, RainAmount, FloodLevel, LastUpdate
    FROM Rivers
END