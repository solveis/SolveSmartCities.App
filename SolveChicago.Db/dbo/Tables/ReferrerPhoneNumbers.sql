CREATE TABLE [dbo].[ReferrerPhoneNumbers]
(
	[ReferrerId] INT NOT NULL,
    [PhoneNumberId] INT NOT NULL,
	
    PRIMARY KEY ([ReferrerId], [PhoneNumberId]), 
    CONSTRAINT [FK_ReferrerPhoneNumbers_Referrer] FOREIGN KEY ([ReferrerId]) REFERENCES [Referrers]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ReferrerPhoneNumbers_PhoneNumber] FOREIGN KEY (PhoneNumberId) REFERENCES [PhoneNumbers]([Id]) ON DELETE CASCADE,
)
