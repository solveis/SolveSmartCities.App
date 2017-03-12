CREATE TABLE [dbo].[AdminInviteCodes]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [InviteCode] NVARCHAR(50) NOT NULL, 
    [InvitingAdminUserId] NVARCHAR(128) NOT NULL, 
    [RecevingAdminUserId] NVARCHAR(128) NULL, 
	
	CONSTRAINT [PK_AdminInviteCodes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdminInviteCodes_Admin] FOREIGN KEY (InvitingAdminUserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE, 
)
