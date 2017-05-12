CREATE TABLE [dbo].[NonprofitSkills]
(
	[NonprofitId] INT NOT NULL, 
    [SkillId] INT NOT NULL,
	PRIMARY KEY ([NonprofitId], [SkillId]), 
    CONSTRAINT [FK_NonprofitSkills_Nonprofit] FOREIGN KEY (NonprofitId) REFERENCES [Nonprofits](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_NonprofitSkills_Skill] FOREIGN KEY (SkillId) REFERENCES [Skills]([Id]) ON DELETE CASCADE
)
