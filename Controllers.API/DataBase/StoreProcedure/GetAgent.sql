

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAgent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAgent] AS' 
END
GO
Alter PROCEDURE [dbo].[GetAgent] 
	-- Add the parameters for the stored procedure here
	@PAgentID INT
AS
BEGIN
	

    Select [clmAgents_ID] As AgentId
      ,[clmAgents_Agent] As AgentAgent
      ,[clmAgents_Contact] As AgentContact
      ,[clmAgents_Phone] As  AgentPhone
      ,[clmAgents_Email] As AgentEmail
      ,[clmAgents_Web] As AgentWeb
      ,[clmAgents_Address] As AgentAddress
      ,[clmAgents_Country] As AgentCountry
      ,[clmAgents_Notes] As AgentNotes
      ,[clmAgents_Other] As AgentOther
      ,[clmAgents_IsActive] As Active
	 from tblAgents where clmAgents_ID = @PAgentID;
END
GO