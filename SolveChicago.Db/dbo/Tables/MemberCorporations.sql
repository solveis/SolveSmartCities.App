CREATE TABLE [dbo].[MemberCorporations]
(
	[MemberId] INT NOT NULL, 
    [CorporationId] INT NOT NULL, 
	[Start] DATETIME2 NOT NULL, 
    [End] DATETIME2 NULL, 
    [ReasonForLeaving] NVARCHAR(MAX) NULL, 
    [Pay] DECIMAL(18, 2) NULL, 

    [NonprofitId] INT NULL, 
    [IsMemberConfirmed] BIT NULL , 
    PRIMARY KEY([MemberId], [CorporationId]),
    CONSTRAINT [FK_MemberCorporations_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_MemberCorporations_Corporations] FOREIGN KEY (CorporationId) REFERENCES [Corporations]([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MemberCorporations_Nonprofits] FOREIGN KEY (NonprofitId) REFERENCES [Nonprofits]([Id]) ON DELETE SET NULL
)
