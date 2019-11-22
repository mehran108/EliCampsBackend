SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblPrograms')) 
BEGIN CREATE TABLE  [dbo].[tblPrograms](
	[clmPrograms_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmPrograms_Name] [nvarchar](255) NULL,
	[clmPrograms_IsActive] [bit] default 1,
	[clmPrograms_CreateDate] [DATETIME] NULL,
	[clmPrograms_CreatedBy] [int] NULL,
	[clmPrograms_ModifiedDate] [DATETIME] NULL,
	[clmPrograms_ModifiedBy] [int] NULL
	
 CONSTRAINT [PK_tblPrograms] PRIMARY KEY CLUSTERED 
(
	[clmPrograms_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END 
GO