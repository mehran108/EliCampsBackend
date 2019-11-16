

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupTrips]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GroupTrips] AS' 
END
GO
Alter PROCEDURE [dbo].[GroupTrips] 
	-- Add the parameters for the stored procedure here
	@PGroupID INT ,
	@PGroupTripsID nvarchar(500),
	@PRefNumber nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION [Tran]

 BEGIN TRY
  --delete Trips data

	Delete from [dbo].[tblGroupTrips] where clmGpTrips_GroupID = @PGroupID;
 -- Add Addins data
 if @PGroupTripsID <> '' 
 Begin
 
	INSERT INTO [dbo].[tblGroupTrips]
			   ([clmGpTrips_GroupRef]
			   ,[clmGpTrips_Trip]
			   ,[clmGpTrips_GroupID])
		 select @PRefNumber,value,@PGroupID
					from STRING_SPLIT(@PGroupTripsID, ',') 
 END 
           
		     
     COMMIT TRANSACTION [Tran];

  END TRY

  BEGIN CATCH

    ROLLBACK TRANSACTION [Tran];
		throw
  END CATCH  

 END