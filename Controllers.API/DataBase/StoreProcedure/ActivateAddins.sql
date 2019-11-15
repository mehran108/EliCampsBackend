

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivateAddins]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ActivateAddins] AS' 
END
GO
Alter PROCEDURE [dbo].[ActivateAddins] 
	-- Add the parameters for the stored procedure here
	@PID INT ,
	@PActive bit
AS
BEGIN
	SET NOCOUNT ON;

    update [dbo].[tblAddins]
				set clmAddins_IsActive = @PActive
		where clmAddins_ID = @PID;
END
GO