

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddStudent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddStudent] AS' 
END
GO
Alter PROCEDURE [dbo].[AddStudent] 
	-- Add the parameters for the stored procedure here
	@PID INT = NULL OUTPUT,
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
	@PGroupID [int] ,
	@PIsGroupLeader  bit
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	BEGIN TRANSACTION [Tran]

 BEGIN TRY
	Declare @RefNumberCount INT
	if(@PYear = 0 or @PYear is null)
	Begin
		set @PYear =  year(GetDate());
	END
	set @RefNumberCount = ((Select top 1 clmYear_StudentRef from [tblYears] where  clmYear_Year = @PYear) + 1)
	
    Insert Into [dbo].[tblRegistration]
				([clmReg_Year],[clmReg_Ref],[clmReg_GrpRef],[clmReg_Camps]
				,[clmReg_Gender], [clmReg_FirstName], [clmReg_LastName], [clmReg_HomeAddress], [clmReg_City], [clmReg_State]
				,[clmReg_Country], [clmReg_PostCode], [clmReg_EmergencyContact], [clmReg_Email], [clmReg_Phone]
				,[clmReg_DOB], [clmReg_Age], [clmReg_PassportNumber], [clmReg_AgencyID], [clmReg_ArrivalDate]
				,[clmReg_Terminal], [clmReg_FlightNumber], [clmReg_DestinationFrom], [clmReg_ArrivalTime]
				 ,[clmReg_DepartureDate], [clmReg_DepartureTerminal], [clmReg_DepartureFlightNumber]
				 ,[clmReg_DestinationTo], [clmReg_FlightDepartureTime], [clmReg_MedicalInformation]
				 ,[clmReg_DietaryNeeds], [clmReg_Allergies], [clmRerameStartDatg_Notes]
				 ,[clmReg_ExtraNotes], [clmReg_ExtraNotesHTML], [clmReg_Status], [clmReg_IsActive], [clmReg_CreateDate]
				 ,[clmReg_Proge],[clmReg_ProgrameEndDate],[clmReg_Campus]
				 ,[clmReg_Format],[clmReg_MealPlan],GroupID,IsGroupLeader)
		Values	(@PYear,CONCAT(@PYear, '-',format(@RefNumberCount,'00')),@PGroupRef,@PCamps
				,@PGender, @PFirstName, @PLastName, @PHomeAddress, @PCity, @PState
				,@PCountry, @PPostCode, @PEmergencyContact, @PEmail, @PPhone
				,@PDOB, @PAge, @PPassportNumber, @PAgencyID, @PArrivalDate
				,@PTerminal, @PFlightNumber, @PDestinationFrom, @PArrivalTime
				,@PDepartureDate, @PDepartureTerminal, @PDepartureFlightNumber
				,@PDestinationTo, @PFlightDepartureTime, @PMedicalInformation 
				,@PDietaryNeeds, @PAllergies, @PMedicalNotes
				,@PExtraNotes,@PExtraNotesHTML, @PStatus, 1, GETDATE()
				,@PProgrameStartDate,@PProgrameEndDate,@PCampus
				,@PFormat,@PMealPlan,@PGroupID,@PIsGroupLeader
				);

		SET @PID = SCOPE_IDENTITY();

		 --Add Addins data
		 if @PAddinsID <> '' 
		 Begin
 
				INSERT INTO [dbo].[tblAddinsVsStudent]
					   ([clmAdvsSt_AddinsID],[clmAdvsSt_StudentID])
				 select value , @PID
							from STRING_SPLIT(@PAddinsID, ',');
		 END 

		 update [tblYears] set clmYear_StudentRef = (clmYear_StudentRef + 1)
			where clmYear_Year = @PYear;

	COMMIT TRANSACTION [Tran];

  END TRY

  BEGIN CATCH

    ROLLBACK TRANSACTION [Tran];
		throw
  END CATCH  
END
GO