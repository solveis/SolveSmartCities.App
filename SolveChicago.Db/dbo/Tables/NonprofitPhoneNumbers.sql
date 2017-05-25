CREATE TABLE [dbo].[NonprofitPhoneNumbers]
(
	[NonprofitId] INT NOT NULL,
    [PhoneNumberId] INT NOT NULL,
	
    PRIMARY KEY ([NonprofitId], [PhoneNumberId]), 
    CONSTRAINT [FK_NonprofitPhoneNumbers_Nonprofits] FOREIGN KEY ([NonprofitId]) REFERENCES [Nonprofits]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_NonprofitPhoneNumbers_PhoneNumber] FOREIGN KEY (PhoneNumberId) REFERENCES [PhoneNumbers]([Id]) ON DELETE CASCADE,
)
