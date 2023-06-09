USE [dbsample]
GO
/****** Object:  Table [dbo].[tbl_NetworkPlans]    Script Date: 05/05/2023 04:16:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_NetworkPlans](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NetworkID] [int] NULL,
	[Description] [varchar](max) NULL,
	[Combo] [varchar](max) NULL,
	[Booster] [varchar](max) NULL,
	[Duration] [datetime] NULL,
	[CreditLimit] [int] NULL,
	[SpendingLimit] [int] NULL,
	[BillingCycle] [varchar](max) NULL,
	[EncBy] [int] NULL,
	[EncDate] [datetime] NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
