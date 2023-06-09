USE [dbsample]
GO
/****** Object:  Table [dbo].[tbl_Contacts]    Script Date: 05/05/2023 04:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Contacts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EID] [int] NULL,
	[NetworkNo] [nvarchar](50) NULL,
	[NetworkTypeID] [int] NULL,
	[BinCard] [nvarchar](50) NULL,
	[Remarks] [nvarchar](max) NULL,
	[EncBy] [int] NULL,
	[EncDate] [datetime] NULL CONSTRAINT [DF_tbl_Contacts_EncDate]  DEFAULT (getdate()),
	[LastModifiedBy] [int] NULL,
	[LastModifiedDate] [datetime] NULL CONSTRAINT [DF_tbl_Contacts_LastModifiedDate]  DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
