CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
	[UserName]             NVARCHAR (256) NOT NULL,
    [ReceiveEmail] BIT NULL, 
    [LastActivityDate] DATETIME2 NOT NULL DEFAULT ((GETDATE())), 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT ((GETDATE())), 
    CONSTRAINT [PK_AspNetUser] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);
GO
EXEC sp_addextendedproperty N'MS_Description', N'Default ASP.NET Identity Table', 'SCHEMA', N'dbo', 'TABLE', N'AspNetUsers', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'Opt-out user wishes to receive email', 'SCHEMA', N'dbo', 'TABLE', N'AspNetUsers', 'COLUMN', N'ReceiveEmail'
