

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateRoomtbl]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateRoomtbl] AS' 
END
GO
Alter PROCEDURE [dbo].UpdateRoomtbl 
	-- Add the parameters for the stored procedure here
	@PRoomID nvarchar(255),
	@PCampus INT,
	@PRoomType nvarchar(255),
	@PBuilding nvarchar(255),
	@PFloor nvarchar(255),
	@PLdx nvarchar(255),
	@PNotes nvarchar(255),
	@PBookedFrom Datetime,
	@PBookedTo Datetime,
	@PAvailable bit,
	@PAvailableFrom Datetime,
	@PAvailableTo Datetime,
	@PImportedOne INT,
	@PWeekno nvarchar(50),
	@PYear INT,
	@PID INT,
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update  [dbo].[tblRoomsList]
	set clmRoom_RoomID = @PRoomID,
	clmRoom_Campus = @PCampus,
	clmRoom_RoomType = @PRoomType,
	clmRoom_Building = @PBuilding,
	clmRoom_Floor = @PFloor,
	clmRoom_Idx = @PLdx,
	clmRoom_Notes = @PNotes,
	clmRoom_BookedFrom = @PBookedFrom,
	clmRoom_BookedTo = @PBookedTo,
	clmRoom_Available = @PAvailable,
	clmRoom_AvailableFrom = @PAvailableFrom,
	clmRoom_AvailableTo = @PAvailableTo,
	clmRoom_ImportedOne = @PImportedOne,
	clmRoom_Weekno = @PWeekno,
	clmRoom_Year = @PYear,
	clmRoom_ModifiedDate= GETDATE(),
	clmRoom_IsActive = @PActive

	where clmRoom_ID = @PID;

END
GO