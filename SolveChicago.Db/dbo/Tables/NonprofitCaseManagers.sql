CREATE TABLE [dbo].[NonprofitCaseManagers]
(
	[NonprofitId] INT NOT NULL, 
    [CaseManagerId] INT NOT NULL, 

	PRIMARY KEY([NonprofitId], [CaseManagerId]),
    CONSTRAINT [FK_NonprofitCaseManagers_Nonprofits] FOREIGN KEY (NonprofitId) REFERENCES [Nonprofits](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_NonprofitCaseManagers_CaseManagers] FOREIGN KEY (CaseManagerId) REFERENCES [CaseManagers]([Id]) ON DELETE CASCADE
)
