
/****** Object:  StoredProcedure [dbo].[GetAllAgent]    Script Date: 11/19/2019 2:29:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetAllAgent] 
	-- Add the parameters for the stored procedure here
	@POffset int = 1, 
    @PPageSize int = 100,
    @PSortColumn varchar(60),
    @PSortAscending bit,
	@PActive bit

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
	 from tblAgents 
	 where ( clmAgents_IsActive = (CASE WHEN @PActive is not null then @PActive else clmAgents_IsActive end))
	  ORDER BY [clmAgents_ID]
  OFFSET 5 ROWS
  FETCH NEXT @PPageSize ROWS ONLY;
END
