CREATE TABLE [dbo].[MemberPhoneNumbers]
(
	[MemberId] INT NOT NULL,
    [PhoneNumberId] INT NOT NULL,
	
    PRIMARY KEY ([MemberId], [PhoneNumberId]), 
    CONSTRAINT [FK_MemberPhoneNumbers_Member] FOREIGN KEY ([MemberId]) REFERENCES [Members]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MemberPhoneNumbers_PhoneNumber] FOREIGN KEY (PhoneNumberId) REFERENCES [PhoneNumbers]([Id]) ON DELETE CASCADE,
)
