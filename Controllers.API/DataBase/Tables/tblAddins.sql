
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



-- Adding column clmAddins_IsDefault

IF EXISTS (
 SELECT   * 
 FROM     sys.objects 
 WHERE    object_id = OBJECT_ID(N'[dbo].[tblAddins]')
)
BEGIN

    IF NOT EXISTS(
   SELECT *
   FROM sys.columns 
   WHERE Name = N'clmAddins_IsDefault'
   AND Object_ID = Object_ID(N'[dbo].[tblAddins]')
   )
 BEGIN
     ALTER TABLE tblAddins
	 ADD [clmAddins_IsDefault] [bit] NOT NULL default 0
 END

END


