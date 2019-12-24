

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLookupValueList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetLookupValueList] AS' 
END
GO
Alter PROCEDURE [dbo].[GetLookupValueList] 
	-- Add the parameters for the stored procedure here
	@PLookupTable nvarchar(255)
AS
BEGIN
	

	 Select lv.id As Value,
		lv.name  
		from [dbo].[LookupValue] lv with (nolock)
		inner join  [dbo].[LookupTable] lt with (nolock) on lv.lookupTableId = lt.Id
		where lt.Name = @PLookupTable;
END
GO