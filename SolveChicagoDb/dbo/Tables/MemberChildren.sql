CREATE TABLE [dbo].[MemberChildren]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL, 
    [MemberId] INT NOT NULL, 
    [BirthDate] DATETIME2 NOT NULL,

	CONSTRAINT [PK_MemberChildren] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_MemberChildren_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
)
