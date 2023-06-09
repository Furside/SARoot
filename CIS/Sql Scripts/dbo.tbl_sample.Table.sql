USE [dbsample]
GO
/****** Object:  Table [dbo].[tbl_sample]    Script Date: 05/05/2023 04:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_sample](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[lname] [varchar](255) NULL,
	[fname] [varchar](255) NULL,
	[mname] [varchar](255) NULL,
	[content] [varchar](max) NULL,
	[encDate] [datetime] NULL CONSTRAINT [DF__tbl_sampl__encDa__0F975522]  DEFAULT (getdate())
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
