

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddRooms]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddRooms] AS' 
END
GO
Alter PROCEDURE [dbo].[AddRooms] 
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
	@PID INT = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblRoomsList]
				(clmRoom_RoomID,  clmRoom_Campus,  clmRoom_RoomType, clmRoom_Building, clmRoom_Floor, clmRoom_Idx,clmRoom_Notes,clmRoom_BookedFrom,clmRoom_BookedTo, clmRoom_Available,clmRoom_AvailableFrom,clmRoom_AvailableTo,clmRoom_ImportedOne,clmRoom_Weekno,clmRoom_Year,clmRoom_CreateDate)
		Values	(@PRoomID,        @PCampus,        @PRoomType,       @PBuilding,       @PFloor,       @PLdx,      @PNotes,      @PBookedFrom,      @PBookedTo,       @PAvailable       , @PAvailableFrom    , @PAvailableTo,     @PImportedOne,      @PWeekno, @PYear,GETDATE());

		SET @PID = SCOPE_IDENTITY();
END
GO