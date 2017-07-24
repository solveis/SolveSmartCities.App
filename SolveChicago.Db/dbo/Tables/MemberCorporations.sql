CREATE TABLE [dbo].[MemberCorporations]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[MemberId] INT NOT NULL, 
    [CorporationId] INT NOT NULL, 
	[Start] DATETIME2 NOT NULL, 
    [End] DATETIME2 NULL, 
    [ReasonForLeaving] NVARCHAR(MAX) NULL, 
    [Pay] DECIMAL(18, 2) NULL, 
    [NonprofitId] INT NULL, 
    [IsMemberConfirmed] BIT NULL , 
    CONSTRAINT [FK_MemberCorporations_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_MemberCorporations_Corporations] FOREIGN KEY (CorporationId) REFERENCES [Corporations]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MemberCorporations_Nonprofits] FOREIGN KEY (NonprofitId) REFERENCES [Nonprofits]([Id]) ON DELETE SET NULL
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Maps Members to Corporations', 'SCHEMA', N'dbo', 'TABLE', N'MemberCorporations', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Member must confirm job for it to count in Impact', 'SCHEMA', N'dbo', 'TABLE', N'MemberCorporations', 'COLUMN', N'IsMemberConfirmed'
GO
EXEC sp_addextendedproperty N'MS_Description', N'The Nonprofit that placed the Job', 'SCHEMA', N'dbo', 'TABLE', N'MemberCorporations', 'COLUMN', N'NonprofitId'