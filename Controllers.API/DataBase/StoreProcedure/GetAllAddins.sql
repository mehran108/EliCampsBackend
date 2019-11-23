

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllAddins]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllAddins] AS' 
END
GO
Alter PROCEDURE [dbo].[GetAllAddins] 
	-- Add the parameters for the stored procedure here
	
	@PActive bit
AS
BEGIN
	

   Select [clmAddins_ID] As ID
      ,[clmAddins_Addin] As Addins
      ,[clmAddins_Camps] As AddinsCamps
      ,[clmAddins_Type] As AddinsType,
	  clmAddins_IsActive AS Active
	 from tblAddins
	  where ( clmAddins_IsActive = (CASE WHEN @PActive is not null then @PActive else clmAddins_IsActive end));
END
GO