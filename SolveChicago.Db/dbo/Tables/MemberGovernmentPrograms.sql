CREATE TABLE [dbo].[MemberGovernmentPrograms]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [MemberId] INT NOT NULL, 
    [GovernmentProgramId] INT NOT NULL, 
    [Start] DATETIME2 NOT NULL, 
    [End] DATETIME2 NULL,

	CONSTRAINT [PK_MemberGovernmentPrograms] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_MemberGovernmentPrograms_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
	CONSTRAINT [FK_MemberGovernmentPrograms_GovernmentPrograms] FOREIGN KEY (GovernmentProgramId) REFERENCES [GovernmentPrograms](Id) ON DELETE CASCADE, 
)
