CREATE TABLE [dbo].[Outcomes]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [MemberId] INT NOT NULL,
    [Name] NVARCHAR(128) NOT NULL, 

	CONSTRAINT [PK_Outcomes] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_Outcomes_Members] FOREIGN KEY (MemberId) REFERENCES [Members](Id) ON DELETE NO ACTION, 
)
