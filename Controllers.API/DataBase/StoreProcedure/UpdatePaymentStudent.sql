

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdatePaymentStudent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdatePaymentStudent] AS' 
END
GO
Alter PROCEDURE [dbo].[UpdatePaymentStudent] 
	-- Add the parameters for the stored procedure here
	@PPaymentStudentID INT ,
	@PID INT,
	@PPaymentStudentDate date,
	@PPaymentStudentAmount money,
	@PPaymentStudentRemarks nvarchar(255),
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update [dbo].[tblPayments]
				set [ClmPayments_RegID] = @PID,  
				[ClmPayments_Date] = @PPaymentStudentDate, 
				[ClmPayments_Amount] = @PPaymentStudentAmount, 
				[ClmPayments_Remarks] = @PPaymentStudentRemarks,
				 [ClmPayments_IsActive] =  @PActive, 
				 [ClmPayments_ModifiedDate] = GETDATE()
		where [ClmPayments_ID] = @PPaymentStudentID;
END
GO