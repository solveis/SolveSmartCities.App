CREATE TABLE [dbo].[MemberCaseManagers]
(
    [MemberId] INT NOT NULL,
	[CaseManagerId] INT NOT NULL, 
	[Start] DATETIME2 NOT NULL, 
    [End] DATETIME2 NULL, 

    PRIMARY KEY ([MemberId], [CaseManagerId]), 
    CONSTRAINT [FK_MembersCaseManagers_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_MembersCaseManagers_CaseManagers] FOREIGN KEY (CaseManagerId) REFERENCES [CaseManagers]([Id]) ON DELETE CASCADE
)

