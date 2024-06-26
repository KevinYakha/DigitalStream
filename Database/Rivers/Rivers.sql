CREATE TABLE [dbo].[Rivers] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (100)   NULL,
    [WaterLevel]  FLOAT (53)       NULL,
    [Temperature] FLOAT (53)       NULL,
    [RainAmount]  FLOAT (53)       NULL,
    [FloodLevel]  FLOAT (53)       NULL,
    [LastUpdate]  DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

