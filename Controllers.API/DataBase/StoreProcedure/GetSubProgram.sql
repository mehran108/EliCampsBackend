

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSubProgram]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetSubProgram] AS' 
END
GO
Alter PROCEDURE [dbo].[GetSubProgram] 
	-- Add the parameters for the stored procedure here
	@PSubProgramID INT
AS
BEGIN
	

    Select [clmSubPrograms_ID] As SubProgramID
      ,[clmSubPrograms_ProgramID] As ProgramID
      ,[clmSubPrograms_Name] As SubProgramName
	  ,[clmSubPrograms_IsActive] As Active
	 from [tblSubPrograms] with (nolock) where [clmSubPrograms_ID] = @PSubProgramID;
END
GO