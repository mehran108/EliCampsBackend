

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllGroup] AS' 
END
GO
Alter PROCEDURE [dbo].[GetAllGroup] 
	-- Add the parameters for the stored procedure herez
	--@PActive bit

AS
BEGIN
	

	 Select 
				tbl.clmGroups_ID As GroupID,
				tbl.clmGroups_Year As Year,  
				tbl.clmGroups_Camps As Camps, 
				tbl.clmGroups_RefNumber As  RefNumber, 
				tbl.clmGroups_AgentID As AgentID,
				tbl.clmGroups_AgencyRef As  AgencyRef, 
				tbl.clmGroups_Country As Country,
				tbl.clmGroups_InvType As InvoiceType,
				tbl.clmGroups_ArrivalDate AS ArrivalDate,
				tbl.clmGroups_Terminal As Terminal,
				tbl.clmGroups_FlightNumber As FlightNumber,
				tbl.clmGroups_DestinationFrom As DestinationFrom,
				tbl.clmGroups_ArrivalTime As ArrivalTime,
				tbl.clmGroups_DepartureDate As DepartureDate,
				tbl.clmGroups_DepartureTerminal As DepartureTerminal,
				tbl.clmGroups_DepartureFlightNumber As DepartureFlightNumber,
				tbl.clmGroups_DestinationTo As DestinationTo,
				tbl.clmGroups_FlightDepartureTime As FlightDepartureTime,
				tbl.clmGroups_ProgrameStartDate As ProgrameStartDate,
				tbl.clmGroups_ProgrameEndDate As ProgrameEndDate,
				tbl.clmGroups_Campus As Campus,
				tbl.clmGroups_Format As Format,
				tbl.clmGroups_MealPlan As MealPlan,
				tbl.clmGroups_NumberOfNights As NumberOfNights,
				tbl.clmGroups_TotalGrossPrice As TotalGrossPrice,
				tbl.clmGroups_Paid As Paid,
				tbl.clmGroups_Commision As Commision,
				tbl.clmGroups_NetPrice As NetPrice,
				tbl.clmGroups_Balance As Balance,
				tbl.clmGroups_NumOfStudents As NumOfStudents,
				tbl.clmGroups_NumOfGrpLeaders As NumOfGrpLeaders,
				tbl.clmGroups_PerStudent As PerStudent,
				tbl.clmGroups_PerGrpLeader As PerGrpLeader,
				tbl.clmGroups_Active As Active,
				tbl.clmGroups_ApplyToAllStudent AS ApplyToAllStudent,
				agents.clmAgents_Agent AS AgentName,
				campus.clmCampuses_Campus AS CampusName,
				lv.name AS FormatName
		from [dbo].[tblGroups] tbl
		left join [tblAgents] agents on tbl.clmGroups_AgentID = agents.clmAgents_ID
		left join [tblCampuses] campus on tbl.clmGroups_Campus = campus.clmCampuses_ID
		left join [LookupValue] lv on tbl.clmGroups_Format = lv.id
		-- where ( tbl.clmGroups_Active = (CASE WHEN @PActive is not null then @PActive else tbl.clmGroups_Active end))
END
GO