CREATE TABLE [dbo].[NonprofitAddresses]
(
	[AddressId] INT NOT NULL, 
    [NonprofitId] INT NOT NULL,

    PRIMARY KEY ([AddressId], [NonprofitId]), 
    CONSTRAINT [FK_NonprofitAddresses_Addresses] FOREIGN KEY ([AddressId]) REFERENCES [Addresses]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NonprofitAddresses_Nonprofits] FOREIGN KEY ([NonprofitId]) REFERENCES [Nonprofits]([Id]) ON DELETE CASCADE,
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Maps Nonprofits to Addresses', 'SCHEMA', N'dbo', 'TABLE', N'NonprofitAddresses', NULL, NULL
