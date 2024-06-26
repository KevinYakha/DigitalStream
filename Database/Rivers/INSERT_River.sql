CREATE PROCEDURE [dbo].[INSERT_River]
    @Id UNIQUEIDENTIFIER,
    @Name NVARCHAR(100),
    @FloodLevel FLOAT(53),
    @LastUpdate DATETIME
AS
BEGIN

INSERT INTO Rivers
        (Id, Name, FloodLevel, LastUpdate)
        VALUES
        (@Id, @Name, @FloodLevel, @LastUpdate)
END
