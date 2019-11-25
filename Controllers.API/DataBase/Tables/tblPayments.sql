SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES  WHERE TABLE_NAME = 'tblPayments')) 
BEGIN CREATE TABLE [dbo].[tblPayments](
	[ClmPayments_ID] [int] IDENTITY(1,1) NOT NULL,
	[ClmPayments_RegID] [int] NOT NULL,
	[ClmPayments_Date] [date] NULL,
	[ClmPayments_Amount] [money] NULL,
	[ClmPayments_Remarks] [nvarchar](255) NULL,
 CONSTRAINT [PK_tblPayments] PRIMARY KEY CLUSTERED 
(
	[ClmPayments_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[tblPayments]  ADD  CONSTRAINT [FK_tblPayments_tblRegistration] FOREIGN KEY([ClmPayments_RegID])
REFERENCES [dbo].[tblRegistration] ([clmReg_ID])

END 
GO