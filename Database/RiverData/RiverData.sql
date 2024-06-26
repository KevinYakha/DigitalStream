CREATE TABLE [dbo].[RiverData] (
    [RiverId] NVARCHAR(250) NOT NULL,
    [WaterLevel] FLOAT(53) NOT NULL,
    [Temperature] FLOAT(53) NOT NULL,
    [RainAmount] FLOAT(53) NOT NULL,
    [DateTimeAdded] DATETIME NOT NULL,
    CONSTRAINT PK_RiverData PRIMARY KEY CLUSTERED ([DateTimeAdded] ASC)
);
