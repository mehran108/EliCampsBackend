

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddPaymentStudent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddPaymentStudent] AS' 
END
GO
Alter PROCEDURE [dbo].[AddPaymentStudent] 
	-- Add the parameters for the stored procedure here
	@PPaymentStudentID INT = NULL OUTPUT,
	@PID INT,
	@PPaymentStudentDate date,
	@PPaymentStudentAmount money,
	@PPaymentStudentRemarks nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblPayments]
				([ClmPayments_RegID],[ClmPayments_Date],[ClmPayments_Amount],[ClmPayments_Remarks],
				[ClmPayments_IsActive],[ClmPayments_CreateDate])
		Values	(@PID,@PPaymentStudentDate,@PPaymentStudentAmount,@PPaymentStudentRemarks,
		1,GETDATE());

		SET @PPaymentStudentID = SCOPE_IDENTITY();

		update tblRegistration set clmReg_Paid = (
		Select Sum(ClmPayments_Amount) from tblPayments
		where ClmPayments_RegID = @PID and ClmPayments_IsActive = 1)
		where clmReg_ID = @PID;
END
GO