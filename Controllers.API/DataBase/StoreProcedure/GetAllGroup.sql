

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
	-- Add the parameters for the stored procedure here
AS
BEGIN
	

	 Select 
				clmGroups_ID  As GroupID,
				clmGroups_Year As Year,  
				clmGroups_Camps As Camps, 
				clmGroups_RefNumber As  RefNumber, 
				clmGroups_AgentID As AgentID,
				clmGroups_AgencyRef As  AgencyRef, 
				clmGroups_Country As Country,
				clmGroups_ArrivalDate AS ArrivalDate,
				clmGroups_Terminal As Terminal,
				clmGroups_FlightNumber As FlightNumber,
				clmGroups_DestinationFrom As DestinationFrom,
				clmGroups_ArrivalTime As ArrivalTime,
				clmGroups_DepartureDate As DepartureDate,
				clmGroups_DepartureTerminal As DepartureTerminal,
				clmGroups_DepartureFlightNumber As DepartureFlightNumber,
				clmGroups_DestinationTo As DestinationTo,
				clmGroups_FlightDepartureTime As FlightDepartureTime
		from [dbo].[tblGroups] ;
END
GO