CREATE TABLE [dbo].[MemberSurveys]
(
	[Id] INT IDENTITY(1, 1)NOT NULL, 
    [SurveyId] INT NOT NULL, 
    [MemberId] INT NOT NULL,

	CONSTRAINT [PK_MemberSurveys] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_MemberSurveys_Surveys] FOREIGN KEY (SurveyId) REFERENCES [Surveys](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_MemberSurveys_Members] FOREIGN KEY (MemberId) REFERENCES [Members]([Id]) ON DELETE CASCADE,
)
