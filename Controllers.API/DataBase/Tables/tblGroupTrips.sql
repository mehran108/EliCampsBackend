SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblGroupTrips')) 
BEGIN CREATE TABLE  [dbo].[tblGroupTrips](
	[clmGpTrips_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmGpTrips_GroupRef] [nvarchar](255) NULL,
	[clmGpTrips_GroupID] [int] NOT NULL,
	[clmGpTrips_Trip] [int] NOT NULL
 CONSTRAINT [PK_tblGroupTrips] PRIMARY KEY CLUSTERED 
(
	[clmGpTrips_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[tblGroupTrips]  WITH CHECK ADD  CONSTRAINT [FK_tblGroupTrips_tblGroup] FOREIGN KEY([clmGpTrips_GroupID])
REFERENCES [dbo].[tblGroups] ([clmGroups_ID])


ALTER TABLE [dbo].[tblGroupTrips]  WITH CHECK ADD  CONSTRAINT [FK_tblGroupTrips_tblTrips] FOREIGN KEY([clmGpTrips_Trip])
REFERENCES [dbo].[tblTrips] ([clmTrips_ID])


END 
GO