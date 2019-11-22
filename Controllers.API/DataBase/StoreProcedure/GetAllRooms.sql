USE [elicampsdb]
GO
/****** Object:  StoredProcedure [dbo].[GetAllRooms]    Script Date: 11/19/2019 10:52:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetAllRooms] 
	@PActive bit
AS
BEGIN
	

 Select
	   [clmRoom_ID] As ID
      ,[clmRoom_RoomID] As RoomID
       ,tblc.clmCamps_Camp As Campus
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
	 from tblRoomsList
	 inner join tblCamps tblc on [clmRoom_Campus] = tblc.clmCamps_ID
	  where ( clmRoom_IsActive = (CASE WHEN @PActive is not null then @PActive else clmRoom_IsActive end))
END
