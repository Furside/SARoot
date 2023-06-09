USE [dbsample]
GO
/****** Object:  Table [dbo].[tbl_Contracts]    Script Date: 05/05/2023 04:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Contracts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountNo] [varchar](50) NULL,
	[StartDate] [datetime] NULL CONSTRAINT [DF__Contracts__Start__5BE2A6F2]  DEFAULT (getdate()),
	[EndDate] [datetime] NULL CONSTRAINT [DF__Contracts__EndDa__5CD6CB2B]  DEFAULT (getdate()),
	[Description] [varchar](max) NULL,
	[Status] [varchar](50) NULL,
	[Offer] [varchar](max) NULL,
	[Holdertype] [int] NULL,
	[HolderID] [int] NULL,
	[EncBy] [int] NULL,
	[EncDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
