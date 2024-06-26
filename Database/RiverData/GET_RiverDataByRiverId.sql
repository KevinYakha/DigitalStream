CREATE PROCEDURE [dbo].[GET_RiverDataByRiverId]
    @RiverId NVARCHAR(250)
AS
BEGIN
    SELECT RiverId, WaterLevel, Temperature, RainAmount, DateTimeAdded
    FROM RiverData
    WHERE RiverId = @RiverId;
END