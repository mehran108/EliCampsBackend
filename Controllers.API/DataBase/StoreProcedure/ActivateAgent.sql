

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivateAgent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ActivateAgent] AS' 
END
GO
Alter PROCEDURE [dbo].[ActivateAgent] 
	-- Add the parameters for the stored procedure here
	@PAgentID INt,
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update  [dbo].[tblAgents]
	set 
	clmAgents_ModifiedDate = GETDATE(),
	clmAgents_IsActive = @PActive
	where clmAgents_ID = @PAgentID;

END
GO