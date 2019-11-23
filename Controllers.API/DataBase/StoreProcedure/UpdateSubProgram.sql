

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateSubProgram]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateSubProgram] AS' 
END
GO
Alter PROCEDURE [dbo].[UpdateSubProgram] 
	-- Add the parameters for the stored procedure here
	@PSubProgramName nvarchar(255),
	@PProgramID INT,
	@PSubProgramID INT ,
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update  [dbo].[tblSubPrograms]
	set [clmSubPrograms_ProgramID] = @PProgramID,
	[clmSubPrograms_Name] = @PSubProgramName,
	[clmSubPrograms_ModifiedDate] = GETDATE(),
	[clmSubPrograms_IsActive] = @PActive
	where [clmSubPrograms_ID] = @PSubProgramID;

END
GO