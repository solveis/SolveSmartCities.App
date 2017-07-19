CREATE TABLE [dbo].[Communications]
(
	[Id] INT IDENTITY (1, 1) NOT NULL,
    [Type] NVARCHAR(256) NULL, 
    [Date] DATETIME2 NULL, 
    [UserId] NVARCHAR(128) NULL, 
    [Success] BIT NULL, 
    [HtmlContent] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Communications] PRIMARY KEY (Id), 
	CONSTRAINT [FK_Communications_AspNetUsers] FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Stores information about Communications', 'SCHEMA', N'dbo', 'TABLE', N'Communications', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Unused - idea is to store content of message here for auditing', 'SCHEMA', N'dbo', 'TABLE', N'Communications', 'COLUMN', N'HtmlContent'

