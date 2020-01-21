

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllPaymentStudentByGroupID]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllPaymentStudentByGroupID] AS' 
END
GO
Alter PROCEDURE [dbo].[GetAllPaymentStudentByGroupID] 
	-- Add the parameters for the stored procedure here
	@PID INT
AS
BEGIN
	 
	 Select 
				pay.[ClmPayments_ID] As PaymentStudentID,
				pay.[ClmPayments_RegID] As ID,  
				pay.[ClmPayments_Date] As PaymentStudentDate, 
				pay.[ClmPayments_Amount] As  PaymentStudentAmount, 
				pay.[ClmPayments_Remarks] As PaymentStudentRemarks,
				pay.[ClmPayments_IsActive] As  Active
		from tblGroups grp with(nolock)
		inner join tblRegistration reg with(nolock) on grp.clmGroups_ID = reg.GroupID
		inner join tblPayments pay with(nolock) on pay.ClmPayments_RegID = reg.clmReg_ID
		where grp.clmGroups_ID = @PID and reg.clmReg_ChapFamily = 'Chaperone'
		order by pay.ClmPayments_RegID, pay.[ClmPayments_ID] desc;
END
GO