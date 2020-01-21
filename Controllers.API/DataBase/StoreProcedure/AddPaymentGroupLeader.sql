

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddPaymentGroupLeader]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddPaymentGroupLeader] AS' 
END
GO
Alter PROCEDURE [dbo].[AddPaymentGroupLeader] 
	-- Add the parameters for the stored procedure here
	@PPaymentGroupID INT = NULL OUTPUT,
	@PGroupID INT,
	@PRefNumber nvarchar(255),
	@PPaymentGroupAmount money
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblPaymentsGroupLeader]
				(ClmPaymentsGrp_RefNumber,ClmPaymentsGrp_GroupID,ClmPaymentsGrp_Amount,
				ClmPaymentsGrp_IsActive,ClmPaymentsGrp_CreateDate)
		Values	(@PRefNumber,@PGroupID,@PPaymentGroupAmount,
		1,GETDATE());

		SET @PPaymentGroupID = SCOPE_IDENTITY();
END
GO