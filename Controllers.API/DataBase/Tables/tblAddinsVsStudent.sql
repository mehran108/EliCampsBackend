
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblAddinsVsStudent')) 
BEGIN CREATE TABLE [dbo].[tblAddinsVsStudent](
	[clmAdvsSt_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmAdvsSt_StudentID] [int] NOT NULL,
	[clmAdvsSt_AddinsID] [int] NOT NULL,
	[clmAdvsSt_Price] [money] NULL,
 CONSTRAINT [PK_tblAddinsVsStudent] PRIMARY KEY CLUSTERED 
(
	[clmAdvsSt_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


ALTER TABLE [dbo].[tblAddinsVsStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblAddinsVsStudent_tblAddins] FOREIGN KEY([clmAdvsSt_AddinsID])
REFERENCES [dbo].[tblAddins] ([clmAddins_ID])
ON UPDATE CASCADE


ALTER TABLE [dbo].[tblAddinsVsStudent] CHECK CONSTRAINT [FK_tblAddinsVsStudent_tblAddins]


ALTER TABLE [dbo].[tblAddinsVsStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblAddinsVsStudent_tblRegistration] FOREIGN KEY([clmAdvsSt_StudentID])
REFERENCES [dbo].[tblRegistration] ([clmReg_ID])


ALTER TABLE [dbo].[tblAddinsVsStudent] CHECK CONSTRAINT [FK_tblAddinsVsStudent_tblRegistration]


END 
GO