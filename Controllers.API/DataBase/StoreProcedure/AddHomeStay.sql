

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddHomestay]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddHomestay] AS' 
END
GO
Alter PROCEDURE [dbo].[AddHomestay] 
	-- Add the parameters for the stored procedure here
	

	@PHomeRefrenance nvarchar(255),
	@PHomeName nvarchar(50),
	@PHomeCellNumber nvarchar(50),
	@PHomeEmail nvarchar(255),
	@PHomePrefer nvarchar(50),
	@PHomeMeals nvarchar(50),
	@PHomeDistance nvarchar(50),
	@PHomeIntersection nvarchar(50),
	@PHomeRegion nvarchar(50),
	@PHomeAddress nvarchar(255),
	@PHomeRooms nvarchar(50),
	@PHomeAggrement nvarchar(50),
	@PHomePoliceCheck nvarchar(50),
	@PHomeStayLocationURL nvarchar(500),
	@PHomeID INT = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblHomestay]
				(clmHome_Reference,  clmHome_Name, clmHome_CellNumber, clmHome_Email, clmHome_Address,clmHome_Region,clmHome_Intersection,clmHome_Distance,clmHome_Meals,clmHome_Prefer,clmHome_Rooms,clmHome_Agreement,clmHome_PoliceCheck,clmHome_StayLocationURL, clmHome_CreateDate)
		Values	(@PHomeRefrenance,   @PHomeName,   @PHomeCellNumber,   @PHomeEmail,   @PHomeAddress,  @PHomeRegion,  @PHomeIntersection,  @PHomeDistance, @PHomeMeals,  @PHomePrefer,@PHomeRooms,@PHomeAggrement ,@PHomePoliceCheck, @PHomeStayLocationURL, GETDATE());

		SET @PHomeID = SCOPE_IDENTITY();
END
GO