CREATE TABLE [dbo].[CorporationSurveys]
(
	[Id] INT IDENTITY(1, 1) NOT NULL, 
    [SurveyId] INT NOT NULL, 
    [CorporationId] INT NOT NULL,
	
	CONSTRAINT [PK_CorporationSurveys] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_CorporationSurveys_Surveys] FOREIGN KEY (SurveyId) REFERENCES [Surveys](Id) ON DELETE CASCADE, 
)
