
/****** Object:  Table [dbo].[tblYears]    Script Date: 16/11/2019 7:17:08 pm ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblYears](
	[clmYear_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmYear_Year] [int] NULL,
	[clmYear_StudentRef] [int] NULL,
	[clmYear_GroupRef] [int] NULL,
	[clmYear_isActive] [bit] Not NULL,
 CONSTRAINT [PK_tblYears] PRIMARY KEY CLUSTERED 
(
	[clmYear_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblYears] ADD  CONSTRAINT [DF_tblYears_clmYear_StudentRef]  DEFAULT ((0)) FOR [clmYear_StudentRef]
GO

ALTER TABLE [dbo].[tblYears] ADD  CONSTRAINT [DF_tblYears_clmYear_GroupRef]  DEFAULT ((0)) FOR [clmYear_GroupRef]
GO

ALTER TABLE [dbo].[tblYears] ADD  CONSTRAINT [DF_tblYears_clmYear_isActive]  DEFAULT ((1)) FOR [clmYear_isActive]
GO

