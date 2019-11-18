

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetGroup] AS' 
END
GO
Alter PROCEDURE [dbo].[GetGroup] 
	-- Add the parameters for the stored procedure here
	@PGroupID INT
AS
BEGIN
	

	 Select 
				clmGroups_ID As GroupID,
				clmGroups_Year As Year,  
				clmGroups_Camps As Camps, 
				clmGroups_RefNumber As  RefNumber, 
				clmGroups_AgentID As AgentID,
				clmGroups_AgencyRef As  AgencyRef, 
				clmGroups_Country As Country,
				clmGroups_InvType As InvoiceType,
				clmGroups_ArrivalDate AS ArrivalDate,
				clmGroups_Terminal As Terminal,
				clmGroups_FlightNumber As FlightNumber,
				clmGroups_DestinationFrom As DestinationFrom,
				clmGroups_ArrivalTime As ArrivalTime,
				clmGroups_DepartureDate As DepartureDate,
				clmGroups_DepartureTerminal As DepartureTerminal,
				clmGroups_DepartureFlightNumber As DepartureFlightNumber,
				clmGroups_DestinationTo As DestinationTo,
				clmGroups_FlightDepartureTime As FlightDepartureTime,
				clmGroups_ProgrameStartDate As ProgrameStartDate,
				clmGroups_ProgrameEndDate As ProgrameEndDate,
				clmGroups_Campus As Campus,
				clmGroups_Format As Format,
				clmGroups_MealPlan As MealPlan,
				clmGroups_NumberOfNights As NumberOfNights,
				clmGroups_TotalGrossPrice As TotalGrossPrice,
				clmGroups_Paid As Paid,
				clmGroups_Commision As Commision,
				clmGroups_NetPrice As NetPrice,
				clmGroups_Balance As Balance,
				clmGroups_NumOfStudents As NumOfStudents,
				clmGroups_NumOfGrpLeaders As NumOfGrpLeaders,
				clmGroups_PerStudent As PerStudent,
				clmGroups_PerGrpLeader As PerGrpLeader,
				clmGroups_Active As Active,
				clmGroups_ApplyToAllStudent AS ApplyToAllStudent
		from [dbo].[tblGroups] 
		where clmGroups_ID = @PGroupID;

		Select clmAdvsGrpCam_AddinsID As LinkID , 'AddinsID' AS LinkTypeID from 
		[dbo].[tblAddinsVsGropCam] where clmAdvsGrpCam_GroupID = @PGroupID
		UNION ALL
		Select clmGpTrips_Trip As LinkID, 'GroupTripID' AS LinkTypeID from
		[dbo].[tblGroupTrips] where clmGpTrips_GroupID = @PGroupID;
END
GO