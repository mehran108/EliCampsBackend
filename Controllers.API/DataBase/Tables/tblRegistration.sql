SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblRegistration')) 
BEGIN CREATE TABLE  [dbo].[tblRegistration](
	[clmReg_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmReg_Year] [int] NULL,
	[clmReg_Ref] [nvarchar](255) NULL,
	[clmReg_GrpRef] [nvarchar](255) NULL,
	[clmReg_Camps] [nvarchar](255) NULL,
	[clmReg_Gender] [nvarchar](255) NULL,
	[clmReg_FirstName] [nvarchar](255) NULL,
	[clmReg_LastName] [nvarchar](255) NULL,
	[clmReg_HomeAddress] [nvarchar](255) NULL,
	[clmReg_City] [nvarchar](255) NULL,
	[clmReg_State] [nvarchar](255) NULL,
	[clmReg_Country] [nvarchar](255) NULL,
	[clmReg_PostCode] [nvarchar](255) NULL,
	[clmReg_EmergencyContact] [nvarchar](255) NULL,
	[clmReg_Email] [nvarchar](255) NULL,
	[clmReg_Phone] [nvarchar](255) NULL,
	[clmReg_DOB] [date] NULL,
	[clmReg_Age] [int] NULL,
	[clmReg_PassportNumber] [nvarchar](255) NULL,
	[clmReg_AgencyID] [int] NULL,
	[clmReg_ArrivalDate] [date] NULL,
	[clmReg_Terminal] [nvarchar](255) NULL,
	[clmReg_FlightNumber] [nvarchar](255) NULL,
	[clmReg_DestinationFrom] [nvarchar](255) NULL,
	[clmReg_ArrivalTime] [datetime] NULL,
	[clmReg_DepartureDate] [date] NULL,
	[clmReg_DepartureTerminal] [nvarchar](255) NULL,
	[clmReg_DepartureFlightNumber] [nvarchar](255) NULL,
	[clmReg_DestinationTo] [nvarchar](255) NULL,
	[clmReg_FlightDepartureTime] [datetime] NULL,
	[clmReg_MedicalInformation] [ntext] NULL,
	[clmReg_DietaryNeeds] [ntext] NULL,
	[clmReg_Allergies] [ntext] NULL,
	[clmRerameStartDatg_Notes] [ntext] NULL,
	[clmReg_Proge] [date] NULL,
	[clmReg_ProgrameEndDate] [date] NULL,
	[clmReg_Campus] [int] NULL,
	[clmReg_Format] [int] NULL,
	[clmReg_MealPlan] [nvarchar](255) NULL,
	[clmReg_NumberOfNights] [int] NULL,
	[clmReg_TotalGrossPrice] [money] NULL,
	[clmReg_Incentives] [money] NULL,
	[clmReg_Paid] [money] NULL,
	[clmReg_Commision] [numeric](18, 5) NULL,
	[clmReg_NetPrice] [money] NULL,
	[clmReg_Balance] [money] NULL,
	[clmReg_TotalAddins] [money] NULL,
	[clmReg_CommissionAddins] [money] NULL,
	[clmReg_ProfilePic] [nvarchar](300) NULL,
	[clmReg_RoomID] [int] NULL,
	[clmReg_HomestayOrResi] [nvarchar](50) NULL,
	[clmReg_HomestayID] [int] NULL,
	[clmReg_Ac_Academic] [int] NULL,
	[clmReg_Ac_Acco] [int] NULL,
	[clmReg_AcaPrice] [money] NULL,
	[clmReg_AcaWeeks] [int] NULL,
	[clmReg_AcaAmount] [money] NULL,
	[clmReg_Acco_Price] [money] NULL,
	[clmReg_Acco_Days] [numeric](18, 2) NULL,
	[clmReg_Acco_Amount] [money] NULL,
	[clmReg_AcaGrandTotal] [money] NULL,
	[clmReg_Status] [nvarchar](50) NULL,
	[clmReg_ExtraNotes] [ntext] NULL,
	[clmReg_ExtraNotesHTML] [ntext] NULL,
	[clmReg_RoomSearchCampus] [int] NULL,
	[clmReg_RoomSearchFrom] [date] NULL,
	[clmReg_RoomSearchTo] [date] NULL,
	[clmReg_InvoiceDate] [date] NULL,
	[clmReg_IsActive] [bit] NOT NULL ,
	[clmReg_CreateDate] [DATETIME] NULL,
	[clmReg_CreatedBy] [int] NULL,
	[clmReg_ModifiedDate] [DATETIME] NULL,
	[clmReg_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_tblRegistration] PRIMARY KEY CLUSTERED 
(
	[clmReg_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_TotalGrossPrice]  DEFAULT ((0)) FOR [clmReg_TotalGrossPrice]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_Incentives]  DEFAULT ((0)) FOR [clmReg_Incentives]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_Paid]  DEFAULT ((0)) FOR [clmReg_Paid]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_Commision]  DEFAULT ((0)) FOR [clmReg_Commision]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_NetPrice]  DEFAULT ((0)) FOR [clmReg_NetPrice]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_Balance]  DEFAULT ((0)) FOR [clmReg_Balance]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_TotalAddins]  DEFAULT ((0)) FOR [clmReg_TotalAddins]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_CommissionAddins]  DEFAULT ((0)) FOR [clmReg_CommissionAddins]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_HomestayID]  DEFAULT ((0)) FOR [clmReg_HomestayID]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_Ac_Academic]  DEFAULT ((0)) FOR [clmReg_Ac_Academic]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_Ac_Acco]  DEFAULT ((0)) FOR [clmReg_Ac_Acco]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_AcaPrice]  DEFAULT ((0)) FOR [clmReg_AcaPrice]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_AcaWeeks]  DEFAULT ((0)) FOR [clmReg_AcaWeeks]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_AcaAmount]  DEFAULT ((0)) FOR [clmReg_AcaAmount]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_Acco_Price]  DEFAULT ((0)) FOR [clmReg_Acco_Price]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_Acco_Days]  DEFAULT ((0)) FOR [clmReg_Acco_Days]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_Acco_Amount]  DEFAULT ((0)) FOR [clmReg_Acco_Amount]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_AcaGrandTotal]  DEFAULT ((0)) FOR [clmReg_AcaGrandTotal]


