

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivatePaymentGroupLeader]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ActivatePaymentGroupLeader] AS' 
END
GO
Alter PROCEDURE [dbo].[ActivatePaymentGroupLeader] 
	-- Add the parameters for the stored procedure here
	@PPaymentGroupID INT ,
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update [dbo].[tblPaymentsGroupLeader]
				set 
				 ClmPaymentsGrp_IsActive = @PActive,
				 ClmPaymentsGrp_ModifiedDate = GETDATE()
		where ClmPaymentsGrp_ID = @PPaymentGroupID;
END
GO