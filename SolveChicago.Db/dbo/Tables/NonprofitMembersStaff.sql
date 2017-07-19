CREATE TABLE [dbo].[NonprofitMembersStaff]
(
	[NonprofitMembersId] INT NOT NULL,
	[NonprofitStaffId] INT NOT NULL,
	
    PRIMARY KEY([NonprofitMembersId], [NonprofitStaffId]),
    CONSTRAINT [FK_NonprofitMembersStaff_NonprofitMembers] FOREIGN KEY (NonprofitMembersId) REFERENCES [NonprofitMembers](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_NonprofitMembersStaff_NonprofitStaff] FOREIGN KEY ([NonprofitStaffId]) REFERENCES [NonprofitStaff]([Id]) ON DELETE NO ACTION,
)
GO
EXEC sp_addextendedproperty N'MS_Description', N'Maps a NonprofitMember relationship to a NonprofitStaff relationship', 'SCHEMA', N'dbo', 'TABLE', N'NonprofitMembersStaff', NULL, NULL
GO
EXEC sp_addextendedproperty N'MS_Description', N'FK to the NonprofitMembers mapping table', 'SCHEMA', N'dbo', 'TABLE', N'NonprofitMembersStaff', 'COLUMN', N'NonprofitMembersId'
GO
EXEC sp_addextendedproperty N'MS_Description', N'FK to the NonprofitStaff mapping table', 'SCHEMA', N'dbo', 'TABLE', N'NonprofitMembersStaff', 'COLUMN', N'NonprofitStaffId'