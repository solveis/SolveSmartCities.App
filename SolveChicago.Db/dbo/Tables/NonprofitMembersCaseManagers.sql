CREATE TABLE [dbo].[NonprofitMembersCaseManagers]
(
	[NonprofitMembersId] INT NOT NULL,
	[CaseManagerId] INT NOT NULL,
	
    PRIMARY KEY([NonprofitMembersId], [CaseManagerId]),
    CONSTRAINT [FK_NonprofitMembersCaseManagers_NonprofitMembers] FOREIGN KEY (NonprofitMembersId) REFERENCES [NonprofitMembers](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_NonprofitMembersCaseManagers_CaseManagers] FOREIGN KEY (CaseManagerId) REFERENCES [CaseManagers]([Id]) ON DELETE CASCADE,
)
