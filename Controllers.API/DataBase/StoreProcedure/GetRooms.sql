

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetRooms]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetRooms] AS' 
END
GO
Alter PROCEDURE [dbo].[GetRooms] 
	-- Add the parameters for the stored procedure here
	@PID INT
AS
BEGIN
	

 Select
	   [clmRoom_ID] As ID
      ,[clmRoom_RoomID] As RoomID
      ,[clmRoom_Campus] As Campus
      ,[clmRoom_RoomType] As  RoomType
      ,[clmRoom_Building] As Building
      ,[clmRoom_Floor] As RoomFloor
      ,[clmRoom_Idx] As ldx
      ,[clmRoom_Notes] As Notes
      ,[clmRoom_BookedFrom] As BookedFrom
      ,[clmRoom_BookedTo] As BookedTo
      ,[clmRoom_Available] As Available
	  ,[clmRoom_AvailableFrom] As AvailableFrom
	  ,[clmRoom_AvailableTo] As AvailableTo
	  ,[clmRoom_ImportedOne] As ImportedOne
	  ,[clmRoom_Weekno] As Weekno
	  ,[clmRoom_Year] As Year,
	  [clmRoom_IsActive] As Active
	 from tblRoomsList where clmRoom_ID = @PID;
END
GO