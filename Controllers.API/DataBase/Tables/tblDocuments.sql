SET ANSI_NULLS ON  
GO  
SET QUOTED_IDENTIFIER ON  
GO  
CREATE TABLE [dbo].[tblDocuments](  
 [documentId] [int] IDENTITY(1,1) NOT NULL,  
 [documentName] [nvarchar](max) NOT NULL,  
 [documentPath] [nvarchar](max) NOT NULL,
 [clmReg_ID] [int] not null, 
 [IsActive] [bit] NULL,
	[CreateDate] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
 FOREIGN KEY (clmReg_ID) REFERENCES tblRegistration(clmReg_ID),
 CONSTRAINT [PK_tbl_file] PRIMARY KEY CLUSTERED   
(  
 [documentId] ASC  
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]  