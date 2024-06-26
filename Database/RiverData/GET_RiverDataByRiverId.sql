CREATE PROCEDURE [dbo].[GET_RiverDataByRiverId]
    @RiverId UNIQUEIDENTIFIER
AS
BEGIN
    SELECT RiverId, WaterLevel, Temperature, RainAmount, DateTimeAdded
    FROM RiverData
    WHERE RiverId = @RiverId;
END