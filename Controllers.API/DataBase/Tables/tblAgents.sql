SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblAgents')) 
BEGIN CREATE TABLE  [dbo].[tblAgents](
	[clmAgents_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmAgents_Agent] [nvarchar](255) NULL,
	[clmAgents_Contact] [nvarchar](255) NULL,
	[clmAgents_Phone] [nvarchar](255) NULL,
	[clmAgents_Email] [nvarchar](255) NULL,
	[clmAgents_Web] [nvarchar](255) NULL,
	[clmAgents_Address] [nvarchar](500) NULL,
	[clmAgents_Country] [nvarchar](50) NULL,
	[clmAgents_Notes] [ntext] NULL,
	[clmAgents_Other] [nvarchar](255) NULL,
	[clmAgents_IsActive] [bit] default 1,
	[clmAgents_CreateDate] [DATETIME] NULL,
	[clmAgents_CreatedBy] [int] NULL,
	[clmAgents_ModifiedDate] [DATETIME] NULL,
	[clmAgents_ModifiedBy] [int] NULL
 CONSTRAINT [PK_tblAgents] PRIMARY KEY CLUSTERED 
(
	[clmAgents_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END 
GO