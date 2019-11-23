SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblSubPrograms')) 
BEGIN CREATE TABLE  [dbo].[tblSubPrograms](
	[clmSubPrograms_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmSubPrograms_ProgramID] [int] NOT NULL,
	[clmSubPrograms_Name] [nvarchar](255) NULL,
	[clmSubPrograms_IsActive] [bit] NULL,
	[clmSubPrograms_CreateDate] [datetime] NULL,
	[clmSubPrograms_CreatedBy] [int] NULL,
	[clmSubPrograms_ModifiedDate] [datetime] NULL,
	[clmSubPrograms_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_tblSubPrograms] PRIMARY KEY CLUSTERED 
(
	[clmSubPrograms_ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[tblSubPrograms] ADD  DEFAULT ((1)) FOR [clmSubPrograms_IsActive]

ALTER TABLE [dbo].[tblSubPrograms]  WITH CHECK ADD  CONSTRAINT [FK_tblSubPrograms_tblPrograms] FOREIGN KEY([clmSubPrograms_ProgramID])
REFERENCES [dbo].[tblPrograms] ([clmPrograms_ID])
END 