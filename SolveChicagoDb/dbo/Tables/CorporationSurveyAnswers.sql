CREATE TABLE [dbo].[CorporationSurveyAnswers]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [SurveyId] INT NOT NULL, 
    [AnswerBatchId] NVARCHAR(36) NOT NULL, 
    [CorporationId] INT NOT NULL,

	CONSTRAINT [PK_CorporationSurveyAnswers] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_CorporationSurveyAnswers_Surveys] FOREIGN KEY (SurveyId) REFERENCES [Surveys](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_CorporationSurveyAnswers_SurveyAnswers] FOREIGN KEY (AnswerBatchId) REFERENCES [SurveyAnswers]([AnswerBatchId]) ON DELETE CASCADE,
    CONSTRAINT [FK_CorporationSurveyAnswers_Corporations] FOREIGN KEY (CorporationId) REFERENCES [Corporations]([Id]) ON DELETE CASCADE,
)
