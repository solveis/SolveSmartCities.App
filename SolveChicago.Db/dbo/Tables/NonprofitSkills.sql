CREATE TABLE [dbo].[NonprofitSkills]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[NonprofitId] INT NOT NULL, 
    [SkillId] INT NOT NULL,
    [ProgramId] INT NULL, 
    CONSTRAINT [FK_NonprofitSkills_Nonprofit] FOREIGN KEY (NonprofitId) REFERENCES [Nonprofits](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_NonprofitSkills_Skill] FOREIGN KEY (SkillId) REFERENCES [Skills]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NonprofitSkills_NonprofitProgram] FOREIGN KEY (ProgramId) REFERENCES [NonprofitPrograms]([Id]) ON DELETE NO ACTION,
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Maps Nonprofits to Skills', 'SCHEMA', N'dbo', 'TABLE', N'NonprofitSkills', NULL, NULL