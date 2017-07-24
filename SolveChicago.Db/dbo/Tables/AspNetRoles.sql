CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex]
    ON [dbo].[AspNetRoles]([Name] ASC);
GO
EXEC sp_addextendedproperty N'MS_Description', N'Default ASP.NET Identity Table', 'SCHEMA', N'dbo', 'TABLE', N'AspNetRoles', NULL, NULL

