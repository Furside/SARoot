USE [master]
GO
/****** Object:  Database [dbMobilePlan]    Script Date: 19/05/2023 10:17:22 am ******/
CREATE DATABASE [dbMobilePlan]
ALTER DATABASE [dbMobilePlan] COLLATE SQL_Latin1_General_CP1_CI_AS
GO
USE [dbMobilePlan]
GO
/****** Object:  StoredProcedure [dbo].[tbl_Contact_Proc]    Script Date: 19/05/2023 10:17:22 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[tbl_Contact_Proc]
@Type VARCHAR(50),
@Search VARCHAR(max) = null,
@ID int = null,
@EID int = null,
@MobileNo varchar(max) = null,
@NetworkTypeID int = null,
@BINCARD varchar(max) = null,
@Remarks varchar(max) = null,
@ContractStart datetime = null,
@ContractEnd datetime = null,
@Status varchar(max) = null,
@EndUser int = null,
@OverdueBalance varchar(max) = null,
@EarlyRenewal varchar(max) = null,
@ExistingPlan varchar(max) = null,
@Inclusion varchar(max) = null,
@HandsetModel varchar(max) = null,
@ExistingDuoNo varchar(max) = null,
@CreditLimit varchar(max) = null,
@SpendingLimit varchar(max) = null,
@BillingCycle varchar(max) = null,
@ModifiedBy int = null,
@ModifiedDate datetime = null,
@encBy int = null,
@encDate datetime = null
AS
BEGIN
IF @Type = 'Create'
BEGIN
INSERT INTO [tbl_Contact]
([EID],[MobileNo],[NetworkTypeID],[BINCARD],[Remarks],[ContractStart],[ContractEnd],[Status],[EndUser],[OverdueBalance],[EarlyRenewal],[ExistingPlan],[Inclusion],[HandsetModel],[ExistingDuoNo],[CreditLimit],[SpendingLimit],[BillingCycle],[ModifiedBy],[ModifiedDate],[encBy])
VALUES
(@EID,@MobileNo,@NetworkTypeID,@BINCARD,@Remarks,@ContractStart,@ContractEnd,@Status,@EndUser,@OverdueBalance,@EarlyRenewal,@ExistingPlan,@Inclusion,@HandsetModel,@ExistingDuoNo,@CreditLimit,@SpendingLimit,@BillingCycle,@ModifiedBy,@ModifiedDate,@encBy)

END
IF @Type = 'Update'
BEGIN
UPDATE [tbl_Contact] SET [EID] = @EID
,[MobileNo] = @MobileNo
,[NetworkTypeID] = @NetworkTypeID
,[BINCARD] = @BINCARD
,[Remarks] = @Remarks
,[ContractStart] = @ContractStart
,[ContractEnd] = @ContractEnd
,[Status] = @Status
,[EndUser] = @EndUser
,[OverdueBalance] = @OverdueBalance
,[EarlyRenewal] = @EarlyRenewal
,[ExistingPlan] = @ExistingPlan
,[Inclusion] = @Inclusion
,[HandsetModel] = @HandsetModel
,[ExistingDuoNo] = @ExistingDuoNo
,[CreditLimit] = @CreditLimit
,[SpendingLimit] = @SpendingLimit
,[BillingCycle] = @BillingCycle
,[ModifiedBy] = @ModifiedBy
,[ModifiedDate] = GETDATE()
 WHERE [ID] = @ID
END
IF @Type = 'Search'
BEGIN
SELECT * FROM [Contact] WHERE CONCAT([ID],[EID],[MobileNo],[NetworkTypeID],[BINCARD],[Remarks],[ContractStart],[ContractEnd],[Status],[EndUser],[OverdueBalance],[EarlyRenewal],[ExistingPlan],[Inclusion],[HandsetModel],[ExistingDuoNo],[CreditLimit],[SpendingLimit],[BillingCycle],[ModifiedBy],[ModifiedDate],[encBy],[encDate]) LIKE @Search 
END
IF @Type = 'Find'
BEGIN
SELECT * FROM [Contact] WHERE  ID = @ID
END

END

GO
/****** Object:  UserDefinedFunction [dbo].[GetNetwork]    Script Date: 19/05/2023 10:17:22 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetNetwork](@ID INT)
RETURNS VARCHAR(MAX)
AS
BEGIN
	RETURN (SELECT Network FROM tbl_NetworkType WHERE ID = @ID)
END

GO
/****** Object:  Table [dbo].[tbl_Contact]    Script Date: 19/05/2023 10:17:22 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_Contact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EID] [int] NULL,
	[MobileNo] [varchar](max) NULL,
	[NetworkTypeID] [int] NULL,
	[BINCARD] [varchar](max) NULL,
	[Remarks] [varchar](max) NULL,
	[ContractStart] [datetime] NULL,
	[ContractEnd] [datetime] NULL,
	[Status] [varchar](max) NULL,
	[EndUser] [int] NULL,
	[OverdueBalance] [varchar](max) NULL,
	[EarlyRenewal] [varchar](max) NULL,
	[ExistingPlan] [varchar](max) NULL,
	[Inclusion] [varchar](max) NULL,
	[HandsetModel] [varchar](max) NULL,
	[ExistingDuoNo] [varchar](max) NULL,
	[CreditLimit] [varchar](max) NULL,
	[SpendingLimit] [varchar](max) NULL,
	[BillingCycle] [varchar](max) NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[encBy] [int] NULL,
	[encDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbl_NetworkType]    Script Date: 19/05/2023 10:17:22 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbl_NetworkType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Network] [varchar](max) NULL,
	[encBy] [int] NULL,
	[encDate] [datetime] NULL DEFAULT (getdate()),
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[Contact]    Script Date: 19/05/2023 10:17:22 am ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[Contact]
AS
SELECT *, dbo.GetNetwork(NetworkTypeID) NetworkTypeName, dbITSD.dbo.GetEncodedBy(EID) Employee, dbITSD.dbo.GetEncodedBy(EndUser) EndUserName,
 dbITSD.dbo.GetEncodedBy(ModifiedBy) ModifiedByName,
 dbITSD.dbo.GetEncodedBy(encBy) encByName
 FROM tbl_Contact

GO
SET IDENTITY_INSERT [dbo].[tbl_NetworkType] ON 

GO
INSERT [dbo].[tbl_NetworkType] ([ID], [Network], [encBy], [encDate]) VALUES (1, N'Globe', 125, CAST(N'2023-05-19 10:14:01.403' AS DateTime))
GO
INSERT [dbo].[tbl_NetworkType] ([ID], [Network], [encBy], [encDate]) VALUES (2, N'Smart', 125, CAST(N'2023-05-19 10:16:18.603' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[tbl_NetworkType] OFF
GO
ALTER TABLE [dbo].[tbl_Contact] ADD  DEFAULT (getdate()) FOR [encDate]
GO
USE [master]
GO
ALTER DATABASE [dbMobilePlan] SET  READ_WRITE 
GO
