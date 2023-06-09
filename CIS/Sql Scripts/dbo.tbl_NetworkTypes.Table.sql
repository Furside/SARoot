USE [dbsample]
GO
/****** Object:  Table [dbo].[tbl_NetworkTypes]    Script Date: 05/05/2023 04:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_NetworkTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](max) NULL,
	[EncBy] [int] NULL,
	[EncDate] [datetime] NULL CONSTRAINT [DF_tbl_NetworkTypes_EncDate]  DEFAULT (getdate()),
	[LastModifiedBy] [int] NULL,
	[LastModifiedDate] [datetime] NULL CONSTRAINT [DF_tbl_NetworkTypes_LastModifiedDate]  DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
