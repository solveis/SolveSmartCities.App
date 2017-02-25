CREATE TABLE [dbo].[CaseNotes]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [MemberId] INT NOT NULL, 
    [CaseManagerId] INT NULL, 
    [CreatedDate] DATETIME2 NOT NULL, 
    [Note] NVARCHAR(MAX) NOT NULL, 
    [OutcomeId] INT NULL, 
    [OutcomeWeight] DECIMAL(18, 4) NULL,

	CONSTRAINT [PK_CaseNotes] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_CaseNotes_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
	CONSTRAINT [FK_CaseNotes_CaseManagers] FOREIGN KEY (CaseManagerId) REFERENCES [CaseManagers](Id) ON DELETE SET NULL, 
	CONSTRAINT [FK_CaseNotes_Outcomes] FOREIGN KEY (OutcomeId) REFERENCES [Outcomes](Id) ON DELETE SET NULL, 
)
