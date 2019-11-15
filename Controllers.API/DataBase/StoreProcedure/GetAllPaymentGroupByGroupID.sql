

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllPaymentGroupByGroupID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllPaymentGroupByGroupID] AS' 
END
GO
Alter PROCEDURE [dbo].[GetAllPaymentGroupByGroupID] 
	-- Add the parameters for the stored procedure here
	@PGroupID INT
AS
BEGIN
	 

	 Select 
				[ClmPaymentsGrp_ID] As PaymentGroupID,
				[ClmPaymentsGrp_RefNumber] As RefNumber,  
				[ClmPaymentsGrp_GroupID] As GroupID, 
				[ClmPaymentsGrp_Date] As  PaymentGroupDate, 
				[ClmPaymentsGrp_Amount] As PaymentGroupAmount,
				[ClmPaymentsGrp_Remarks] As  PaymentGroupRemarks, 
				[ClmPaymentsGrp_IsActive] As Active
				
		from [dbo].[tblPaymentsGroups] 
		where [ClmPaymentsGrp_GroupID] = @PGroupID;
END
GO