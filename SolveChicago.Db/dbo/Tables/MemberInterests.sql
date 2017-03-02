CREATE TABLE [dbo].[MemberInterests]
(
    [MemberId] INT NOT NULL, 
    [InterestId] INT NOT NULL,

	
	PRIMARY KEY ([MemberId], [InterestId]), 
    CONSTRAINT [FK_MemberInterests_Member] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_MemberInterests_Interest] FOREIGN KEY (InterestId) REFERENCES [Interests]([Id]) ON DELETE CASCADE
)
