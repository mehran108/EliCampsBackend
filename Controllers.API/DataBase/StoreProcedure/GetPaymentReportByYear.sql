USE [DB_A4427D_elicamps1]
GO
/****** Object:  StoredProcedure [dbo].[GetPaymentReportByYear]    Script Date: 3/28/2020 11:19:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetPaymentReportByYear] 
	-- Add the parameters for the stored procedure herez
	@PYear varchar(50)

AS
BEGIN
	
select
tbl.[clmReg_ID] as ID,
tbl.[clmReg_Year] as Year,
tbl.[clmReg_FirstName] as FirstName,
tbl.[clmReg_LastName] as LastName,
tbl.[clmReg_AgencyID] as AgencyID,
tbl.clmReg_Campus as Campus,
tbl.[clmReg_Status] as Active,
tbl.clmReg_Ref as Reg_Ref,
[dbo].[tblCampuses].[clmCampuses_Campus] as CampusName,
tbl.clmReg_Format as Format,
dbo.tblAgents.clmAgents_Agent  AS AgentName,
[dbo].[LookupValue].name as FormatName,
[dbo].[tblPrograms].[clmPrograms_Name] as ProgramName,
tbl.[clmReg_TotalAddins] as TotalAddins,
tbl.[clmReg_CommissionAddins] as CommissionAddins,
tbl.[clmReg_TotalGrossPrice] as TotalGrossPrice,
tbl.clmReg_NetPrice AS NetPrice,
tbl.[clmReg_Commision] AS Commision,
tbl.[clmReg_Paid] as Paid,
tbl.[clmReg_Balance] AS Balance
 from [dbo].[tblRegistration] as tbl
 left outer join dbo.tblPayments on tbl.clmReg_ID=dbo.tblPayments.ClmPayments_RegID
 left outer join dbo.tblAgents on  tbl.clmReg_AgencyID = dbo.tblAgents.clmAgents_ID
 left outer join [dbo].[LookupValue] on tbl.clmReg_Format = [dbo].[LookupValue].[id]
 left outer join [dbo].[tblCampuses] on tbl.clmReg_Campus = [dbo].[tblCampuses].[clmCampuses_ID]
 left outer join [dbo].[tblPrograms] on tbl.[clmReg_ProgramID] = [dbo].[tblPrograms].[clmPrograms_ID]
 where tbl.[clmReg_year] = @PYear
order by tbl.[clmReg_ID] desc;
END
