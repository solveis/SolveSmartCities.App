CREATE TABLE [dbo].[NonprofitStaff]
(
	[Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY, 
	[NonprofitId] INT NOT NULL, 
    [CaseManagerId] INT NOT NULL, 
    [ProgramId] INT NULL, 
    [Role] NVARCHAR(50) NULL,

	CONSTRAINT [FK_NonprofitStaff_Nonprofit] FOREIGN KEY ([NonprofitId]) REFERENCES [Nonprofits](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_NonprofitStaff_CaseManager] FOREIGN KEY ([CaseManagerId]) REFERENCES [CaseManagers]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NonprofitStaff_NonprofitPrograms] FOREIGN KEY ([ProgramId]) REFERENCES [NonprofitPrograms]([Id]) ON DELETE NO ACTION
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Maps Nonprofits to CaseManagers', 'SCHEMA', N'dbo', 'TABLE', N'NonprofitStaff', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'i.e. CaseManager, Bookkeeper', 'SCHEMA', N'dbo', 'TABLE', N'NonprofitStaff', 'COLUMN', N'Role'