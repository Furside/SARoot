USE [dbsample]
GO
/****** Object:  Table [dbo].[tbl_Accounts]    Script Date: 05/05/2023 04:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Accounts](
	[HolderID] [int] NULL,
	[HolderType] [int] NULL,
	[StartDate] [datetime] NULL DEFAULT (getdate()),
	[EndDate] [datetime] NULL DEFAULT (getdate()),
	[EncBy] [int] NULL,
	[EncDate] [datetime] NULL DEFAULT (getdate()),
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL DEFAULT (getdate()),
	[Status] [varchar](max) NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
