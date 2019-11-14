

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetHomeStayById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetHomeStayById] AS' 
END
GO
Alter PROCEDURE [dbo].[GetHomeStayById] 
	-- Add the parameters for the stored procedure here
	@PHomeID INT
AS
BEGIN
	

    Select [clmHome_ID] As HomeID
      ,[clmHome_Reference] As HomeRefrenance
      ,[clmHome_Name] As PHomeName 
      ,[clmHome_CellNumber] As  HomeEmail
      ,[clmHome_Email] As TripDate
      ,[clmHome_Address] As HomeAddress
      ,[clmHome_Region] As HomeRegion

	  ,[clmHome_Intersection] As HomeIntersection
      ,[clmHome_Distance] As HomeDistance
      ,[clmHome_Prefer] As  HomePrefer
      ,[clmHome_Rooms] As HomeRooms
      ,[clmHome_Agreement] As HomeAggrement
      ,[clmHome_PoliceCheck] As HomePoliceCheck
	 from tblHomestay where clmHome_ID = @PHomeID;
END
GO