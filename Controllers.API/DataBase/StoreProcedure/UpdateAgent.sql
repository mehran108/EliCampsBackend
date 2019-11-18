

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateAgent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateAgent] AS' 
END
GO
Alter PROCEDURE [dbo].[UpdateAgent] 
	-- Add the parameters for the stored procedure here
	@PAgentID INt,
	@PAgentAgent nvarchar(255),
	@PAgentContact nvarchar(255),
	@PAgentPhone nvarchar(255),
	@PAgentEmail nvarchar(255),
	@PAgentWeb nvarchar(255),
	@PAgentAddress nvarchar(255),
	@PAgentCountry nvarchar(255),
	@PAgentOther nvarchar(255),
	@PAgentNotes text,
	@Active bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update  [dbo].[tblAgents]
	set clmAgents_Agent = @PAgentAgent,
	clmAgents_Contact = @PAgentContact,
	clmAgents_Phone = @PAgentPhone,
	clmAgents_Email = @PAgentEmail,
	clmAgents_Web = @PAgentWeb,
	clmAgents_Address = @PAgentAddress,
	clmAgents_Country = @PAgentCountry,
	clmAgents_Notes = @PAgentNotes,
	clmAgents_Other = @PAgentOther,
	clmAgents_ModifiedDate = GETDATE(),
	clmAgents_IsActive = @Active
	where clmAgents_ID = @PAgentID;

END
GO