USE [dbsample]
GO
/****** Object:  Table [dbo].[tbl_NonDBObjects]    Script Date: 05/05/2023 04:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_NonDBObjects](
	[ID] [int] NOT NULL,
	[Name] [varchar](max) NULL,
	[Remarks] [varchar](max) NULL,
	[EncBy] [int] NULL,
	[EncDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tbl_NonDBObjects] ADD  DEFAULT (getdate()) FOR [EncDate]
GO
ALTER TABLE [dbo].[tbl_NonDBObjects] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO
