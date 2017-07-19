CREATE TABLE [dbo].[AdminInviteCodes]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [InviteCode] NVARCHAR(50) NOT NULL, 
    [InvitingAdminUserId] NVARCHAR(128) NOT NULL, 
    [RecevingAdminUserId] NVARCHAR(128) NULL, 
	
	[IsStale] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [PK_AdminInviteCodes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_AdminInviteCodes_Admin] FOREIGN KEY (InvitingAdminUserId) REFERENCES AspNetUsers(Id) ON DELETE CASCADE, 
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Stores invite codes from admin to admin', 'SCHEMA', N'dbo', 'TABLE', N'AdminInviteCodes', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Guid', 'SCHEMA', N'dbo', 'TABLE', N'AdminInviteCodes', 'COLUMN', N'InviteCode'
GO
EXEC sp_addextendedproperty N'MS_Description', N'1 = used, 0 = unused', 'SCHEMA', N'dbo', 'TABLE', N'AdminInviteCodes', 'COLUMN', N'IsStale'
