

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupPayment]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GroupPayment] AS' 
END
GO
Alter PROCEDURE [dbo].[GroupPayment] 
	-- Add the parameters for the stored procedure here
	@PGroupID INT ,
	@PNumberOfNights INT,
	@PTotalGrossPrice money,
	@PPaid money,
	@PCommision numeric(5, 2),
	@PNetPrice money,
	@PBalance money,
	@PNumOfStudents INT,
	@PNumOfGrpLeaders INT,
	@PPerStudent money,
	@PPerGrpLeader money
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update [dbo].[tblGroups]
				set [clmGroups_NumberOfNights] = @PNumberOfNights,  
				[clmGroups_TotalGrossPrice] = @PTotalGrossPrice, 
				[clmGroups_Paid] = @PPaid, 
				[clmGroups_Commision] = @PCommision,
				 [clmGroups_NetPrice] =  @PNetPrice, 
				 [clmGroups_Balance] = @PBalance,
				 [clmGroups_NumOfStudents] = @PNumOfStudents,
				[clmGroups_NumOfGrpLeaders] = @PNumOfGrpLeaders,
				 [clmGroups_PerStudent] = @PPerStudent,
				 [clmGroups_PerGrpLeader] = @PPerGrpLeader
		where clmGroups_ID = @PGroupID;
END
GO