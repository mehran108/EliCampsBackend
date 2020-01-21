

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblPaymentsGroupLeader')) 
BEGIN CREATE TABLE  [dbo].[tblPaymentsGroupLeader](
	[ClmPaymentsGrp_ID] [int] IDENTITY(1,1) NOT NULL,
	[ClmPaymentsGrp_RefNumber] [nvarchar](255) NULL,
	[ClmPaymentsGrp_GroupID] [int] NULL,
	[ClmPaymentsGrp_Date] [date] NULL,
	[ClmPaymentsGrp_Amount] [money] NULL,
	[ClmPaymentsGrp_Remarks] [nvarchar](255) NULL,
	[ClmPaymentsGrp_IsActive] [bit] default 1,
	[ClmPaymentsGrp_CreateDate] [DATETIME] NULL,
	[ClmPaymentsGrp_CreatedBy] [int] NULL,
	[ClmPaymentsGrp_ModifiedDate] [DATETIME] NULL,
	[ClmPaymentsGrp_ModifiedBy] [int] NULL
CONSTRAINT [PK_tblPaymentsGroupLeader] PRIMARY KEY CLUSTERED 
(
	[ClmPaymentsGrp_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[tblPaymentsGroupLeader]  WITH CHECK ADD  CONSTRAINT [FK_tblPaymentsGroupLeader_tblRegistration] FOREIGN KEY([ClmPaymentsGrp_GroupID])
REFERENCES [dbo].[tblGroups] ([clmGroups_ID])


ALTER TABLE [dbo].[tblPaymentsGroupLeader] CHECK CONSTRAINT [FK_tblPaymentsGroupLeader_tblRegistration]

END 
GO