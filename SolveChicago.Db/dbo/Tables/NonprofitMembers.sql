CREATE TABLE [dbo].[NonprofitMembers]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[MemberId] INT NOT NULL, 
    [NonprofitId] INT NOT NULL, 
	[ProgramId] INT NULL,
	[MemberEnjoyed] NVARCHAR(MAX) NULL, 
    [MemberStruggled] NVARCHAR(MAX) NULL, 
    [Start] DATETIME2 NOT NULL, 
    [End] DATETIME2 NULL, 
    PRIMARY KEY([Id]),
    CONSTRAINT [FK_NonprofitMembers_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_NonprofitMembers_Nonprofits] FOREIGN KEY (NonprofitId) REFERENCES [Nonprofits]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NonprofitMembers_NonprofitPrograms] FOREIGN KEY ([ProgramId]) REFERENCES [NonprofitPrograms]([Id]) ON DELETE NO ACTION,
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Maps Nonprofits to Members', 'SCHEMA', N'dbo', 'TABLE', N'NonprofitMembers', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'The Nonprofit Program the Member belongs to, if any', 'SCHEMA', N'dbo', 'TABLE', N'NonprofitMembers', 'COLUMN', N'ProgramId'