USE [dbsample]
GO
/****** Object:  Table [dbo].[tbl_Contracts_Signatories]    Script Date: 05/05/2023 04:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Contracts_Signatories](
	[ContractID] [int] IDENTITY(1,1) NOT NULL,
	[SignatoryID] [int] NULL,
	[EncBy] [int] NULL,
	[EncDate] [datetime] NULL CONSTRAINT [DF__Contracts__EncDa__70DDC3D8]  DEFAULT (getdate()),
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL CONSTRAINT [DF__Contracts__Modif__71D1E811]  DEFAULT (getdate())
) ON [PRIMARY]

GO
