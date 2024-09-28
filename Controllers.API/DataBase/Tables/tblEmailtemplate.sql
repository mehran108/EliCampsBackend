SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblEmailtemplate')) 
BEGIN CREATE TABLE  [dbo].[tblEmailtemplate](
	[EmailTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[EmailTemplateCode] [nvarchar](100) NOT NULL,
	[Subject] [nvarchar](300) NULL,
	[HTMLBody] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL ,
 CONSTRAINT [PK_tblEmailtemplate] PRIMARY KEY CLUSTERED 
(
	[EmailTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[tblEmailtemplate] ADD  CONSTRAINT [DF_tblEmailtemplate_Active]  DEFAULT ((1)) FOR [Active]

END 
GO
