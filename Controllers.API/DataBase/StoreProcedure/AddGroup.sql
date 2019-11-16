

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddGroup] AS' 
END
GO
Alter PROCEDURE [dbo].[AddGroup] 
	-- Add the parameters for the stored procedure here
	@PGroupID INT = NULL OUTPUT,
	@PYear INT,
	@PCamps nvarchar(255),
	--@PRefNumber nvarchar(255),
	@PAgentID INT,
	@PAgencyRef nvarchar(50),
	@PCountry nvarchar(50),
	@PInvoiceType nvarchar(50),
	@PArrivalDate date,
	@PTerminal nvarchar(255),
	@PFlightNumber nvarchar(255),
	@PDestinationFrom nvarchar(255),
	@PArrivalTime nvarchar(50),
	@PDepartureDate date,
	@PDepartureTerminal nvarchar(255),
	@PDepartureFlightNumber nvarchar(255),
	@PDestinationTo nvarchar(255),
	@PFlightDepartureTime nvarchar(50),
	@PApplyToAllStudent bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	Declare @RefNumberCount INT

	set @RefNumberCount = ((Select top 1 clmYear_GroupRef from [tblYears] where  clmYear_Year = @PYear) + 1)

    Insert Into [dbo].[tblGroups]
				(clmGroups_Year,  clmGroups_Camps,  clmGroups_RefNumber, clmGroups_AgentID, clmGroups_AgencyRef, clmGroups_Country,clmGroups_ArrivalDate,
				clmGroups_Terminal, clmGroups_FlightNumber,clmGroups_DestinationFrom,clmGroups_ArrivalTime,clmGroups_DepartureDate,
				clmGroups_DepartureTerminal,clmGroups_DepartureFlightNumber,clmGroups_DestinationTo,
				clmGroups_FlightDepartureTime,clmGroups_InvType,clmGroups_CreateDate,clmGroups_Active,clmGroups_ApplyToAllStudent)
		Values	(@PYear, @PCamps, CONCAT('GR',@PYear, '-',format(@RefNumberCount,'00')), @PAgentID, @PAgencyRef, @PCountry, @PArrivalDate, 
		@PTerminal, @PFlightNumber,@PDestinationFrom,@PArrivalTime,@PDepartureDate,
		@PDepartureTerminal,@PDepartureFlightNumber,@PDestinationTo, @PFlightDepartureTime,@PInvoiceType,
		GETDATE(),1,@PApplyToAllStudent);

		SET @PGroupID = SCOPE_IDENTITY();

		 update [tblYears] set clmYear_GroupRef = (clmYear_GroupRef + 1)
			where clmYear_Year = @PYear;
END
GO