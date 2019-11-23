

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddSubProgram]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddSubProgram] AS' 
END
GO
Alter PROCEDURE [dbo].[AddSubProgram] 
	-- Add the parameters for the stored procedure here
	@PSubProgramName nvarchar(255),
	@PProgramID INT,
	@PSubProgramID INT = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblSubPrograms]
				(clmSubPrograms_Name,clmSubPrograms_ProgramID,  clmSubPrograms_IsActive,  clmSubPrograms_CreateDate)
		Values	(@PSubProgramName,@PProgramID,1,GETDATE());

		SET @PSubProgramID = SCOPE_IDENTITY();
END
GO