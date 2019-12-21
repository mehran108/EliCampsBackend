

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllTrips]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllTrips] AS' 
END
GO
Alter PROCEDURE [dbo].[GetAllTrips] 
	-- Add the parameters for the stored procedure here
	
	@PActive bit
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
	   from tblTrips
	    where ( clmTrips_IsActive = (CASE WHEN @PActive is not null then @PActive else clmTrips_IsActive end))
		order by [clmTrips_ID] desc;
END
GO