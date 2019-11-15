SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblGroups')) 
BEGIN 
CREATE TABLE [dbo].[tblGroups](
	[clmGroups_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmGroups_Year] [int] NULL,
	[clmGroups_Camps] [nvarchar](255) NULL,
	[clmGroups_RefNumber] [nvarchar](255) NOT NULL,
	[clmGroups_AgentID] [int] NULL,
	[clmGroups_AgencyRef] [nvarchar](50) NULL,
	[clmGroups_Country] [nvarchar](50) NULL,
	[clmGroups_ArrivalDate] [date] NULL,
	[clmGroups_Terminal] [nvarchar](255) NULL,
	[clmGroups_FlightNumber] [nvarchar](255) NULL,
	[clmGroups_DestinationFrom] [nvarchar](255) NULL,
	[clmGroups_ArrivalTime] [nvarchar](50) NULL,
	[clmGroups_DepartureDate] [date] NULL,
	[clmGroups_DepartureTerminal] [nvarchar](255) NULL,
	[clmGroups_DepartureFlightNumber] [nvarchar](255) NULL,
	[clmGroups_DestinationTo] [nvarchar](255) NULL,
	[clmGroups_FlightDepartureTime] [nvarchar](50) NULL,
	[clmGroups_TotalGrossPrice] [money] NULL,
	[clmGroups_Paid] [money] NULL,
	[clmGroups_Commision] [numeric](5, 2) NULL,
	[clmGroups_NetPrice] [money] NULL,
	[clmGroups_Balance] [money] NULL,
	[clmGroups_NumberOfNights] [int] NULL,
	[clmGroups_ProgrameStartDate] [date] NULL,
	[clmGroups_ProgrameEndDate] [date] NULL,
	[clmGroups_Campus] [int] NULL,
	[clmGroups_Format] [int] NULL,
	[clmGroups_MealPlan] [nvarchar](50) NULL,
	[clmGroups_NumOfStudents] [int] NULL,
	[clmGroups_NumOfGrpLeaders] [int] NULL,
	[clmGroups_PerStudent] [money] NULL,
	[clmGroups_PerGrpLeader] [money] NULL,
	[clmGroups_InvType] [nvarchar](50) NULL,
	[clmGroups_InvoiceDate] [date] NULL,
	[clmGroups_Active] [bit] NOT NULL,
	[clmGroups_CreateDate] [DATETIME] NULL,
	[clmGroups_CreatedBy] [int] NULL,
	[clmGroups_ModifiedDate] [DATETIME] NULL,
	[clmGroups_ModifiedBy] [int] NULL
 CONSTRAINT [PK_tblGroups] PRIMARY KEY CLUSTERED 
(
	[clmGroups_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_TotalGrossPrice]  DEFAULT ((0)) FOR [clmGroups_TotalGrossPrice]


ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_Paid]  DEFAULT ((0)) FOR [clmGroups_Paid]


ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_Commision]  DEFAULT ((0)) FOR [clmGroups_Commision]


ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_NetPrice]  DEFAULT ((0)) FOR [clmGroups_NetPrice]


ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_Balance]  DEFAULT ((0)) FOR [clmGroups_Balance]


ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_NumberOfNights]  DEFAULT ((0)) FOR [clmGroups_NumberOfNights]


ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_MealPlan]  DEFAULT (N'') FOR [clmGroups_MealPlan]


ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_NumOfStudents]  DEFAULT ((0)) FOR [clmGroups_NumOfStudents]


ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_NumOfGrpLeaders]  DEFAULT ((0)) FOR [clmGroups_NumOfGrpLeaders]


ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_PerStudent]  DEFAULT ((0)) FOR [clmGroups_PerStudent]


ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_PerGrpLeader]  DEFAULT ((0)) FOR [clmGroups_PerGrpLeader]

ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF_tblGroups_clmGroups_Active]  DEFAULT ((1)) FOR [clmGroups_Active]

--Need you Add
--ALTER TABLE [dbo].[tblGroups]  WITH CHECK ADD  CONSTRAINT [FK_tblGroups_tblCamps] FOREIGN KEY([clmGroups_Camps])
--REFERENCES [dbo].[tblCamps] ([clmCamps_Camp])
--ON UPDATE CASCADE


--ALTER TABLE [dbo].[tblGroups] CHECK CONSTRAINT [FK_tblGroups_tblCamps]

END 
GO