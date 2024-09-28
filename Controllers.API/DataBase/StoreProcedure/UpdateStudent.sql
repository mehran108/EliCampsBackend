

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateStudent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateStudent] AS' 
END
GO
Alter PROCEDURE [dbo].[UpdateStudent] 
	-- Add the parameters for the stored procedure here
	@PID INT,
	@PYear INT,
	@PGroupRef [nvarchar](255),
	@PCamps [nvarchar](255),
	@PGender [nvarchar](255),
	@PFirstName [nvarchar](255),
	@PLastName [nvarchar](255),
	@PHomeAddress [nvarchar](255),
	@PCity [nvarchar](255),
	@PState [nvarchar](255),
	@PCountry [nvarchar](255),
	@PPostCode [nvarchar](255),
	@PEmergencyContact [nvarchar](255),
	@PEmail [nvarchar](255),
	@PPhone [nvarchar](255),
	@PDOB [date],
	@PAge INT ,
	@PPassportNumber [nvarchar](255),
	@PAgencyID INT,
	@PArrivalDate  [date],
	@PTerminal [nvarchar](255),
	@PFlightNumber [nvarchar](255),
	@PDestinationFrom [nvarchar](255),
	@PArrivalTime [datetime],
	@PDepartureDate [date],
	@PDepartureTerminal [nvarchar](255),
	@PDepartureFlightNumber [nvarchar](255),
	@PDestinationTo [nvarchar](255),
	@PFlightDepartureTime [datetime],
	@PMedicalInformation [ntext],
	@PDietaryNeeds [ntext],
	@PAllergies [ntext],
	@PMedicalNotes [ntext],
	@PExtraNotes [ntext],
	@PExtraNotesHTML [ntext],
	@PStatus [nvarchar](50),

	@PProgrameStartDate [date],
	@PProgrameEndDate [date],
	@PCampus [int],
	@PFormat [int],
	@PMealPlan [nvarchar](255),
	@PAddinsID [nvarchar](500),
	@PHomestayOrResi   [nvarchar](50) ,
	@PHomestayID   [int] ,
	@PRoomID [int],
	@PRoomSearchCampus  [int]  ,
	@PRoomSearchFrom [date]   ,
	@PRoomSearchTo  [date]  ,
	@PNumberOfNights  [int]  ,
	@PTotalGrossPrice  [money]  ,
	@PTotalAddins   [money] ,
	@PPaid   [money] ,
	@PCommision  [money]  ,
	@PCommissionAddins  [money]  ,
	@PNetPrice  [money]  ,
	@PBalance   [money] ,
	@PStudentTripsID  [nvarchar](500),
	@PActive  bit,
	@PChapFamily nvarchar(255),
	@PProgramID INT,
	@PSubProgramID INT,
	@PStudentDocumentId INT,
	@PGroupID [int] ,
	@PIsGroupLeader  bit,
	@PProfilePic  [nvarchar](1000)

	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION [Tran]

 BEGIN TRY

	--delete Trips data
	Delete from [dbo].[tblStudentTrips] where clmStTrips_Student = @PID;

	--delete Addins data
	Delete from [dbo].[tblAddinsVsStudent] where [clmAdvsSt_StudentID] = @PID;

	--update tblRegistration data
	update  [dbo].[tblRegistration]
	set [clmReg_Year] = @PYear,
	[clmReg_GrpRef] = @PGroupRef,
	[clmReg_Camps] = @PCamps,
	[clmReg_Gender] = @PGender,
	[clmReg_FirstName] = @PFirstName,
	[clmReg_LastName] = @PLastName,
	[clmReg_HomeAddress] = @PHomeAddress,
	[clmReg_City] = @PCity,
	[clmReg_State] = @PState,
	[clmReg_Country] = @PCountry,
	[clmReg_PostCode] = @PPostCode,
	[clmReg_EmergencyContact] = @PEmergencyContact,
	[clmReg_Email] = @PEmail,
	[clmReg_Phone] = @PPhone,
	[clmReg_DOB] = @PDOB,
	[clmReg_Age] = @PAge,
	[clmReg_PassportNumber] = @PPassportNumber,
	[clmReg_AgencyID] = @PAgencyID,
	[clmReg_ArrivalDate] = @PArrivalDate,
	[clmReg_Terminal] = @PTerminal,
	[clmReg_FlightNumber] = @PFlightNumber,
	[clmReg_DestinationFrom] = @PDestinationFrom,
	[clmReg_ArrivalTime] = @PArrivalTime,
	[clmReg_DepartureDate] = @PDepartureDate,
	[clmReg_DepartureTerminal] = @PDepartureTerminal,
	[clmReg_DepartureFlightNumber] = @PDepartureFlightNumber,
	[clmReg_DestinationTo] = @PDestinationTo,
	[clmReg_FlightDepartureTime] = @PFlightDepartureTime,
	[clmReg_MedicalInformation] = @PMedicalInformation,
	[clmReg_DietaryNeeds] = @PDietaryNeeds,
	[clmReg_Allergies] = @PAllergies,
	[clmRerameStartDatg_Notes] = @PMedicalNotes,
	[clmReg_ExtraNotes] = @PExtraNotes,
	[clmReg_ExtraNotesHTML] = @PExtraNotesHTML,
	[clmReg_Status] = @PStatus,

	[clmReg_Proge] = @PProgrameStartDate,
	[clmReg_ProgrameEndDate] = @PProgrameEndDate ,
	[clmReg_Campus] = @PCampus ,
	[clmReg_Format] = @PFormat ,
	[clmReg_MealPlan] = @PMealPlan ,
	[clmReg_HomestayOrResi] = @PHomestayOrResi   ,
	[clmReg_HomestayID] = @PHomestayID  ,
	[clmReg_RoomID] = @PRoomID,
	[clmReg_RoomSearchCampus] = @PRoomSearchCampus   ,
	[clmReg_RoomSearchFrom] = @PRoomSearchFrom   ,
	[clmReg_RoomSearchTo] = @PRoomSearchTo   ,
	[clmReg_NumberOfNights] = @PNumberOfNights   ,
	[clmReg_TotalGrossPrice] = @PTotalGrossPrice    ,
	[clmReg_TotalAddins] = @PTotalAddins ,
	[clmReg_Paid] = @PPaid   ,
	[clmReg_Commision] = @PCommision    ,
	[clmReg_CommissionAddins] = @PCommissionAddins   ,
	[clmReg_NetPrice] = @PNetPrice   ,
	[clmReg_Balance] = @PBalance    ,
	[clmReg_IsActive] = @PActive,
	[clmReg_ModifiedDate] = GETDATE(),
	[documentId] = @PStudentDocumentId,
	clmReg_ChapFamily = @PChapFamily,
	clmReg_ProgramID = @PProgramID,
	clmReg_SubProgramID = @PSubProgramID,
	GroupID = @PGroupID,
	IsGroupLeader = @PIsGroupLeader,
	clmReg_ProfilePic = @PProfilePic

	where [clmReg_ID] = @PID;
				
				

		 --Add Addins data
		 if @PAddinsID <> '' 
		 Begin
 
				INSERT INTO [dbo].[tblAddinsVsStudent]
					   ([clmAdvsSt_AddinsID],[clmAdvsSt_StudentID])
				 select value , @PID
							from STRING_SPLIT(@PAddinsID, ',');
		 END 
		
		 --Add Addins data
		 if @PStudentTripsID <> '' 
		 Begin
 
				INSERT INTO [dbo].[tblStudentTrips]
					   ([clmStTrips_Trip],[clmStTrips_Student])
				 select value , @PID
							from STRING_SPLIT(@PStudentTripsID, ',');
		 END 

		 

	COMMIT TRANSACTION [Tran];

  END TRY

  BEGIN CATCH

    ROLLBACK TRANSACTION [Tran];
		throw
  END CATCH  
END
GO