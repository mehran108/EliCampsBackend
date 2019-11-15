

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddPaymentGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddPaymentGroup] AS' 
END
GO
Alter PROCEDURE [dbo].[AddPaymentGroup] 
	-- Add the parameters for the stored procedure here
	@PPaymentGroupID INT = NULL OUTPUT,
	@PGroupID INT,
	@PRefNumber nvarchar(255),
	@PPaymentGroupDate date,
	@PPaymentGroupAmount money,
	@PPaymentGroupRemarks nvarchar(255)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblPaymentsGroups]
				(ClmPaymentsGrp_RefNumber,ClmPaymentsGrp_GroupID,ClmPaymentsGrp_Date,ClmPaymentsGrp_Amount,
				ClmPaymentsGrp_Remarks,ClmPaymentsGrp_IsActive,ClmPaymentsGrp_CreateDate)
		Values	(@PRefNumber,@PGroupID,@PPaymentGroupDate,@PPaymentGroupAmount,@PPaymentGroupRemarks,
		1,GETDATE());

		SET @PPaymentGroupID = SCOPE_IDENTITY();
END
GO