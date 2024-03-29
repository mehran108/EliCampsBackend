

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupPrograme]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GroupPrograme] AS' 
END
GO
Alter PROCEDURE [dbo].[GroupPrograme] 
	-- Add the parameters for the stored procedure here
	@PGroupID INT ,
	@PProgrameStartDate date,
	@PProgrameEndDate date,
	@PCampus INT,
	@PFormat INT,
	@PMealPlan nvarchar(50),
	@PAddinsID nvarchar(500),
	@PRefNumber nvarchar(255),
	@PChapFamily nvarchar(255),
	@PProgramID INT,
	@PSubProgramID INT
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRANSACTION [Tran]

 BEGIN TRY
  --delete Addins data

	Delete from [dbo].[tblAddinsVsGropCam] where clmAdvsGrpCam_GroupID = @PGroupID;
 -- Add Addins data
 if @PAddinsID <> '' 
 Begin
 
	INSERT INTO [dbo].[tblAddinsVsGropCam]
			   ([clmAdvsGrpCam_GroupRef]
			   ,[clmAdvsGrpCam_AddinsID]
			   ,[clmAdvsGrpCam_GroupID])
		 select @PRefNumber,value,@PGroupID
					from STRING_SPLIT(@PAddinsID, ',') 
 END 
           
		     update [dbo].[tblGroups]
				set clmGroups_ProgrameStartDate = @PProgrameStartDate,
				clmGroups_ProgrameEndDate = @PProgrameEndDate,
				clmGroups_Campus = @PCampus,
				clmGroups_Format = @PFormat,
				clmGroups_MealPlan = @PMealPlan,
				clmGroups_ChapFamily = @PChapFamily,
				clmGroups_ProgramID = @PProgramID,
				clmGroups_SubProgramID = @PSubProgramID
		where clmGroups_ID = @PGroupID;
	

   
     COMMIT TRANSACTION [Tran];

  END TRY

  BEGIN CATCH

    ROLLBACK TRANSACTION [Tran];
		throw
  END CATCH  

 END