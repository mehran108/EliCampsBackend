

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ActivateStudent]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ActivateStudent] AS' 
END
GO
Alter PROCEDURE [dbo].[ActivateStudent] 
	-- Add the parameters for the stored procedure here
	@PID INT,
	@PActive  bit
	
AS
BEGIN
	
	--update tblRegistration data
	update  [dbo].[tblRegistration]
	set 
	[clmReg_IsActive] = @PActive,
	[clmReg_ModifiedDate] = GETDATE()
	where [clmReg_ID] = @PID;
				
END
GO