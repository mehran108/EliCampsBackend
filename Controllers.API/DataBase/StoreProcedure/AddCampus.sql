

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddCampus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[AddCampus] AS' 
END
GO
Alter PROCEDURE [dbo].[AddCampus] 
	-- Add the parameters for the stored procedure here
	@PCampus nvarchar(255),
	@PCampusCamps nvarchar(255),
	@PCampusAddressOnReports nvarchar(max),
	@PCampusCompleteName nvarchar(max),
	@PCampusOnelineaddress nvarchar(max),
	@PCampusID INT = NULL OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Insert Into [dbo].[tblCampuses]
				(clmCampuses_Campus,  clmCampuses_Camps,  clmCampuses_AddressOnReports, clmCampuses_CompleteName, clmCampuses_Onelineaddress,clmCampuses_CreateDate)
		Values	(@PCampus, @PCampusCamps, @PCampusAddressOnReports, @PCampusCompleteName, @PCampusOnelineaddress, GETDATE());

		SET @PCampusID = SCOPE_IDENTITY();
END
GO