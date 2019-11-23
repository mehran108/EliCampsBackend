

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivateProgram]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ActivateProgram] AS' 
END
GO
Alter PROCEDURE [dbo].[ActivateProgram] 
	-- Add the parameters for the stored procedure here

	@PProgramID INT,
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update  [dbo].[tblPrograms]
	set 
	[clmPrograms_ModifiedDate] = GETDATE(),
	[clmPrograms_IsActive] = @PActive
	where [clmPrograms_ID] = @PProgramID;

END
GO