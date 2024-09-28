

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivatePaymentGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ActivatePaymentGroup] AS' 
END
GO
Alter PROCEDURE [dbo].[ActivatePaymentGroup] 
	-- Add the parameters for the stored procedure here
	@PPaymentGroupID INT ,
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update [dbo].[tblPaymentsGroups]
				set 
				 ClmPaymentsGrp_IsActive = @PActive,
				 ClmPaymentsGrp_ModifiedDate = GETDATE()
		where ClmPaymentsGrp_ID = @PPaymentGroupID;

		DECLARE @PGroupID int = (Select top 1 ClmPaymentsGrp_GroupID from tblPaymentsGroups where ClmPaymentsGrp_ID = @PPaymentGroupID )


		 
		 if @PGroupID <> 0 and @PGroupID is not null
		 Begin
			update tblGroups set clmGroups_Paid = (
			Select Sum(ClmPaymentsGrp_Amount) from tblPaymentsGroups
			where ClmPaymentsGrp_GroupID = @PGroupID and ClmPaymentsGrp_IsActive = 1)
			where clmGroups_ID = @PGroupID;
		 END 
		
END
GO