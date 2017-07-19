CREATE TABLE [dbo].[FamilyAddresses]
(
	[AddressId] INT NOT NULL, 
    [FamilyId] INT NOT NULL,

    PRIMARY KEY ([AddressId], [FamilyId]), 
    CONSTRAINT [FK_FamilyAddresses_Addresses] FOREIGN KEY ([AddressId]) REFERENCES [Addresses]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_FamilyAddresses_Families] FOREIGN KEY ([FamilyId]) REFERENCES [Families]([Id]) ON DELETE CASCADE,
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Maps Families to Addresses', 'SCHEMA', N'dbo', 'TABLE', N'FamilyAddresses', NULL, NULL