

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAllCampus]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetAllCampus] AS' 
END
GO
Alter PROCEDURE [dbo].[GetAllCampus] 
	-- Add the parameters for the stored procedure here
	@POffset int, 
    @PPageSize int,
    @PSortColumn varchar(60),
    @PSortAscending bit
AS
BEGIN
	
     Select [clmCampuses_ID] As AgentId
      ,[clmCampuses_Campus] As Campus
      ,[clmCampuses_Camps] As CampusCamps
      ,[clmCampuses_AddressOnReports] As  CampusAddressOnReports
      ,[clmCampuses_CompleteName] As CampusCompleteName
      ,[clmCampuses_Onelineaddress] As CampusOnelineaddress
      ,[clmCampuses_IsActive] As Active
	 from [tblCampuses] ;
END
GO