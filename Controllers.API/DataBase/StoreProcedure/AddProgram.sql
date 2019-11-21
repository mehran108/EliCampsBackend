

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddProgram]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddProgram] AS' 
END
GO
Alter PROCEDURE [dbo].[AddProgram] 
	-- Add the parameters for the stored procedure here
	@PName nvarchar(255),
	@PProgramID INT = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblPrograms]
				(clmPrograms_Name,  clmPrograms_IsActive,  clmPrograms_CreateDate)
		Values	(@PName,1,GETDATE());

		SET @PProgramID = SCOPE_IDENTITY();
END
GO