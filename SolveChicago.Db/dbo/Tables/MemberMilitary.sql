CREATE TABLE [dbo].[MemberMilitary]
(
	[MemberId] INT  NOT NULL, 
    [MilitaryId] INT NOT NULL,
	[Start] DATETIME2 NULL, 
    [End] DATETIME2 NULL, 
    [LastPayGrade] NVARCHAR(4) NULL, 
    [CurrentStatus] NVARCHAR(50) NULL, 
    PRIMARY KEY ([MemberId], [MilitaryId]), 
    CONSTRAINT [FK_MemberMilitary_Member] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_MemberMilitary_MilitaryBranch] FOREIGN KEY (MilitaryId) REFERENCES [MilitaryBranches]([Id]) ON DELETE CASCADE
)