CREATE TABLE [dbo].[MemberSkills]
(
    [MemberId] INT NOT NULL, 
    [SkillId] INT NOT NULL, 
    [NonprofitId] INT NULL

	PRIMARY KEY([MemberId], [SkillId]),
    [IsComplete] BIT NOT NULL DEFAULT ((0)), 
    CONSTRAINT [FK_MemberSkills_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_MemberSkills_Skills] FOREIGN KEY (SkillId) REFERENCES [Skills]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MemberSkills_Nonprofits] FOREIGN KEY (NonprofitId) REFERENCES [Nonprofits]([Id]) ON DELETE SET NULL
)
