CREATE TABLE [dbo].[NonprofitMembersStaff]
(
	[NonprofitMembersId] INT NOT NULL,
	[NonprofitStaffId] INT NOT NULL,
	
    PRIMARY KEY([NonprofitMembersId], [NonprofitStaffId]),
    CONSTRAINT [FK_NonprofitMembersStaff_NonprofitMembers] FOREIGN KEY (NonprofitMembersId) REFERENCES [NonprofitMembers](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_NonprofitMembersStaff_NonprofitStaff] FOREIGN KEY ([NonprofitStaffId]) REFERENCES [NonprofitStaff]([Id]) ON DELETE NO ACTION,
)
