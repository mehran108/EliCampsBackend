

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateProgram]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[UpdateProgram] AS' 
END
GO
Alter PROCEDURE [dbo].[UpdateProgram] 
	-- Add the parameters for the stored procedure here
	@PName nvarchar(255),
	@PProgramID INT,
	@PActive bit,
	@PIsDefault bit
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    update  [dbo].[tblPrograms]
	set [clmPrograms_Name] = @PName,
	[clmPrograms_ModifiedDate] = GETDATE(),
	[clmPrograms_IsActive] = @PActive,
	clmPrograms_IsDefault = @PIsDefault
	where [clmPrograms_ID] = @PProgramID;

		--update IsDefault false if IsDefault true for current record
		 if @PIsDefault = 1 
		 Begin
			update tblPrograms set clmPrograms_IsDefault = 0 where clmPrograms_ID <> @PProgramID;
		 END 

END
GO