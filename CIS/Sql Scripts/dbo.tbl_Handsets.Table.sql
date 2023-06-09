USE [dbsample]
GO
/****** Object:  Table [dbo].[tbl_Handsets]    Script Date: 05/05/2023 04:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Handsets](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HolderID] [int] NULL,
	[HolderType] [int] NULL,
	[Model] [int] NULL,
	[EncBy] [int] NULL,
	[EncDate] [datetime] NULL CONSTRAINT [DF__Handsets__EncDat__73BA3083]  DEFAULT (getdate()),
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL CONSTRAINT [DF__Handsets__Modifi__74AE54BC]  DEFAULT (getdate()),
	[DuoNo] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
