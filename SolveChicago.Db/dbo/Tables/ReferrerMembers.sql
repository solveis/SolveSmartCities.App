CREATE TABLE [dbo].[ReferrerMembers]
(
	[MemberId] INT NOT NULL, 
    [ReferrerId] INT NOT NULL,

	PRIMARY KEY ([ReferrerId], [MemberId]), 
    CONSTRAINT [FK_ReferrerMembers_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_ReferrerMembers_Referrer] FOREIGN KEY (ReferrerId) REFERENCES [Referrers]([Id]) ON DELETE CASCADE
)
