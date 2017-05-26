CREATE TABLE [dbo].[NonprofitAddresses]
(
	[AddressId] INT NOT NULL, 
    [NonprofitId] INT NOT NULL,

    PRIMARY KEY ([AddressId], [NonprofitId]), 
    CONSTRAINT [FK_NonprofitAddresses_Addresses] FOREIGN KEY ([AddressId]) REFERENCES [Addresses]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NonprofitAddresses_Nonprofits] FOREIGN KEY ([NonprofitId]) REFERENCES [Nonprofits]([Id]) ON DELETE CASCADE,
)
