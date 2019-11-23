

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivateSubProgram]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ActivateSubProgram] AS' 
END
GO
Alter PROCEDURE [dbo].[ActivateSubProgram] 
	-- Add the parameters for the stored procedure here
	
	@PSubProgramID INT ,
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update  [dbo].[tblSubPrograms]
	set 
	[clmSubPrograms_ModifiedDate] = GETDATE(),
	[clmSubPrograms_IsActive] = @PActive
	where [clmSubPrograms_ID] = @PSubProgramID;

END
GO