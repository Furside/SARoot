USE [master]
GO
/****** Object:  Database [dbMobilePlan]    Script Date: 19/05/2023 04:49:27 PM ******/
CREATE DATABASE [dbMobilePlan]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'dbMobilePlan', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\dbMobilePlan.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'dbMobilePlan_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\dbMobilePlan_log.ldf' , SIZE = 1344KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [dbMobilePlan] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [dbMobilePlan].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [dbMobilePlan] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [dbMobilePlan] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [dbMobilePlan] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [dbMobilePlan] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [dbMobilePlan] SET ARITHABORT OFF 
GO
ALTER DATABASE [dbMobilePlan] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [dbMobilePlan] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [dbMobilePlan] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [dbMobilePlan] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [dbMobilePlan] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [dbMobilePlan] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [dbMobilePlan] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [dbMobilePlan] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [dbMobilePlan] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [dbMobilePlan] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [dbMobilePlan] SET  ENABLE_BROKER 
GO
ALTER DATABASE [dbMobilePlan] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [dbMobilePlan] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [dbMobilePlan] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [dbMobilePlan] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [dbMobilePlan] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [dbMobilePlan] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [dbMobilePlan] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [dbMobilePlan] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [dbMobilePlan] SET  MULTI_USER 
GO
ALTER DATABASE [dbMobilePlan] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [dbMobilePlan] SET DB_CHAINING OFF 
GO
ALTER DATABASE [dbMobilePlan] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [dbMobilePlan] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [dbMobilePlan]
GO
/****** Object:  StoredProcedure [dbo].[tbl_Contact_Proc]    Script Date: 19/05/2023 04:49:27 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[GetNetwork]    Script Date: 19/05/2023 04:49:27 PM ******/
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
/****** Object:  UserDefinedFunction [dbo].[GetNetworkByNumber]    Script Date: 19/05/2023 04:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[GetNetworkByNumber](@MobileNumber varchar(max))
returns varchar(max)
as
begin
 return (
	select Network
	from NetworkPrefix
	where Prefix = CAST(SUBSTRING(@MobileNumber, 1,3) as int)
	)
	end
GO
/****** Object:  UserDefinedFunction [dbo].[GetNetworkByPrefix]    Script Date: 19/05/2023 04:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE function [dbo].[GetNetworkByPrefix](@Prefix int)
returns varchar(50)
as
begin
	return ( select Network from dbo.NetworkPrefix where Prefix = @Prefix )
end
GO
/****** Object:  Table [dbo].[tbl_Contact]    Script Date: 19/05/2023 04:49:27 PM ******/
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
	[ModifiedDate] [datetime] NULL CONSTRAINT [DF_tbl_Contact_ModifiedDate]  DEFAULT (getdate()),
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
/****** Object:  Table [dbo].[tbl_NetworkType]    Script Date: 19/05/2023 04:49:27 PM ******/
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
/****** Object:  Table [dbo].[tbl_SimNetworkPrefixes]    Script Date: 19/05/2023 04:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SimNetworkPrefixes](
	[Prefix] [int] NULL,
	[NetworkProviderID] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[Contact]    Script Date: 19/05/2023 04:49:27 PM ******/
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
/****** Object:  View [dbo].[NetworkPrefix]    Script Date: 19/05/2023 04:49:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[NetworkPrefix]
as

	select tbl_SimNetworkPrefixes.Prefix, dbo.GetNetwork(NetworkProviderID) Network from tbl_SimNetworkPrefixes
GO
USE [master]
GO
ALTER DATABASE [dbMobilePlan] SET  READ_WRITE 
GO
