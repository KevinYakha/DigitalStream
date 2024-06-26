﻿CREATE PROCEDURE [dbo].[INSERT_RiverData]
    @RiverId NVARCHAR(250),
    @WaterLevel FLOAT(53),
    @Temperature FLOAT(53),
    @RainAmount FLOAT(53),
    @DateTimeAdded DATETIME
AS
BEGIN
    INSERT INTO [dbo].[RiverData] (RiverId, WaterLevel, Temperature, RainAmount, DateTimeAdded)
    VALUES (@RiverId, @WaterLevel, @Temperature, @RainAmount, @DateTimeAdded);
END
