CREATE TABLE [dbo].[AdminPhoneNumbers]
(
	[AdminId] INT NOT NULL,
    [PhoneNumberId] INT NOT NULL,
	
    PRIMARY KEY ([AdminId], [PhoneNumberId]), 
    CONSTRAINT [FK_AdminPhoneNumbers_Admin] FOREIGN KEY ([AdminId]) REFERENCES [Admins]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AdminPhoneNumbers_PhoneNumber] FOREIGN KEY (PhoneNumberId) REFERENCES [PhoneNumbers]([Id]) ON DELETE CASCADE,
)
