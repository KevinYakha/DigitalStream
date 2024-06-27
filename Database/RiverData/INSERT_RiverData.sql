CREATE PROCEDURE [dbo].[INSERT_RiverData]
    @RiverId UNIQUEIDENTIFIER,
    @WaterLevel FLOAT(53),
    @Temperature FLOAT(53),
    @RainAmount FLOAT(53),
    @DateTimeAdded DATETIME
AS
BEGIN
    INSERT INTO [dbo].[RiverData] (Id, RiverId, WaterLevel, Temperature, RainAmount, DateTimeAdded)
    VALUES (NEWID(), @RiverId, @WaterLevel, @Temperature, @RainAmount, @DateTimeAdded);
END
