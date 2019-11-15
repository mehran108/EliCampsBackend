

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdatePaymentGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdatePaymentGroup] AS' 
END
GO
Alter PROCEDURE [dbo].[UpdatePaymentGroup] 
	-- Add the parameters for the stored procedure here
	@PPaymentGroupID INT ,
	@PGroupID INT,
	@PRefNumber nvarchar(255),
	@PPaymentGroupDate date,
	@PPaymentGroupAmount money,
	@PPaymentGroupRemarks nvarchar(255),
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update [dbo].[tblPaymentsGroups]
				set ClmPaymentsGrp_RefNumber = @PRefNumber,  
				ClmPaymentsGrp_GroupID = @PGroupID, 
				ClmPaymentsGrp_Date = @PPaymentGroupDate, 
				ClmPaymentsGrp_Amount = @PPaymentGroupAmount,
				 ClmPaymentsGrp_Remarks =  @PPaymentGroupRemarks, 
				 ClmPaymentsGrp_IsActive = @PActive,
				 ClmPaymentsGrp_ModifiedDate = GETDATE()
		where ClmPaymentsGrp_ID = @PPaymentGroupID;
END
GO