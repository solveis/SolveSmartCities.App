CREATE TABLE [dbo].[MemberEmergencyContacts]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [MemberId] INT NOT NULL, 
    [Name] NVARCHAR(128) NOT NULL, 
    [Phone] NVARCHAR(128) NOT NULL, 
    [Email] NVARCHAR(128) NOT NULL,

	CONSTRAINT [PK_MemberEmergencyContacts] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_MemberEmergencyContacts_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
)
