

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivatePaymentStudent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ActivatePaymentStudent] AS' 
END
GO
Alter PROCEDURE [dbo].[ActivatePaymentStudent] 
	-- Add the parameters for the stored procedure here
	@PPaymentStudentID INT ,
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update [dbo].[tblPayments]
				set 
				 [ClmPayments_IsActive] =  @PActive, 
				 [ClmPayments_ModifiedDate] = GETDATE()
		where [ClmPayments_ID] = @PPaymentStudentID;

	DECLARE @PID int = (Select top 1 ClmPayments_RegID from tblPayments where ClmPayments_ID = @PPaymentStudentID )


		 
		 if @PID <> 0 and @PID is not null
		 Begin
			update tblRegistration set clmReg_Paid = (
			Select Sum(ClmPayments_Amount) from tblPayments
			where ClmPayments_RegID = @PID and ClmPayments_IsActive = 1)
			where clmReg_ID = @PID;
		 END 
END
GO