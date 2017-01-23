CREATE TABLE [dbo].[SurveyQuestions]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [SurveyId] INT NOT NULL, 
    [Question] NVARCHAR(MAX) NOT NULL, 
    [DataType] NVARCHAR(50) NOT NULL

	CONSTRAINT [PK_SurveyQuestions] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_SurveyQuestions_Surveys] FOREIGN KEY (SurveyId) REFERENCES [Surveys](Id) ON DELETE CASCADE, 
)