ALTER TABLE [dbo].[tblRegistration] ADD  CONSTRAINT [DF_tblRegistration_clmReg_IsActive]  DEFAULT ((1)) FOR [clmReg_IsActive]

END 
GO


-- Adding column clmReg_ChapFamily

IF EXISTS (
 SELECT   * 
 FROM     sys.objects 
 WHERE    object_id = OBJECT_ID(N'[dbo].[tblRegistration]')
)
BEGIN

    IF NOT EXISTS(
   SELECT *
   FROM sys.columns 
   WHERE Name = N'clmReg_ChapFamily'
   AND Object_ID = Object_ID(N'[dbo].[tblRegistration]')
   )
 BEGIN
     ALTER TABLE tblRegistration
	 ADD [clmReg_ChapFamily] [nvarchar](100) NULL
 END

END

-- Adding column  clmReg_ProgramID

IF EXISTS (
 SELECT   * 
 FROM     sys.objects 
 WHERE    object_id = OBJECT_ID(N'[dbo].[tblRegistration]')
)
BEGIN

    IF NOT EXISTS(
   SELECT *
   FROM sys.columns 
   WHERE Name = N'clmReg_ProgramID'
   AND Object_ID = Object_ID(N'[dbo].[tblRegistration]')
   )
 BEGIN
     ALTER TABLE tblRegistration
	 ADD [clmReg_ProgramID] [int] NULL
 END

END

-- Adding column  clmReg_SubProgramID

IF EXISTS (
 SELECT   * 
 FROM     sys.objects 
 WHERE    object_id = OBJECT_ID(N'[dbo].[tblRegistration]')
)
BEGIN

    IF NOT EXISTS(
   SELECT *
   FROM sys.columns 
   WHERE Name = N'clmReg_SubProgramID'
   AND Object_ID = Object_ID(N'[dbo].[tblRegistration]')
   )
 BEGIN
     ALTER TABLE tblRegistration
	 ADD [clmReg_SubProgramID] [int] NULL
 END

END
