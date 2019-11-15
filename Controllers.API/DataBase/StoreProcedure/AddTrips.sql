

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================

-- =============================================
-- Author:		<Author,,Zulqarnain>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddTrips]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddTrips] AS' 
END
GO
Alter PROCEDURE [dbo].[AddTrips] 
	-- Add the parameters for the stored procedure here
	

	@PYear INT,
	@PTrip nvarchar(255),
	@PCamps nvarchar(255),
	@PTripsDate Datetime,
	@PLdx nvarchar(255),
	@PNotes nvarchar(255),
	@PID INT = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblTrips]
				(clmTrips_Year,  clmTrips_Trip, clmTrips_Camps, clmTrips_Date, clmTrips_Notes,clmTrips_IDX, clmTrips_CreateDate)
		Values	(@PYear,        @PTrip,       @PCamps,       @PTripsDate,       @PNotes,      @PLdx ,     GETDATE());

		SET @PID = SCOPE_IDENTITY();
END
GO