CREATE TABLE [dbo].[MemberStatuses]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [MemberId] INT NOT NULL, 
    [Status] NVARCHAR(128) NOT NULL, 
    [CreatedDate] DATETIME2 NOT NULL,

	CONSTRAINT [PK_MemberStatuses] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_MemberStatuses_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
)
