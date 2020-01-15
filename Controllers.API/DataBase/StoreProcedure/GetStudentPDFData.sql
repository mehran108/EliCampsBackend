

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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetStudentPDFData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[GetStudentPDFData] AS' 
END
GO
Alter PROCEDURE [dbo].[GetStudentPDFData] 
	-- Add the parameters for the stored procedure here
	@PID INT
AS
BEGIN

	Select 
	tbl.[clmReg_FirstName] AS FirstName ,
	tbl.[clmReg_LastName] As LastName,
	tbl.clmReg_Email As Email,
	agents.clmAgents_Agent AS AgentName,
	agents.clmAgents_Address AS AgentAddress,
	agents.clmAgents_Country AS AgentCountry,
	tbl.[clmReg_Ref] As Reg_Ref,
	tbl.clmReg_DOB AS DOB,
	tbl.clmReg_Proge AS ProgrameStartDate,
	tbl.clmReg_ProgrameEndDate  AS ProgrameEndDate
	,cam.clmCampuses_AddressOnReports AS CampusAddressOnReports
	,pro.clmPrograms_Name AS ProgramName
	,subPro.clmSubPrograms_Name AS SubProgramName
	,lv.name AS FormatName
	,tbl.[clmReg_MealPlan] AS MealPlan
	,tbl.[clmReg_TotalGrossPrice] AS TotalGrossPrice 
	,tbl.[clmReg_CommissionAddins] AS CommissionAddins
	,tbl.[clmReg_Commision] AS Commision
	,tbl.[clmReg_Paid] AS Paid 
	,tbl.[clmReg_Balance] AS Balance
	,tbl.clmReg_NetPrice AS NetPrice
	,tbl.clmReg_ArrivalDate AS ArrivalDate
	,tbl.clmReg_ArrivalTime AS ArrivalTime
	,tbl.clmReg_FlightNumber as FlightNumber
	,tbl.[clmReg_Country] as Country

	from [dbo].[tblRegistration] tbl with (nolock)
	left join [tblAgents] agents with (nolock) on tbl.[clmReg_AgencyID] = agents.clmAgents_ID
	left join [dbo].[tblCampuses] cam with (nolock) on tbl.[clmReg_Campus] = cam.clmCampuses_ID
	left join tblPrograms pro with (nolock) on pro.clmPrograms_ID = tbl.clmReg_ProgramID
	left join tblSubPrograms subPro with (nolock) on subPro.clmSubPrograms_ID  = tbl.clmReg_SubProgramID
	left join [LookupValue] lv with (nolock) on tbl.[clmReg_Format] = lv.id
	where tbl.[clmReg_ID] = @PID;


	Select distinct addin.clmAddins_Addin AS AddinName, addin.clmAddins_Type AS AddinsType from [dbo].[tblAddinsVsStudent]  addStu with(nolock)
	inner join tblRegistration  reg with (nolock) on reg.clmReg_ID = addStu.clmAdvsSt_StudentID and reg.clmReg_ID = @PID
	inner join [tblAddins] addin with(nolock) on addStu.clmAdvsSt_AddinsID = addin.clmAddins_ID


	
END
GO