CREATE PROCEDURE [dbo].[GET_AllRivers]
 AS
BEGIN
    SELECT Id, Name, FloodLevel, LastUpdate
    FROM Rivers
END