CREATE TABLE [dbo].[UserProfileReferrers]
(
	[UserId] NVARCHAR(128)  NOT NULL, 
    [ReferrerId] INT NOT NULL,

	PRIMARY KEY ([ReferrerId], [UserId]), 
    CONSTRAINT [FK_UserProfileReferrers_AspNetUser] FOREIGN KEY (UserId) REFERENCES [AspNetUsers](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_UserProfileReferrers_Referrer] FOREIGN KEY (ReferrerId) REFERENCES [Referrers]([Id]) ON DELETE CASCADE
)
