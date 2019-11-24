

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStudent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetStudent] AS' 
END
GO
Alter PROCEDURE [dbo].[GetStudent] 
	-- Add the parameters for the stored procedure here
	@PID INT
AS
BEGIN
	SELECT  [clmReg_ID] AS ID
      ,[clmReg_Year] As Year
      ,[clmReg_Ref] As Reg_Ref
      ,[clmReg_GrpRef] AS GroupRef
      ,[clmReg_Camps] AS Camps
      ,[clmReg_Gender] AS Gender
      ,[clmReg_FirstName] AS FirstName 
      ,[clmReg_LastName] As LastName
      ,[clmReg_HomeAddress] AS HomeAddress
      ,[clmReg_City] AS City
      ,[clmReg_State] AS State
      ,[clmReg_Country] AS Country
      ,[clmReg_PostCode] AS PostCode
      ,[clmReg_EmergencyContact] AS EmergencyContact
      ,[clmReg_Email] AS Email
      ,[clmReg_Phone] AS Phone
      ,[clmReg_DOB] AS DOB
      ,[clmReg_Age] AS Age
      ,[clmReg_PassportNumber] AS PassportNumber
      ,[clmReg_AgencyID] AS AgencyID
      ,[clmReg_ArrivalDate] AS ArrivalDate 
      ,[clmReg_Terminal] AS Terminal 
      ,[clmReg_FlightNumber] AS FlightNumber 
      ,[clmReg_DestinationFrom] AS DestinationFrom
      ,[clmReg_ArrivalTime] AS ArrivalTime
      ,[clmReg_DepartureDate] AS DepartureDate
      ,[clmReg_DepartureTerminal] AS DepartureTerminal 
      ,[clmReg_DepartureFlightNumber] AS DepartureFlightNumber 
      ,[clmReg_DestinationTo] AS DestinationTo
      ,[clmReg_FlightDepartureTime] AS FlightDepartureTime 
      ,[clmReg_MedicalInformation]AS MedicalInformation 
      ,[clmReg_DietaryNeeds] AS DietaryNeeds
      ,[clmReg_Allergies] AS Allergies 
      ,[clmRerameStartDatg_Notes] AS MedicalNotes
      ,[clmReg_Proge] AS ProgrameStartDate
      ,[clmReg_ProgrameEndDate] AS ProgrameEndDate
      ,[clmReg_Campus] AS Campus
      ,[clmReg_Format] AS Format
      ,[clmReg_MealPlan] AS MealPlan
      ,[clmReg_NumberOfNights] AS NumberOfNights 
      ,[clmReg_TotalGrossPrice] AS TotalGrossPrice 
      ,[clmReg_Paid] AS Paid 
      ,[clmReg_Commision] AS Commision
      ,[clmReg_NetPrice] AS NetPrice
      ,[clmReg_Balance] AS Balance
      ,[clmReg_TotalAddins] AS TotalAddins
      ,[clmReg_CommissionAddins] AS CommissionAddins
      ,[clmReg_RoomID] AS RoomID 
      ,[clmReg_HomestayOrResi] AS HomestayOrResi
      ,[clmReg_HomestayID] AS HomestayID
      ,[clmReg_Status] AS Status
      ,[clmReg_ExtraNotes] AS ExtraNotes 
      ,[clmReg_ExtraNotesHTML] AS ExtraNotesHTML 
      ,[clmReg_RoomSearchCampus] AS RoomSearchCampus 
      ,[clmReg_RoomSearchFrom] AS RoomSearchFrom
      ,[clmReg_RoomSearchTo] AS RoomSearchTo
      ,[clmReg_IsActive] AS Active
  FROM [dbo].[tblRegistration]
	where [clmReg_ID] = @PID;

		Select clmAdvsSt_AddinsID As LinkID , 'AddinsID' AS LinkTypeID from 
		[dbo].[tblAddinsVsStudent] where clmAdvsSt_StudentID = @PID
		UNION ALL
		Select clmStTrips_Trip As LinkID, 'GroupTripID' AS LinkTypeID from
		[dbo].[tblStudentTrips] where clmStTrips_Student = @PID;
END
GO