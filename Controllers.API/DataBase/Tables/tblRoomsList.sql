



/****** Object:  Table [dbo].[tblRoomsList]    Script Date: 11/16/2019 12:08:40 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblRoomsList](
	[clmRoom_ID] [int] NOT NULL,
	[clmRoom_RoomID] [nvarchar](255) NULL,
	[clmRoom_Campus] [int] NULL,
	[clmRoom_RoomType] [nvarchar](255) NULL,
	[clmRoom_Building] [nvarchar](255) NULL,
	[clmRoom_Floor] [nvarchar](255) NULL,
	[clmRoom_Idx] [nvarchar](255) NULL,
	[clmRoom_Notes] [nvarchar](255) NULL,
	[clmRoom_BookedFrom] [date] NULL,
	[clmRoom_BookedTo] [date] NULL,
	[clmRoom_Available] [bit] NULL,
	[clmRoom_AvailableFrom] [date] NULL,
	[clmRoom_AvailableTo] [date] NULL,
	[clmRoom_ImportedOne] [int] NULL,
	[clmRoom_Weekno] [nvarchar](50) NULL,
	[clmRoom_Year] [int] NULL,
	[clmRoom_IsActive] [bit] default 1,
	[clmRoom_CreateDate] [DATETIME] NULL,
	[clmRoom_CreatedBy] [int] NULL,
	[clmRoom_ModifiedDate] [DATETIME] NULL,
	[clmRoom_ModifiedBy] [int] NULL
 CONSTRAINT [PK_tblRoomsList] PRIMARY KEY CLUSTERED 
(
	[clmRoom_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO




