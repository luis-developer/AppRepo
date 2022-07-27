CREATE TABLE [dbo].[Client]
(
	[ClientId] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [EmailAddress] NVARCHAR(100) NULL, 
    [Birthday] DATE NULL
)
