CREATE TABLE [dbo].[MemberChildrenGovernmentPrograms]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [MemberId] INT NOT NULL, 
    [MemberChildrenId] INT NOT NULL, 
    [GovernmentProgramId] INT NOT NULL, 
    [Start] DATETIME2 NOT NULL, 
    [End] DATETIME2 NULL,

	CONSTRAINT [PK_MemberChildrenGovernmentPrograms] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_MemberChildrenGovernmentPrograms_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE NO ACTION, 
	CONSTRAINT [FK_MemberChildrenGovernmentPrograms_MemberChildren] FOREIGN KEY (MemberChildrenId) REFERENCES [MemberChildren](Id) ON DELETE CASCADE, 
	CONSTRAINT [FK_MemberChildrenGovernmentPrograms_GovernmentPrograms] FOREIGN KEY (GovernmentProgramId) REFERENCES [GovernmentPrograms](Id) ON DELETE CASCADE, 
)
