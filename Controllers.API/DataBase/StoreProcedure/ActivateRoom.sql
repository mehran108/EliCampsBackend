

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivateGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ActivateGroup] AS' 
END
GO
Alter PROCEDURE [dbo].[ActivateGroup] 
	-- Add the parameters for the stored procedure here
	@PGroupID INT ,
	@PActive bit
AS
BEGIN
	SET NOCOUNT ON;

    update [dbo].[tblGroups]
				set clmGroups_Active = @PActive
		where clmGroups_ID = @PGroupID;
END
GO