
/****** Object:  Table [dbo].[tblTrips]    Script Date: 11/15/2019 5:50:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblTrips](
	[clmTrips_ID] [int] IDENTITY(1,1) NOT NULL,
	[clmTrips_Year] [int] NULL,
	[clmTrips_Trip] [nvarchar](255) NULL,
	[clmTrips_Camps] [nvarchar](255) NULL,
	[clmTrips_Date] [date] NULL,
	[clmTrips_Notes] [ntext] NULL,
	[clmTrips_IDX] [nvarchar](350) NULL,
	[clmTrips_IsActive] [bit] default 1,
	[clmTrips_CreateDate] [DATETIME] NULL,
	[clmTrips_CreatedBy] [int] NULL,
	[clmTrips_ModifiedDate] [DATETIME] NULL,
	[clmTrips_ModifiedBy] [int] NULL
 CONSTRAINT [PK_tblTrips] PRIMARY KEY CLUSTERED 
(
	[clmTrips_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[tblTrips]  WITH CHECK ADD  CONSTRAINT [FK_tblTrips_tblCamps] FOREIGN KEY([clmTrips_Camps])
REFERENCES [dbo].[tblCamps] ([clmCamps_Camp])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[tblTrips] CHECK CONSTRAINT [FK_tblTrips_tblCamps]
GO


