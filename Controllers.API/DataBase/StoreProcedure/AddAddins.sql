

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================

-- =============================================
-- Author:		<Author,,Zulqarnain>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddAddins]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddAddins] AS' 
END
GO
Alter PROCEDURE [dbo].[AddAddins] 
	-- Add the parameters for the stored procedure here
	

	@PAddins nvarchar(255),
	@PAddinsType nvarchar(50),
	@PAddinsCamps nvarchar(255),
	@PAddinsCost nvarchar(255),
	@PID INT = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblAddins]
				(clmAddins_Addin,  clmAddins_Camps, clmAddins_Cost, clmAddins_Type)
		Values	(@PAddins,        @PAddinsCamps,       @PAddinsCost,       @PAddinsType);

		SET @PID = SCOPE_IDENTITY();
END
GO