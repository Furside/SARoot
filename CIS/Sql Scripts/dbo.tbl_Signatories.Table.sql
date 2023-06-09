USE [dbsample]
GO
/****** Object:  Table [dbo].[tbl_Signatories]    Script Date: 05/05/2023 04:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Signatories](
	[ID] [int] NOT NULL,
	[EncBy] [int] NULL,
	[EncDate] [datetime] NULL DEFAULT (getdate()),
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL DEFAULT (getdate()),
	[EID] [int] NULL
) ON [PRIMARY]

GO
