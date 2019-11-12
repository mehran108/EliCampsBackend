

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateGroup] AS' 
END
GO
Alter PROCEDURE [dbo].[UpdateGroup] 
	-- Add the parameters for the stored procedure here
	@PGroupID INT ,
	@PYear INT,
	@PCamps nvarchar(255),
	@PRefNumber nvarchar(255),
	@PAgentID INT,
	@PAgencyRef nvarchar(50),
	@PCountry nvarchar(50),
	@PArrivalDate date,
	@PTerminal nvarchar(255),
	@PFlightNumber nvarchar(255),
	@PDestinationFrom nvarchar(255),
	@PArrivalTime nvarchar(50),
	@PDepartureDate date,
	@PDepartureTerminal nvarchar(255),
	@PDepartureFlightNumber nvarchar(255),
	@PDestinationTo nvarchar(255),
	@PFlightDepartureTime nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update [dbo].[tblGroups]
				set clmGroups_Year = @PYear,  
				clmGroups_Camps = @PCamps, 
				clmGroups_RefNumber = @PRefNumber, 
				clmGroups_AgentID = @PAgentID,
				 clmGroups_AgencyRef =  @PAgencyRef, 
				 clmGroups_Country = @PCountry,
				 clmGroups_ArrivalDate = @PArrivalDate,
				clmGroups_Terminal = @PTerminal,
				 clmGroups_FlightNumber = @PFlightNumber,
				 clmGroups_DestinationFrom = @PDestinationFrom,
				 clmGroups_ArrivalTime = @PArrivalTime,
				 clmGroups_DepartureDate = @PDepartureDate,
				clmGroups_DepartureTerminal = @PDepartureTerminal,
				clmGroups_DepartureFlightNumber = @PDepartureFlightNumber,
				clmGroups_DestinationTo = @PDestinationTo,
				clmGroups_FlightDepartureTime = @PFlightDepartureTime
		where clmGroups_ID = @PGroupID;
END
GO