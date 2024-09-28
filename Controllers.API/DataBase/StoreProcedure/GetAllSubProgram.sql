

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllSubProgram]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllSubProgram] AS' 
END
GO
Alter PROCEDURE [dbo].[GetAllSubProgram] 
	-- Add the parameters for the stored procedure here
	@PProgramID INT,
	@PActive bit
AS
BEGIN
	

    Select tbl.[clmSubPrograms_ID] As SubProgramID
      ,tbl.[clmSubPrograms_ProgramID] As ProgramID
      ,tbl.[clmSubPrograms_Name] As SubProgramName
	  ,tbl.[clmSubPrograms_IsActive] As Active,
	  p.[clmPrograms_Name] as ProgramName
	 from [tblSubPrograms] tbl with (nolock)
	 inner join [tblPrograms] p with (nolock) on tbl.[clmSubPrograms_ProgramID] = p.clmPrograms_ID
	 where ( [clmSubPrograms_ProgramID] = (CASE WHEN @PProgramID is not null and @PProgramID <> 0 then @PProgramID else [clmSubPrograms_ProgramID] end))
	 and ( [clmSubPrograms_IsActive] = (CASE WHEN @PActive is not null then @PActive else [clmSubPrograms_IsActive] end))
	  order by tbl.[clmSubPrograms_ID] desc ;
END
GO