CREATE TABLE [dbo].[CaseManagerPhoneNumbers]
(
	[CaseManagerId] INT NOT NULL,
    [PhoneNumberId] INT NOT NULL,
	
    PRIMARY KEY ([CaseManagerId], [PhoneNumberId]), 
    CONSTRAINT [FK_CaseManagerPhoneNumbers_CaseManager] FOREIGN KEY ([CaseManagerId]) REFERENCES [CaseManagers]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_CaseManagerPhoneNumbers_PhoneNumber] FOREIGN KEY (PhoneNumberId) REFERENCES [PhoneNumbers]([Id]) ON DELETE CASCADE,
)
