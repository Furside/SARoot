USE [dbsample]
GO
/****** Object:  Table [dbo].[tbl_Contracts_NetworkPlans]    Script Date: 05/05/2023 04:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Contracts_NetworkPlans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ContractID] [int] NULL,
	[NetworkPlanID] [int] NULL,
	[EncBy] [int] NULL,
	[EncDate] [datetime] NULL CONSTRAINT [DF__Contracts__EncDa__68487DD7]  DEFAULT (getdate()),
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL CONSTRAINT [DF__Contracts__Modif__693CA210]  DEFAULT (getdate())
) ON [PRIMARY]

GO
