CREATE TABLE [dbo].[Outcomes]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [MemberId] INT NOT NULL,
    [Name] NVARCHAR(128) NOT NULL, 

	CONSTRAINT [PK_Outcomes] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_Outcomes_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE NO ACTION, 
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Stores information about Outcomes', 'SCHEMA', N'dbo', 'TABLE', N'Outcomes', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Member that Outcome is attached to', 'SCHEMA', N'dbo', 'TABLE', N'Outcomes', 'COLUMN', N'MemberId'