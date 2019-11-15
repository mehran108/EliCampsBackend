USE [DB_A4427D_elicamps]
GO

/****** Object:  Table [dbo].[tblAddins]    Script Date: 11/14/2019 12:09:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblAddins](
	[clmAddins_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmAddins_Addin] [nvarchar](255) NULL,
	[clmAddins_Camps] [nvarchar](255) NULL,
	[clmAddins_Cost] [money] NULL,
	[clmAddins_Type] [nvarchar](50) NULL,
	[clmAddins_IsActive] [bit] default 1,
	[clmAddins_CreateDate] [DATETIME] NULL,
	[clmAddins_CreatedBy] [int] NULL,
	[clmAddins_ModifiedDate] [DATETIME] NULL,
	[clmAddins_ModifiedBy] [int] NULL
 CONSTRAINT [PK_tblAddins] PRIMARY KEY CLUSTERED 
(
	[clmAddins_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblAddins]  WITH CHECK ADD  CONSTRAINT [FK_tblAddins_tblCamps] FOREIGN KEY([clmAddins_Camps])
REFERENCES [dbo].[tblCamps] ([clmCamps_Camp])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[tblAddins] CHECK CONSTRAINT [FK_tblAddins_tblCamps]
GO


