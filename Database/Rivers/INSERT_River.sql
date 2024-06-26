CREATE PROCEDURE [dbo].[INSERT_River]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @WaterLevel FLOAT(53),
    @Temperature FLOAT(53),
    @RainAmount FLOAT(53),
    @FloodLevel FLOAT(53),
    @LastUpdate DATETIME
AS
BEGIN

INSERT INTO Rivers
        (Id, Name, WaterLevel, Temperature, RainAmount, FloodLevel, LastUpdate)
        VALUES
        (@Id, @Name, @WaterLevel, @Temperature, @RainAmount, @FloodLevel, @LastUpdate)
END
