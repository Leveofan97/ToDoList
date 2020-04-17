CREATE TABLE [dbo].[Item]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [Description] NVARCHAR(MAX) NOT NULL DEFAULT '', 
    [IsDone] BIT NOT NULL DEFAULT 0
)

INSERT INTO [dbo].[Item] ([Description]) VALUES
('test 1'),('test 2'),('test 3')

SELECT * FROM [dbo].[Item] 