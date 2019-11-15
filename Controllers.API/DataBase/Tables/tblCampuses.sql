SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblCampuses')) 
BEGIN CREATE TABLE  [dbo].[tblCampuses](
	[clmCampuses_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmCampuses_Campus] [nvarchar](255) NULL,
	[clmCampuses_Camps] [nvarchar](255) NULL,
	[clmCampuses_AddressOnReports] [nvarchar](max) NULL,
	[clmCampuses_CompleteName] [nvarchar](max) NULL,
	[clmCampuses_Onelineaddress] [nvarchar](max) NULL,
	[clmAgents_IsActive] [bit] default 1,
	
 CONSTRAINT [PK_tblCampuses] PRIMARY KEY CLUSTERED 
(
	[clmCampuses_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END 
GO