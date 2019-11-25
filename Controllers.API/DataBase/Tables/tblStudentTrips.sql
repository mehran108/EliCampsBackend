SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblStudentTrips')) 
BEGIN CREATE TABLE [dbo].[tblStudentTrips](
	[clmStTrips_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmStTrips_Student] [int] NOT NULL,
	[clmStTrips_Trip] [int] NOT NULL,
 CONSTRAINT [PK_tblStudentTrips] PRIMARY KEY CLUSTERED 
(
	[clmStTrips_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[tblStudentTrips]  ADD  CONSTRAINT [FK_tblStudentTrips_tblTrips] FOREIGN KEY([clmStTrips_Trip])
REFERENCES [dbo].[tblTrips] ([clmTrips_ID])

ALTER TABLE [dbo].[tblStudentTrips]  ADD  CONSTRAINT [FK_tblStudentTrips_tblRegistration] FOREIGN KEY([clmStTrips_Student])
REFERENCES [dbo].[tblRegistration] ([clmReg_ID])

END 
GO