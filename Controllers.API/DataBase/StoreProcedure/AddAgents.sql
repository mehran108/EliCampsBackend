

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================

-- =============================================
-- Author:		<Author,,Zulqarnain>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddAgents]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddAgents] AS' 
END
GO
Alter PROCEDURE [dbo].[AddAgents] 
	-- Add the parameters for the stored procedure here
	@PAgentAgent nvarchar(255),
	@PAgentContact nvarchar(255),
	@PAgentPhone nvarchar(255),
	@PAgentEmail nvarchar(255),
	@PAgentWeb nvarchar(255),
	@PAgentAddress nvarchar(255),
	@PAgentCountry nvarchar(255),
	@PAgentOther nvarchar(255),
	@PAgentNotes text,
	@PAgentID INT = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblAgents]
				(clmAgents_Agent,  clmAgents_Contact,  clmAgents_Phone, clmAgents_Email, clmAgents_Web, clmAgents_Address,clmAgents_Country,clmAgents_Notes,clmAgents_Other, clmAgents_CreateDate)
		Values	(@PAgentAgent, @PAgentContact, @PAgentPhone, @PAgentEmail, @PAgentWeb, @PAgentAddress, @PAgentCountry, @PAgentNotes, @PAgentOther, GETDATE());

		SET @PAgentID = SCOPE_IDENTITY();
END
GO