CREATE TABLE [dbo].[SurveyQuestionOptions]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [QuestionId] INT NOT NULL, 
    [Option] NVARCHAR(MAX) NOT NULL,

	CONSTRAINT [PK_SurveyQuestionOptions] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_SurveyQuestionOptions_SurveyQuestions] FOREIGN KEY (QuestionId) REFERENCES [SurveyQuestions](Id) ON DELETE CASCADE, 
)
