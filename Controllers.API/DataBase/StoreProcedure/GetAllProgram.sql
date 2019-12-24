

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllProgram]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllProgram] AS' 
END
GO
Alter PROCEDURE [dbo].[GetAllProgram] 
	-- Add the parameters for the stored procedure here
@PActive bit	
AS
BEGIN
	
    Select [clmPrograms_ID] As ProgramID
      ,[clmPrograms_Name] As ProgramName
      ,[clmPrograms_IsActive] As Active,
	   clmPrograms_IsDefault AS IsDefault
	 from [tblPrograms] with (nolock)
	 where ( [clmPrograms_IsActive] = (CASE WHEN @PActive is not null then @PActive else [clmPrograms_IsActive] end)) 
	 order by [clmPrograms_ID] desc;
END
GO