CREATE TABLE [dbo].[CaseNotes]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [MemberId] INT NOT NULL, 
    [CaseManagerId] NVARCHAR(128) NULL, 
    [CreatedDate] DATETIME2 NOT NULL, 
    [Note] NVARCHAR(MAX) NOT NULL, 
    [OutcomeId] INT NULL, 
    [OutcomeWeight] DECIMAL(18, 4) NULL,

	[NonprofitId] INT NULL, 
    CONSTRAINT [PK_CaseNotes] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_CaseNotes_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
	CONSTRAINT [FK_CaseNotes_CaseManagers] FOREIGN KEY (CaseManagerId) REFERENCES [AspNetUsers](Id) ON DELETE SET NULL, 
	CONSTRAINT [FK_CaseNotes_Outcomes] FOREIGN KEY (OutcomeId) REFERENCES [Outcomes](Id) ON DELETE SET NULL, 
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Stores information about CaseNotes', 'SCHEMA', N'dbo', 'TABLE', N'CaseNotes', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Unused - idea is to tie a CaseNote to an Outcome', 'SCHEMA', N'dbo', 'TABLE', N'CaseNotes', 'COLUMN', N'OutcomeId'
GO
EXEC sp_addextendedproperty N'MS_Description', N'Unused - idea is to allow a CaseNote to weight progress toward a goal', 'SCHEMA', N'dbo', 'TABLE', N'CaseNotes', 'COLUMN', N'OutcomeWeight'
