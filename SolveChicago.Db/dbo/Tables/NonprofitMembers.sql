CREATE TABLE [dbo].[NonprofitMembers]
(
	[MemberId] INT NOT NULL, 
    [NonprofitId] INT NOT NULL, 
	[CaseManagerId] INT NULL,
	[MemberEnjoyed] NVARCHAR(MAX) NULL, 
    [MemberStruggled] NVARCHAR(MAX) NULL, 

    PRIMARY KEY([MemberId], [NonprofitId]),
    CONSTRAINT [FK_NonprofitMembers_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_NonprofitMembers_Nonprofits] FOREIGN KEY (NonprofitId) REFERENCES [Nonprofits]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NonprofitMembers_CaseManagers] FOREIGN KEY (CaseManagerId) REFERENCES [CaseManagers]([Id]) ON DELETE CASCADE
)
