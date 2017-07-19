CREATE TABLE [dbo].[MemberSchools]
(
	[MemberId] INT NOT NULL, 
    [SchoolId] INT NOT NULL, 
    [Start] DATETIME2 NOT NULL, 
    [End] DATETIME2 NULL, 
    [IsCurrent] BIT NOT NULL DEFAULT 0, 
    [Degree] NVARCHAR(128) NULL,
	
	PRIMARY KEY ([MemberId], [SchoolId]), 
    CONSTRAINT [FK_MemberSchools_Member] FOREIGN KEY ([MemberId]) REFERENCES [Members]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MemberSchools_School] FOREIGN KEY (SchoolId) REFERENCES [Schools]([Id]) ON DELETE CASCADE,
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Maps Members to Schools', 'SCHEMA', N'dbo', 'TABLE', N'MemberSchools', NULL, NULL