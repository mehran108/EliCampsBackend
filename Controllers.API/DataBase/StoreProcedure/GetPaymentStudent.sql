

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPaymentStudent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetPaymentStudent] AS' 
END
GO
Alter PROCEDURE [dbo].[GetPaymentStudent] 
	-- Add the parameters for the stored procedure here
	@PPaymentStudentID INT
AS
BEGIN
	 

	 Select 
				[ClmPayments_ID] As PaymentStudentID,
				[ClmPayments_RegID] As ID,  
				[ClmPayments_Date] As PaymentStudentDate, 
				[ClmPayments_Amount] As  PaymentStudentAmount, 
				[ClmPayments_Remarks] As PaymentStudentRemarks,
				[ClmPayments_IsActive] As  Active
				
		from [dbo].[tblPayments] 
		where [ClmPayments_ID] = @PPaymentStudentID;
END
GO