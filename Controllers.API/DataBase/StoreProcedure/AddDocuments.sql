

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================

-- =============================================
-- Author:		<Author,,>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddDocuments]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddDocuments] AS' 
END
GO
Alter PROCEDURE [dbo].[AddDocuments] 
	-- Add the parameters for the stored procedure here
	@PDocumentId INT = NULL OUTPUT,
	@PDocumentPath nvarchar(max),
	@PDocumentName nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblDocuments]
					([documentPath],[documentName],[IsActive],[CreateDate])
		Values	(@PDocumentPath,@PDocumentName,1,GETDATE());

		SET @PDocumentId = SCOPE_IDENTITY();
END
GO