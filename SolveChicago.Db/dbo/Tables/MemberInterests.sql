CREATE TABLE [dbo].[MemberInterests]
(
    [MemberId] INT NOT NULL, 
    [InterestId] INT NOT NULL,

	
	PRIMARY KEY ([MemberId], [InterestId]), 
    CONSTRAINT [FK_MemberInterests_Member] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_MemberInterests_Interest] FOREIGN KEY (InterestId) REFERENCES [Interests]([Id]) ON DELETE CASCADE
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Maps Members to Interests', 'SCHEMA', N'dbo', 'TABLE', N'MemberInterests', NULL, NULL
