CREATE PROCEDURE [dbo].[INSERT_RiverData]
    @RiverId INT,
    @WaterLevel FLOAT(53),
    @Temperature FLOAT(53),
    @RainAmount FLOAT(53),
    @DateTimeAdded DATETIME
AS
BEGIN
    INSERT INTO [dbo].[RiverData] (RiverId, WaterLevel, Temperature, RainAmount, DateTimeAdded)
    VALUES (@RiverId, @WaterLevel, @Temperature, @RainAmount, @DateTimeAdded);
END
