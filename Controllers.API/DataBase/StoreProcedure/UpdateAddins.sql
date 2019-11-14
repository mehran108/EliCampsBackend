

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateAddins]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateAddins] AS' 
END
GO
Alter PROCEDURE [dbo].[UpdateAddins] 
	-- Add the parameters for the stored procedure here
	@PAddins nvarchar(255),
	@PAddinsType nvarchar(50),
	@PAddinsCamps nvarchar(255),
	@PID INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update  [dbo].[tblAddins]
	set clmAddins_Addin = @PAddins,
	clmAddins_Camps = @PAddinsCamps,
	clmAddins_Type = @PAddinsType
	where clmAddins_ID = @PID;

END
GO