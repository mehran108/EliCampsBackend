

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateTrips]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateTrips] AS' 
END
GO
Alter PROCEDURE [dbo].[UpdateTrips] 
	-- Add the parameters for the stored procedure here
	@PID INT,
	@PYear INT,
	@PTrip nvarchar(255),
	@PCamps  nvarchar(255),
	@PTripsDate Datetime,
	@PTripsNotes nvarchar(255),
	@PLdx nvarchar(350),
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update  [dbo].[tblTrips]
	set 
	clmTrips_Year = @PYear,
	clmTrips_Trip = @PTrip,
	clmTrips_Camps = @PCamps,
	clmTrips_Date = @PTripsDate,
	clmTrips_Notes = @PTripsNotes,
	clmTrips_IDX = @PLdx,
	clmTrips_ModifiedDate = GETDATE(),
	clmTrips_IsActive = @PActive
	where clmTrips_ID = @PID;

END
GO