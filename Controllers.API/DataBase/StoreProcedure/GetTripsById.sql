

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTripsById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetTripsById] AS' 
END
GO
Alter PROCEDURE [dbo].[GetTripsById] 
	-- Add the parameters for the stored procedure here
	@PID INT
AS
BEGIN
	

    Select [clmTrips_ID] As Trips_ID
      ,[clmTrips_Year] As TripYear
      ,[clmTrips_Trip] As TripName
      ,[clmTrips_Camps] As  Camps
      ,[clmTrips_Date] As TripDate
      ,[clmTrips_Notes] As TripNotes
      ,[clmTrips_IDX] As TripLdx,
	  [clmTrips_IsActive] As Active
	 from tblTrips where clmTrips_ID = @PID;
END
GO