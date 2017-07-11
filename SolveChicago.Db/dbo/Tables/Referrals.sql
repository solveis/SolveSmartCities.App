CREATE TABLE [dbo].[Referrals]
(
	[Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY, 
    [ReferringId] INT NOT NULL, 
    [ReferredId] INT NOT NULL, 
	[MemberId] INT NOT NULL,
    [ProgramId] INT NULL, 
    [Date] DATETIME2 NULL,

	
    CONSTRAINT [FK_Referrals_ReferringNonprofit] FOREIGN KEY (ReferringId) REFERENCES [Nonprofits](Id) ON DELETE NO ACTION, 
    CONSTRAINT [FK_Referrals_ReferredNonprofit] FOREIGN KEY (ReferredId) REFERENCES [Nonprofits](Id) ON DELETE NO ACTION,  
    CONSTRAINT [FK_Referrals_Member] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE NO ACTION, 
    CONSTRAINT [FK_Referrals_NonprofitProgram] FOREIGN KEY (ProgramId) REFERENCES [NonprofitPrograms](Id) ON DELETE NO ACTION, 
)
