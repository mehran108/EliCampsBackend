SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblAddinsVsGropCam')) 
BEGIN CREATE TABLE  [dbo].[tblAddinsVsGropCam](
	[clmAdvsGrpCam_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmAdvsGrpCam_GroupRef] [nvarchar](255) NULL,
	[clmAdvsGrpCam_AddinsID] [int] NULL,
	[clmAdvsGrpCam_GroupID] [int] NULL,
	[clmAdvsGrpCam_Price] [money] NULL,
 CONSTRAINT [PK_tblAddinsVsGropCam] PRIMARY KEY CLUSTERED 
(
	[clmAdvsGrpCam_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[tblAddinsVsGropCam]  WITH CHECK ADD  CONSTRAINT [FK_tblAddinsVsGropCam_tblAddins] FOREIGN KEY([clmAdvsGrpCam_AddinsID])
REFERENCES [dbo].[tblAddins] ([clmAddins_ID])


ALTER TABLE [dbo].[tblAddinsVsGropCam]  WITH CHECK ADD  CONSTRAINT [FK_tblAddinsVsGropCam_tblGroups] FOREIGN KEY([clmAdvsGrpCam_GroupID])
REFERENCES [dbo].[tblGroups] ([clmGroups_ID])

END 
GO