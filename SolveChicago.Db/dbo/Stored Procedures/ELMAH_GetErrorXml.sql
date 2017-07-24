CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

    SET NOCOUNT ON

    SELECT 
        [AllXml]
    FROM 
        [ELMAH_Error]
    WHERE
        [ErrorId] = @ErrorId
    AND
        [Application] = @Application

GO
EXEC sp_addextendedproperty N'MS_Description', N'Default SP for ELMAH Error Logging', 'SCHEMA', N'dbo', 'PROCEDURE', N'ELMAH_GetErrorXml', NULL, NULL