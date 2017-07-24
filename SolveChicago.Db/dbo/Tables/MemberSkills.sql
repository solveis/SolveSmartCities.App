CREATE TABLE [dbo].[MemberSkills]
(
    [MemberId] INT NOT NULL, 
    [SkillId] INT NOT NULL, 
    [NonprofitSkillsId] INT NULL

	PRIMARY KEY([MemberId], [SkillId]),
    [IsComplete] BIT NOT NULL DEFAULT ((0)), 
    CONSTRAINT [FK_MemberSkills_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_MemberSkills_Skills] FOREIGN KEY (SkillId) REFERENCES [Skills]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MemberSkills_NonprofitSkills] FOREIGN KEY ([NonprofitSkillsId]) REFERENCES [NonprofitSkills]([Id]) ON DELETE NO ACTION
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Maps Members to Skills', 'SCHEMA', N'dbo', 'TABLE', N'MemberSkills', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'1 = Complete, 0 = Desired', 'SCHEMA', N'dbo', 'TABLE', N'MemberSkills', 'COLUMN', N'IsComplete'
GO
EXEC sp_addextendedproperty N'MS_Description', N'FK to the NonprofitSkills mapping table', 'SCHEMA', N'dbo', 'TABLE', N'MemberSkills', 'COLUMN', N'NonprofitSkillsId'