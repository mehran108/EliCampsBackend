

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivateHomeStay]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ActivateHomeStay] AS' 
END
GO
Alter PROCEDURE [dbo].[ActivateHomeStay] 
	-- Add the parameters for the stored procedure here
	@PID INT ,
	@PActive bit
AS
BEGIN
	SET NOCOUNT ON;

    update [dbo].[tblHomestay]
				set clmHome_IsActive = @PActive
		where clmHome_ID = @PID;
END
GO