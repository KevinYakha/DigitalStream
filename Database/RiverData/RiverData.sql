CREATE TABLE [dbo].[RiverData] (
    [Id] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL,
    [RiverId] UNIQUEIDENTIFIER NOT NULL,
    [WaterLevel] FLOAT(53) NOT NULL,
    [Temperature] FLOAT(53) NOT NULL,
    [RainAmount] FLOAT(53) NOT NULL,
    [DateTimeAdded] DATETIME NOT NULL,
    CONSTRAINT FK_RiverData_Rivers FOREIGN KEY ([RiverId]) REFERENCES [dbo].[Rivers]([Id])
);
