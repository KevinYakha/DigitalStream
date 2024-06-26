CREATE TABLE [dbo].[RiverData] (
    [RiverId] UNIQUEIDENTIFIER NOT NULL,
    [WaterLevel] FLOAT(53) NOT NULL,
    [Temperature] FLOAT(53) NOT NULL,
    [RainAmount] FLOAT(53) NOT NULL,
    [DateTimeAdded] DATETIME NOT NULL,
    CONSTRAINT PK_RiverData PRIMARY KEY CLUSTERED ([DateTimeAdded] ASC),
    CONSTRAINT FK_RiverData_Rivers FOREIGN KEY ([RiverId]) REFERENCES [dbo].[Rivers]([Id])
);
