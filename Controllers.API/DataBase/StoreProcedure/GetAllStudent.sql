

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllStudent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllStudent] AS' 
END
GO
Alter PROCEDURE [dbo].[GetAllStudent] 
	-- Add the parameters for the stored procedure herez
	@PActive bit

AS
BEGIN
	

	 SELECT  tbl.[clmReg_ID] AS ID
      ,tbl.[clmReg_Year] As Year
      ,tbl.[clmReg_Ref] As Reg_Ref
      ,tbl.[clmReg_GrpRef] AS GroupRef
      ,tbl.[clmReg_Camps] AS Camps
      ,tbl.[clmReg_Gender] AS Gender
      ,tbl.[clmReg_FirstName] AS FirstName 
      ,tbl.[clmReg_LastName] As LastName
      ,tbl.[clmReg_HomeAddress] AS HomeAddress
      ,tbl.[clmReg_City] AS City
      ,tbl.[clmReg_State] AS State
      ,tbl.[clmReg_Country] AS Country
      ,tbl.[clmReg_PostCode] AS PostCode
      ,tbl.[clmReg_EmergencyContact] AS EmergencyContact
      ,tbl.[clmReg_Email] AS Email
      ,tbl.[clmReg_Phone] AS Phone
      ,tbl.[clmReg_DOB] AS DOB
      ,tbl.[clmReg_Age] AS Age
      ,tbl.[clmReg_PassportNumber] AS PassportNumber
      ,tbl.[clmReg_AgencyID] AS AgencyID
      ,tbl.[clmReg_ArrivalDate] AS ArrivalDate 
      ,tbl.[clmReg_Terminal] AS Terminal 
      ,tbl.[clmReg_FlightNumber] AS FlightNumber 
      ,tbl.[clmReg_DestinationFrom] AS DestinationFrom
      ,tbl.[clmReg_ArrivalTime] AS ArrivalTime
      ,tbl.[clmReg_DepartureDate] AS DepartureDate
      ,tbl.[clmReg_DepartureTerminal] AS DepartureTerminal 
      ,tbl.[clmReg_DepartureFlightNumber] AS DepartureFlightNumber 
      ,tbl.[clmReg_DestinationTo] AS DestinationTo
      ,tbl.[clmReg_FlightDepartureTime] AS FlightDepartureTime 
      ,tbl.[clmReg_MedicalInformation]AS MedicalInformation 
      ,tbl.[clmReg_DietaryNeeds] AS DietaryNeeds
      ,tbl.[clmReg_Allergies] AS Allergies 
      ,tbl.[clmRerameStartDatg_Notes] AS MedicalNotes
      ,tbl.[clmReg_Proge] AS ProgrameStartDate
      ,tbl.[clmReg_ProgrameEndDate] AS ProgrameEndDate
      ,tbl.[clmReg_Campus] AS Campus
      ,tbl.[clmReg_Format] AS Format
      ,tbl.[clmReg_MealPlan] AS MealPlan
      ,tbl.[clmReg_NumberOfNights] AS NumberOfNights 
      ,tbl.[clmReg_TotalGrossPrice] AS TotalGrossPrice 
      ,tbl.[clmReg_Paid] AS Paid 
      ,tbl.[clmReg_Commision] AS Commision
      ,tbl.[clmReg_NetPrice] AS NetPrice
      ,tbl.[clmReg_Balance] AS Balance
      ,tbl.[clmReg_TotalAddins] AS TotalAddins
      ,tbl.[clmReg_CommissionAddins] AS CommissionAddins
      ,tbl.[clmReg_RoomID] AS RoomID 
      ,tbl.[clmReg_HomestayOrResi] AS HomestayOrResi
      ,tbl.[clmReg_HomestayID] AS HomestayID
      ,tbl.[clmReg_Status] AS Status
      ,tbl.[clmReg_ExtraNotes] AS ExtraNotes 
      ,tbl.[clmReg_ExtraNotesHTML] AS ExtraNotesHTML 
      ,tbl.[clmReg_RoomSearchCampus] AS RoomSearchCampus 
      ,tbl.[clmReg_RoomSearchFrom] AS RoomSearchFrom
      ,tbl.[clmReg_RoomSearchTo] AS RoomSearchTo
      ,tbl.[clmReg_IsActive] AS Active
	  ,agents.clmAgents_Agent AS AgentName
	  ,lv.name AS FormatName,
	  tbl.clmReg_ChapFamily AS  ChapFamily,
		tbl.clmReg_ProgramID AS ProgramID,
		tbl.clmReg_SubProgramID AS SubProgramID
  FROM [dbo].[tblRegistration] tbl
		left join [tblAgents] agents on tbl.[clmReg_AgencyID] = agents.clmAgents_ID
		left join [LookupValue] lv on tbl.[clmReg_Format] = lv.id
		 where ( tbl.[clmReg_IsActive] = (CASE WHEN @PActive is not null then @PActive else tbl.[clmReg_IsActive] end))
END
GO