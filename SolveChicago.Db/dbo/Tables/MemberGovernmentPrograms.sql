CREATE TABLE [dbo].[MemberGovernmentPrograms]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [MemberId] INT NOT NULL, 
    [GovernmentProgramId] INT NOT NULL, 
    [Start] DATETIME2 NOT NULL, 
    [End] DATETIME2 NULL,

	[Amount] MONEY NULL, 
    CONSTRAINT [PK_MemberGovernmentPrograms] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_MemberGovernmentPrograms_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
	CONSTRAINT [FK_MemberGovernmentPrograms_GovernmentPrograms] FOREIGN KEY (GovernmentProgramId) REFERENCES [GovernmentPrograms](Id) ON DELETE CASCADE, 
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Maps Members to GovernmentPrograms', 'SCHEMA', N'dbo', 'TABLE', N'MemberGovernmentPrograms', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Amount received', 'SCHEMA', N'dbo', 'TABLE', N'MemberGovernmentPrograms', 'COLUMN', N'Amount'