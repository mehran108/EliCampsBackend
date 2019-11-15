
/****** Object:  Table [dbo].[tblHomestay]    Script Date: 11/15/2019 4:35:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblHomestay](
	[clmHome_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmHome_Reference] [nvarchar](255) NULL,
	[clmHome_Name] [nvarchar](50) NULL,
	[clmHome_CellNumber] [nvarchar](50) NULL,
	[clmHome_Email] [nvarchar](255) NULL,
	[clmHome_Address] [nvarchar](255) NULL,
	[clmHome_Region] [nvarchar](50) NULL,
	[clmHome_Intersection] [nvarchar](50) NULL,
	[clmHome_Distance] [nvarchar](50) NULL,
	[clmHome_Meals] [nvarchar](50) NULL,
	[clmHome_Prefer] [nvarchar](50) NULL,
	[clmHome_Rooms] [nvarchar](50) NULL,
	[clmHome_Agreement] [nvarchar](50) NULL,
	[clmHome_PoliceCheck] [nvarchar](50) NULL,
	[clmHome_IsActive] [bit] NULL,
	[clmHome_CreateDate] [datetime] NULL,
	[clmHome_CreatedBy] [int] NULL,
	[clmHome_ModifiedDate] [datetime] NULL,
	[clmHome_ModifiedBy] [int] NULL,
 CONSTRAINT [PK_tblHomestay] PRIMARY KEY CLUSTERED 
(
	[clmHome_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO

ALTER TABLE [dbo].[tblHomestay] ADD  DEFAULT ((1)) FOR [clmHome_IsActive]
GO


