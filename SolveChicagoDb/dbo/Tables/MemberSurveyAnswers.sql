CREATE TABLE [dbo].[MemberSurveyAnswers]
(
	[Id] INT IDENTITY(1, 1)NOT NULL, 
    [SurveyId] INT NOT NULL, 
    [AnswerBatchId] NVARCHAR(36) NOT NULL, 
    [MemberId] INT NOT NULL,

	CONSTRAINT [PK_MemberSurveyAnswers] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_MemberSurveyAnswers_Surveys] FOREIGN KEY (SurveyId) REFERENCES [Surveys](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_MemberSurveyAnswers_SurveyAnswers] FOREIGN KEY (AnswerBatchId) REFERENCES [SurveyAnswers]([AnswerBatchId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MemberSurveyAnswers_Members] FOREIGN KEY (MemberId) REFERENCES [Members]([Id]) ON DELETE CASCADE,
)
