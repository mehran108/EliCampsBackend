

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivateCampus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ActivateCampus] AS' 
END
GO
Alter PROCEDURE [dbo].[ActivateCampus] 
	-- Add the parameters for the stored procedure here
	@PCampusID INt,
	@PActive bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update  [dbo].[tblCampuses]
	set 
	[clmCampuses_ModifiedDate] = GETDATE(),
	[clmCampuses_IsActive] = @PActive
	where [clmCampuses_ID] = @PCampusID;

END
GO