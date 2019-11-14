

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateAgent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateAgent] AS' 
END
GO
Alter PROCEDURE [dbo].[UpdateAgent] 
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
	@PHomeID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update  [dbo].[tblHomestay]
	set clmHome_Reference = @PHomeRefrenance,
	clmHome_Name = @PHomeName,
	clmHome_CellNumber = @PHomeCellNumber,
	clmHome_Email = @PHomeEmail,
	clmHome_Address = @PHomeAddress,
	clmHome_Region = @PHomeRegion,
	clmHome_Intersection = @PHomeIntersection,
	clmHome_Distance = @PHomeDistance,
	clmHome_Meals = @PHomeMeals,
	clmHome_Prefer = @PHomePrefer,
	clmHome_Rooms = @PHomeRooms,

	clmHome_Agreement = @PHomeAggrement,
	clmHome_PoliceCheck = @PHomePoliceCheck
	where clmHome_ID = @PHomeID;

END
GO