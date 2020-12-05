USE [master]
GO
/****** Object:  Database [QIQOMonitor]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE DATABASE [QIQOMonitor]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QIQOMonitor', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.D1\MSSQL\DATA\QIQOMonitor.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QIQOMonitor_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.D1\MSSQL\DATA\QIQOMonitor_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [QIQOMonitor] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QIQOMonitor].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QIQOMonitor] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QIQOMonitor] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QIQOMonitor] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QIQOMonitor] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QIQOMonitor] SET ARITHABORT OFF 
GO
ALTER DATABASE [QIQOMonitor] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QIQOMonitor] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QIQOMonitor] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QIQOMonitor] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QIQOMonitor] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QIQOMonitor] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QIQOMonitor] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QIQOMonitor] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QIQOMonitor] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QIQOMonitor] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QIQOMonitor] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QIQOMonitor] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QIQOMonitor] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QIQOMonitor] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QIQOMonitor] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QIQOMonitor] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QIQOMonitor] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QIQOMonitor] SET RECOVERY FULL 
GO
ALTER DATABASE [QIQOMonitor] SET  MULTI_USER 
GO
ALTER DATABASE [QIQOMonitor] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QIQOMonitor] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QIQOMonitor] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QIQOMonitor] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QIQOMonitor] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'QIQOMonitor', N'ON'
GO
ALTER DATABASE [QIQOMonitor] SET QUERY_STORE = OFF
GO
USE [QIQOMonitor]
GO
/****** Object:  User [QIQOMonitorUser]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE USER [QIQOMonitorUser] FOR LOGIN [QIQOMonitorUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [QIQOMonitorUser]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [QIQOMonitorUser]
GO
GRANT VIEW ANY COLUMN ENCRYPTION KEY DEFINITION TO [public] AS [dbo]
GO
GRANT VIEW ANY COLUMN MASTER KEY DEFINITION TO [public] AS [dbo]
GO
GRANT CONNECT TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  Table [dbo].[AttributeDataType]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttributeDataType](
	[AttributeDataTypeKey] [int] IDENTITY(1,1) NOT NULL,
	[AttributeDataTypeName] [nvarchar](50) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_AttributeDataType] PRIMARY KEY NONCLUSTERED 
(
	[AttributeDataTypeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [uixAttributeDataType_Name]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE CLUSTERED INDEX [uixAttributeDataType_Name] ON [dbo].[AttributeDataType]
(
	[AttributeDataTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AttributeType]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttributeType](
	[AttributeTypeKey] [int] IDENTITY(1,1) NOT NULL,
	[AttributeDataTypeKey] [int] NOT NULL,
	[AttributeTypeName] [nvarchar](50) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_AttributeType] PRIMARY KEY CLUSTERED 
(
	[AttributeTypeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EnvironmentServer]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EnvironmentServer](
	[EnvironmentKey] [int] NOT NULL,
	[ServerKey] [int] NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_EvironmentServer] PRIMARY KEY CLUSTERED 
(
	[EnvironmentKey] ASC,
	[ServerKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EnvironmentService]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EnvironmentService](
	[EnvironmentKey] [int] NOT NULL,
	[ServiceKey] [int] NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_EvironmentService] PRIMARY KEY CLUSTERED 
(
	[EnvironmentKey] ASC,
	[ServiceKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Monitor]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monitor](
	[MonitorKey] [int] IDENTITY(1,1) NOT NULL,
	[MonitorTypeKey] [int] NOT NULL,
	[MonitorName] [nvarchar](50) NOT NULL,
	[LevelKey] [int] NOT NULL,
	[CategoryKey] [int] NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_Monitor] PRIMARY KEY CLUSTERED 
(
	[MonitorKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonitorCategory]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitorCategory](
	[CategoryKey] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[CategoryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonitoredServer]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitoredServer](
	[ServerKey] [int] IDENTITY(1,1) NOT NULL,
	[ServerName] [nvarchar](50) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_Server] PRIMARY KEY CLUSTERED 
(
	[ServerKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonitoredService]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitoredService](
	[ServiceKey] [int] IDENTITY(1,1) NOT NULL,
	[ServiceTypeKey] [int] NOT NULL,
	[ServerKey] [int] NOT NULL,
	[ServiceName] [nvarchar](50) NOT NULL,
	[InstanceName] [nvarchar](50) NOT NULL,
	[ServiceSource] [nvarchar](50) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_SqlServer] PRIMARY KEY CLUSTERED 
(
	[ServiceKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonitorEnvironment]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitorEnvironment](
	[EnvironmentKey] [int] IDENTITY(1,1) NOT NULL,
	[EnvironmentName] [nvarchar](50) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_Evironment] PRIMARY KEY CLUSTERED 
(
	[EnvironmentKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonitorLevel]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitorLevel](
	[LevelKey] [int] IDENTITY(1,1) NOT NULL,
	[LevelName] [nvarchar](50) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_Level] PRIMARY KEY CLUSTERED 
(
	[LevelKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonitorQuery]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitorQuery](
	[MonitorKey] [int] NOT NULL,
	[QueryKey] [int] NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_SqlServiceQuery] PRIMARY KEY CLUSTERED 
(
	[MonitorKey] ASC,
	[QueryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonitorType]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitorType](
	[MonitorTypeKey] [int] IDENTITY(1,1) NOT NULL,
	[MonitorTypeName] [nvarchar](50) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_MonitorType] PRIMARY KEY CLUSTERED 
(
	[MonitorTypeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Query]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Query](
	[QueryKey] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[QueryText] [nvarchar](max) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_Query] PRIMARY KEY CLUSTERED 
(
	[QueryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QueryHistory]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueryHistory](
	[QueryHistoryKey] [int] IDENTITY(1,1) NOT NULL,
	[ServiceKey] [int] NOT NULL,
	[QueryKey] [int] NOT NULL,
	[TesultText] [nvarchar](max) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_QueryHistory] PRIMARY KEY CLUSTERED 
(
	[QueryHistoryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceMonitor]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceMonitor](
	[ServiceKey] [int] NOT NULL,
	[MonitorKey] [int] NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_ServiceMonitor] PRIMARY KEY CLUSTERED 
(
	[ServiceKey] ASC,
	[MonitorKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceMonitorAttribute]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceMonitorAttribute](
	[ServiceKey] [int] NOT NULL,
	[MonitorKey] [int] NOT NULL,
	[AttributeTypeKey] [int] NOT NULL,
	[AttributeValue] [nvarchar](250) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_ServiceMonitorAttribute_1] PRIMARY KEY CLUSTERED 
(
	[ServiceKey] ASC,
	[MonitorKey] ASC,
	[AttributeTypeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceType]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceType](
	[ServiceTypeKey] [int] IDENTITY(1,1) NOT NULL,
	[ServiceTypeName] [nvarchar](50) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
	[RowAddedBy] [nvarchar](30) NOT NULL,
	[RowUpdateDate] [datetime] NULL,
	[RowUpdateBy] [nvarchar](30) NULL,
 CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED 
(
	[ServiceTypeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WaitStatsLog]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaitStatsLog](
	[LogKey] [bigint] IDENTITY(1,1) NOT NULL,
	[BatchNo] [bigint] NOT NULL,
	[ServiceKey] [int] NOT NULL,
	[WaitTypeKey] [int] NOT NULL,
	[WaitPercentage] [decimal](5, 2) NOT NULL,
	[AvgWaitSec] [decimal](16, 4) NOT NULL,
	[AvgResSec] [decimal](16, 4) NOT NULL,
	[AvgSigSec] [decimal](16, 4) NOT NULL,
	[WaitSec] [decimal](16, 2) NOT NULL,
	[ResourceSec] [decimal](16, 2) NOT NULL,
	[SignalSec] [decimal](16, 2) NOT NULL,
	[WaitCount] [bigint] NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_WaitStatsLog] PRIMARY KEY NONCLUSTERED 
(
	[LogKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [idx_WaitStatsLog_BatchNo_WaitTypeKey]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE CLUSTERED INDEX [idx_WaitStatsLog_BatchNo_WaitTypeKey] ON [dbo].[WaitStatsLog]
(
	[ServiceKey] ASC,
	[BatchNo] ASC,
	[WaitTypeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WaitType]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaitType](
	[WaitTypeKey] [int] IDENTITY(1,1) NOT NULL,
	[WaitType] [nvarchar](120) NOT NULL,
	[RowAddedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[WaitTypeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [uixAttributeType_Name]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [uixAttributeType_Name] ON [dbo].[AttributeType]
(
	[AttributeTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idxMonitor_All]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idxMonitor_All] ON [dbo].[Monitor]
(
	[MonitorTypeKey] ASC,
	[MonitorName] ASC,
	[LevelKey] ASC,
	[CategoryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [uixCategory_Name]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [uixCategory_Name] ON [dbo].[MonitorCategory]
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [uixServer_Name]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [uixServer_Name] ON [dbo].[MonitoredServer]
(
	[ServerName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [uix_Service_ServiceName]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [uix_Service_ServiceName] ON [dbo].[MonitoredService]
(
	[ServiceName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [udx_Environment_EnvironmentName]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [udx_Environment_EnvironmentName] ON [dbo].[MonitorEnvironment]
(
	[EnvironmentName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [uixLevel_Name]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [uixLevel_Name] ON [dbo].[MonitorLevel]
(
	[LevelName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [uixMonitorType_Name]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [uixMonitorType_Name] ON [dbo].[MonitorType]
(
	[MonitorTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [uixQuery_Name]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [uixQuery_Name] ON [dbo].[Query]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [uixServiceType_Name]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [uixServiceType_Name] ON [dbo].[ServiceType]
(
	[ServiceTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [idxWaitType_WaitType]    Script Date: 12/5/2020 2:43:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [idxWaitType_WaitType] ON [dbo].[WaitType]
(
	[WaitType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AttributeDataType] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[AttributeDataType] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[AttributeDataType] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[AttributeDataType] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[AttributeType] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[AttributeType] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[AttributeType] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[AttributeType] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[EnvironmentServer] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[EnvironmentServer] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[EnvironmentServer] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[EnvironmentServer] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[EnvironmentService] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[EnvironmentService] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[EnvironmentService] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[EnvironmentService] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[Monitor] ADD  CONSTRAINT [DF_Monitor_level_key]  DEFAULT ((1)) FOR [LevelKey]
GO
ALTER TABLE [dbo].[Monitor] ADD  CONSTRAINT [DF_Monitor_category_key]  DEFAULT ((1)) FOR [CategoryKey]
GO
ALTER TABLE [dbo].[Monitor] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[Monitor] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[Monitor] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[Monitor] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[MonitorCategory] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[MonitorCategory] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[MonitorCategory] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[MonitorCategory] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[MonitoredServer] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[MonitoredServer] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[MonitoredServer] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[MonitoredServer] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[MonitoredService] ADD  CONSTRAINT [DF_Service_service_type_key]  DEFAULT ((1)) FOR [ServiceTypeKey]
GO
ALTER TABLE [dbo].[MonitoredService] ADD  CONSTRAINT [DF_Service_service_source]  DEFAULT ('') FOR [ServiceSource]
GO
ALTER TABLE [dbo].[MonitoredService] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[MonitoredService] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[MonitoredService] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[MonitoredService] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[MonitorEnvironment] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[MonitorEnvironment] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[MonitorEnvironment] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[MonitorEnvironment] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[MonitorLevel] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[MonitorLevel] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[MonitorLevel] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[MonitorLevel] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[MonitorQuery] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[MonitorQuery] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[MonitorQuery] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[MonitorQuery] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[MonitorType] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[MonitorType] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[MonitorType] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[MonitorType] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[Query] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[Query] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[Query] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[Query] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[QueryHistory] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[QueryHistory] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[QueryHistory] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[QueryHistory] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[ServiceMonitor] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[ServiceMonitor] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[ServiceMonitor] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[ServiceMonitor] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[ServiceMonitorAttribute] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[ServiceMonitorAttribute] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[ServiceMonitorAttribute] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[ServiceMonitorAttribute] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[ServiceType] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[ServiceType] ADD  DEFAULT (suser_sname()) FOR [RowAddedBy]
GO
ALTER TABLE [dbo].[ServiceType] ADD  DEFAULT (getdate()) FOR [RowUpdateDate]
GO
ALTER TABLE [dbo].[ServiceType] ADD  DEFAULT (suser_sname()) FOR [RowUpdateBy]
GO
ALTER TABLE [dbo].[WaitStatsLog] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[WaitType] ADD  DEFAULT (getdate()) FOR [RowAddedDate]
GO
ALTER TABLE [dbo].[AttributeType]  WITH CHECK ADD  CONSTRAINT [FK_AttributeType_AttributeDataType] FOREIGN KEY([AttributeDataTypeKey])
REFERENCES [dbo].[AttributeDataType] ([AttributeDataTypeKey])
GO
ALTER TABLE [dbo].[AttributeType] CHECK CONSTRAINT [FK_AttributeType_AttributeDataType]
GO
ALTER TABLE [dbo].[EnvironmentServer]  WITH CHECK ADD  CONSTRAINT [FK_EnvironmentServer_Environment] FOREIGN KEY([EnvironmentKey])
REFERENCES [dbo].[MonitorEnvironment] ([EnvironmentKey])
GO
ALTER TABLE [dbo].[EnvironmentServer] CHECK CONSTRAINT [FK_EnvironmentServer_Environment]
GO
ALTER TABLE [dbo].[EnvironmentServer]  WITH CHECK ADD  CONSTRAINT [FK_EnvironmentServer_Server] FOREIGN KEY([ServerKey])
REFERENCES [dbo].[MonitoredServer] ([ServerKey])
GO
ALTER TABLE [dbo].[EnvironmentServer] CHECK CONSTRAINT [FK_EnvironmentServer_Server]
GO
ALTER TABLE [dbo].[EnvironmentService]  WITH CHECK ADD  CONSTRAINT [FK_EnvironmentService_Environment] FOREIGN KEY([EnvironmentKey])
REFERENCES [dbo].[MonitorEnvironment] ([EnvironmentKey])
GO
ALTER TABLE [dbo].[EnvironmentService] CHECK CONSTRAINT [FK_EnvironmentService_Environment]
GO
ALTER TABLE [dbo].[EnvironmentService]  WITH CHECK ADD  CONSTRAINT [FK_EnvironmentService_Service] FOREIGN KEY([ServiceKey])
REFERENCES [dbo].[MonitoredService] ([ServiceKey])
GO
ALTER TABLE [dbo].[EnvironmentService] CHECK CONSTRAINT [FK_EnvironmentService_Service]
GO
ALTER TABLE [dbo].[Monitor]  WITH CHECK ADD  CONSTRAINT [FK_Monitor_Category] FOREIGN KEY([CategoryKey])
REFERENCES [dbo].[MonitorCategory] ([CategoryKey])
GO
ALTER TABLE [dbo].[Monitor] CHECK CONSTRAINT [FK_Monitor_Category]
GO
ALTER TABLE [dbo].[Monitor]  WITH CHECK ADD  CONSTRAINT [FK_Monitor_Level] FOREIGN KEY([LevelKey])
REFERENCES [dbo].[MonitorLevel] ([LevelKey])
GO
ALTER TABLE [dbo].[Monitor] CHECK CONSTRAINT [FK_Monitor_Level]
GO
ALTER TABLE [dbo].[Monitor]  WITH CHECK ADD  CONSTRAINT [FK_Monitor_MonitorType] FOREIGN KEY([MonitorTypeKey])
REFERENCES [dbo].[MonitorType] ([MonitorTypeKey])
GO
ALTER TABLE [dbo].[Monitor] CHECK CONSTRAINT [FK_Monitor_MonitorType]
GO
ALTER TABLE [dbo].[MonitoredService]  WITH CHECK ADD  CONSTRAINT [FK_Service_ServiceType] FOREIGN KEY([ServiceTypeKey])
REFERENCES [dbo].[ServiceType] ([ServiceTypeKey])
GO
ALTER TABLE [dbo].[MonitoredService] CHECK CONSTRAINT [FK_Service_ServiceType]
GO
ALTER TABLE [dbo].[MonitoredService]  WITH CHECK ADD  CONSTRAINT [FK_SqlServer_Server] FOREIGN KEY([ServerKey])
REFERENCES [dbo].[MonitoredServer] ([ServerKey])
GO
ALTER TABLE [dbo].[MonitoredService] CHECK CONSTRAINT [FK_SqlServer_Server]
GO
ALTER TABLE [dbo].[MonitorQuery]  WITH CHECK ADD  CONSTRAINT [FK_SqlServiceQuery_Monitor] FOREIGN KEY([MonitorKey])
REFERENCES [dbo].[Monitor] ([MonitorKey])
GO
ALTER TABLE [dbo].[MonitorQuery] CHECK CONSTRAINT [FK_SqlServiceQuery_Monitor]
GO
ALTER TABLE [dbo].[MonitorQuery]  WITH CHECK ADD  CONSTRAINT [FK_SqlServiceQuery_Query] FOREIGN KEY([QueryKey])
REFERENCES [dbo].[Query] ([QueryKey])
GO
ALTER TABLE [dbo].[MonitorQuery] CHECK CONSTRAINT [FK_SqlServiceQuery_Query]
GO
ALTER TABLE [dbo].[QueryHistory]  WITH CHECK ADD  CONSTRAINT [FK_QueryHistory_Query] FOREIGN KEY([QueryKey])
REFERENCES [dbo].[Query] ([QueryKey])
GO
ALTER TABLE [dbo].[QueryHistory] CHECK CONSTRAINT [FK_QueryHistory_Query]
GO
ALTER TABLE [dbo].[QueryHistory]  WITH CHECK ADD  CONSTRAINT [FK_QueryHistory_SqlServer] FOREIGN KEY([ServiceKey])
REFERENCES [dbo].[MonitoredService] ([ServiceKey])
GO
ALTER TABLE [dbo].[QueryHistory] CHECK CONSTRAINT [FK_QueryHistory_SqlServer]
GO
ALTER TABLE [dbo].[ServiceMonitor]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMonitor_Monitor] FOREIGN KEY([MonitorKey])
REFERENCES [dbo].[Monitor] ([MonitorKey])
GO
ALTER TABLE [dbo].[ServiceMonitor] CHECK CONSTRAINT [FK_ServiceMonitor_Monitor]
GO
ALTER TABLE [dbo].[ServiceMonitor]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMonitor_Service] FOREIGN KEY([ServiceKey])
REFERENCES [dbo].[MonitoredService] ([ServiceKey])
GO
ALTER TABLE [dbo].[ServiceMonitor] CHECK CONSTRAINT [FK_ServiceMonitor_Service]
GO
ALTER TABLE [dbo].[ServiceMonitorAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMonitorAttribute_AttributeType] FOREIGN KEY([AttributeTypeKey])
REFERENCES [dbo].[AttributeType] ([AttributeTypeKey])
GO
ALTER TABLE [dbo].[ServiceMonitorAttribute] CHECK CONSTRAINT [FK_ServiceMonitorAttribute_AttributeType]
GO
ALTER TABLE [dbo].[ServiceMonitorAttribute]  WITH CHECK ADD  CONSTRAINT [FK_ServiceMonitorAttribute_ServiceMonitor] FOREIGN KEY([ServiceKey], [MonitorKey])
REFERENCES [dbo].[ServiceMonitor] ([ServiceKey], [MonitorKey])
GO
ALTER TABLE [dbo].[ServiceMonitorAttribute] CHECK CONSTRAINT [FK_ServiceMonitorAttribute_ServiceMonitor]
GO
ALTER TABLE [dbo].[WaitStatsLog]  WITH CHECK ADD  CONSTRAINT [FK_WaitStatsLog_WaitType] FOREIGN KEY([WaitTypeKey])
REFERENCES [dbo].[WaitType] ([WaitTypeKey])
GO
ALTER TABLE [dbo].[WaitStatsLog] CHECK CONSTRAINT [FK_WaitStatsLog_WaitType]
GO
/****** Object:  StoredProcedure [dbo].[monAttributeDataTypeDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[monAttributeDataTypeDelete]
	@AttributeDataTypeKey int

AS
BEGIN
	DELETE FROM [AttributeDataType]
	WHERE AttributeDataTypeKey = @AttributeDataTypeKey
END

GO
GRANT EXECUTE ON [dbo].[monAttributeDataTypeDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monAttributeDataTypeGet]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[monAttributeDataTypeGet]
	@AttributeDataTypeKey int

AS

SELECT * FROM [dbo].[AttributeDataType]
WHERE AttributeDataTypeKey = @AttributeDataTypeKey
GO
GRANT EXECUTE ON [dbo].[monAttributeDataTypeGet] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monAttributeDataTypeGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[monAttributeDataTypeGetAll]

AS

SELECT * FROM [dbo].[AttributeDataType]
GO
GRANT EXECUTE ON [dbo].[monAttributeDataTypeGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monAttributeDataTypeUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[monAttributeDataTypeUpsert]
	@AttributeDataTypeKey int = 0,
	@AttributeDataTypeName nvarchar(100)

AS
BEGIN
	IF (@AttributeDataTypeKey = 0)
		INSERT INTO [AttributeDataType] (AttributeDataTypeName)
		VALUES (@AttributeDataTypeName)
	ELSE
		UPDATE [AttributeDataType] SET AttributeDataTypeName = @AttributeDataTypeName
		WHERE AttributeDataTypeKey = @AttributeDataTypeKey
END

GO
GRANT EXECUTE ON [dbo].[monAttributeDataTypeUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monAttributeTypeDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[monAttributeTypeDelete]
	@AttributeTypeKey int

AS
BEGIN
	DELETE FROM [AttributeType]
	WHERE AttributeTypeKey = @AttributeTypeKey
END

GO
GRANT EXECUTE ON [dbo].[monAttributeTypeDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monAttributeTypeGet]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[monAttributeTypeGet]
	@AttributeTypeKey int

AS

SELECT * FROM [dbo].[AttributeType]
WHERE AttributeTypeKey = @AttributeTypeKey
GO
GRANT EXECUTE ON [dbo].[monAttributeTypeGet] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monAttributeTypeGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[monAttributeTypeGetAll]

AS

SELECT * FROM [dbo].[AttributeType]
GO
GRANT EXECUTE ON [dbo].[monAttributeTypeGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monAttributeTypeUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[monAttributeTypeUpsert]
	@AttributeTypeKey int = 0,
	@AttributeTypeName nvarchar(100)

AS
BEGIN
	IF (@AttributeTypeKey = 0)
		INSERT INTO [AttributeType] (AttributeTypeName)
		VALUES (@AttributeTypeName)
	ELSE
		UPDATE [AttributeType] SET AttributeTypeName = @AttributeTypeName
		WHERE AttributeTypeKey = @AttributeTypeKey
END

GO
GRANT EXECUTE ON [dbo].[monAttributeTypeUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorCategoryDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorCategoryDelete]
	@CategoryKey int

AS
BEGIN
	DELETE FROM [MonitorCategory]
	WHERE CategoryKey = @CategoryKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorCategoryDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorCategoryGet]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[monMonitorCategoryGet]
	@CategoryKey int

AS

SELECT * FROM [dbo].[MonitorCategory]
WHERE CategoryKey = @CategoryKey
GO
GRANT EXECUTE ON [dbo].[monMonitorCategoryGet] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorCategoryGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[monMonitorCategoryGetAll]

AS

SELECT * FROM [dbo].[MonitorCategory]
GO
GRANT EXECUTE ON [dbo].[monMonitorCategoryGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorCategoryUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorCategoryUpsert]
	@CategoryKey int = 0,
	@CategoryName nvarchar(100)

AS
BEGIN
	IF (@CategoryKey = 0)
		INSERT INTO [MonitorCategory] (CategoryName)
		VALUES (@CategoryName)
	ELSE
		UPDATE [MonitorCategory] SET CategoryName = @CategoryName
		WHERE CategoryKey = @CategoryKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorCategoryUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorDelete]
	@MonitorKey int

AS
BEGIN
	DELETE FROM [Monitor]
	WHERE MonitorKey = @MonitorKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServerDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitoredServerDelete]
	@ServerKey int

AS
BEGIN
	DELETE FROM [MonitoredServer]
	WHERE ServerKey = @ServerKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitoredServerDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServerGet]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[monMonitoredServerGet]
	@ServerKey int
AS

SELECT * FROM [dbo].[MonitoredServer] WHERE ServerKey = @ServerKey

GO
GRANT EXECUTE ON [dbo].[monMonitoredServerGet] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServerGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[monMonitoredServerGetAll]

AS

SELECT * FROM [dbo].[MonitoredServer]
GO
GRANT EXECUTE ON [dbo].[monMonitoredServerGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServerUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitoredServerUpsert]
	@ServerKey int = 0,
	@ServerName nvarchar(100)

AS
BEGIN
	IF (@ServerKey = 0)
		INSERT INTO [MonitoredServer] (ServerName)
		VALUES (@ServerName)
	ELSE
		UPDATE [MonitoredServer] SET ServerName = @ServerName
		WHERE ServerKey = @ServerKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitoredServerUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServiceDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitoredServiceDelete]
	@ServiceKey int

AS
BEGIN
	DELETE FROM [MonitoredService]
	WHERE ServiceKey = @ServiceKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitoredServiceDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServiceGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[monMonitoredServiceGetAll]

AS

SELECT * FROM [dbo].[MonitoredService]
GO
GRANT EXECUTE ON [dbo].[monMonitoredServiceGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServiceMonitorAttributeDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[monMonitoredServiceMonitorAttributeDelete]
	@ServiceKey int,
	@MonitorKey int,
	@AttributeTypeKey int

AS
BEGIN
	DELETE FROM [ServiceMonitorAttribute]
	WHERE ServiceKey = @ServiceKey
		AND MonitorKey = @MonitorKey
		AND AttributeTypeKey = @AttributeTypeKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitoredServiceMonitorAttributeDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServiceMonitorAttributeGet]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROC [dbo].[monMonitoredServiceMonitorAttributeGet]
	@ServiceKey int,
	@MonitorKey int,
	@AttributeTypeKey int

AS

SELECT *
FROM [dbo].[ServiceMonitorAttribute]
WHERE ServiceKey = @ServiceKey
AND MonitorKey = @MonitorKey
AND AttributeTypeKey = @AttributeTypeKey
GO
GRANT EXECUTE ON [dbo].[monMonitoredServiceMonitorAttributeGet] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServiceMonitorAttributeGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[monMonitoredServiceMonitorAttributeGetAll]

AS

SELECT * FROM [dbo].[ServiceMonitorAttribute]
GO
GRANT EXECUTE ON [dbo].[monMonitoredServiceMonitorAttributeGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServiceMonitorAttributeUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[monMonitoredServiceMonitorAttributeUpsert]
	@ServiceKey int,
	@MonitorKey int,
	@AttributeTypeKey int,
	@AttributeValue nvarchar(500) = ''

AS
BEGIN
	IF NOT EXISTS (SELECT * FROM [dbo].[ServiceMonitorAttribute]
		WHERE ServiceKey = @ServiceKey
		AND MonitorKey = @MonitorKey
		AND AttributeTypeKey = @AttributeTypeKey)
	BEGIN
		INSERT INTO [ServiceMonitorAttribute] (ServiceKey, MonitorKey, AttributeTypeKey, AttributeValue)
		VALUES (@ServiceKey, @MonitorKey, @AttributeTypeKey, @AttributeValue)
	END
	ELSE
	BEGIN
		UPDATE [dbo].[ServiceMonitorAttribute] SET
		AttributeValue = @AttributeValue
		WHERE ServiceKey = @ServiceKey
		AND MonitorKey = @MonitorKey
		AND AttributeTypeKey = @AttributeTypeKey
	END
END

GO
GRANT EXECUTE ON [dbo].[monMonitoredServiceMonitorAttributeUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServiceMonitorDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[monMonitoredServiceMonitorDelete]
	@ServiceKey int,
	@MonitorKey int

AS
BEGIN
	DELETE FROM [ServiceMonitor]
	WHERE ServiceKey = @ServiceKey
		AND MonitorKey = @MonitorKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitoredServiceMonitorDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServiceMonitorGet]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[monMonitoredServiceMonitorGet]
	@ServiceKey int,
	@MonitorKey int

AS

SELECT * FROM [dbo].[ServiceMonitor]
WHERE ServiceKey = @ServiceKey
AND MonitorKey = @MonitorKey
GO
GRANT EXECUTE ON [dbo].[monMonitoredServiceMonitorGet] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServiceMonitorGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[monMonitoredServiceMonitorGetAll]

AS

SELECT * FROM [dbo].[ServiceMonitor]
GO
GRANT EXECUTE ON [dbo].[monMonitoredServiceMonitorGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServiceMonitorUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitoredServiceMonitorUpsert]
	@ServiceKey int,
	@MonitorKey int

AS
BEGIN
	INSERT INTO [ServiceMonitor] (ServiceKey, MonitorKey)
	VALUES (@ServiceKey, @MonitorKey)
END

GO
GRANT EXECUTE ON [dbo].[monMonitoredServiceMonitorUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitoredServiceUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitoredServiceUpsert]
	@ServiceKey int = 0,
	@ServiceName nvarchar(100),
	@ServiceSource nvarchar(100),
	@ServiceTypeKey int,
	@ServerKey int,
	@InstanceName nvarchar(100)

AS
BEGIN
	IF (@ServiceKey = 0)
	BEGIN
		INSERT INTO [MonitoredService] ([ServiceName], ServiceSource, ServiceTypeKey, ServerKey, InstanceName)
		VALUES (@ServiceName, @ServiceSource, @ServiceTypeKey, @ServerKey, @InstanceName)
	END
	ELSE
	BEGIN
		UPDATE [MonitoredService] SET
			-- ServiceTypeKey = @ServiceTypeKey,
			ServerKey = @ServerKey,
			ServiceName = @ServiceName,
			InstanceName = @InstanceName,
			ServiceSource = @ServiceSource
		WHERE ServiceKey = @ServiceKey
	END
END

GO
GRANT EXECUTE ON [dbo].[monMonitoredServiceUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorEnvironmentDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorEnvironmentDelete]
	@EnvironmentKey int

AS
BEGIN
	DELETE FROM MonitorEnvironment
	WHERE EnvironmentKey = @EnvironmentKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorEnvironmentDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorEnvironmentGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[monMonitorEnvironmentGetAll]

AS

SELECT * FROM [dbo].[MonitorEnvironment]
GO
GRANT EXECUTE ON [dbo].[monMonitorEnvironmentGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorEnvironmentServerDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorEnvironmentServerDelete]
	@EnvironmentKey int,
	@ServerKey int

AS
BEGIN
	DELETE FROM EnvironmentServer
	WHERE EnvironmentKey = @EnvironmentKey
		AND ServerKey = @ServerKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorEnvironmentServerDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorEnvironmentServerGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROC [dbo].[monMonitorEnvironmentServerGetAll]

AS

SELECT * FROM [dbo].[EnvironmentServer]
GO
GRANT EXECUTE ON [dbo].[monMonitorEnvironmentServerGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorEnvironmentServerUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorEnvironmentServerUpsert]
	@EnvironmentKey int,
	@ServerKey int

AS
BEGIN
	INSERT INTO EnvironmentServer (EnvironmentKey, ServerKey)
	VALUES (@EnvironmentKey, @ServerKey)
END

GO
GRANT EXECUTE ON [dbo].[monMonitorEnvironmentServerUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorEnvironmentServiceDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorEnvironmentServiceDelete]
	@EnvironmentKey int,
	@ServiceKey int

AS
BEGIN
	DELETE FROM EnvironmentService
	WHERE EnvironmentKey = @EnvironmentKey
		AND ServiceKey = @ServiceKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorEnvironmentServiceDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorEnvironmentServiceGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROC [dbo].[monMonitorEnvironmentServiceGetAll]

AS

SELECT * FROM [dbo].[EnvironmentService]
GO
GRANT EXECUTE ON [dbo].[monMonitorEnvironmentServiceGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorEnvironmentServiceUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorEnvironmentServiceUpsert]
	@EnvironmentKey int,
	@ServiceKey int

AS
BEGIN
	INSERT INTO EnvironmentService (EnvironmentKey, ServiceKey)
	VALUES (@EnvironmentKey, @ServiceKey)
END

GO
GRANT EXECUTE ON [dbo].[monMonitorEnvironmentServiceUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorEnvironmentUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorEnvironmentUpsert]
	@EnvironmentKey int = 0,
	@EnvironmentName nvarchar(100)

AS
BEGIN
	IF (@EnvironmentKey = 0)
		INSERT INTO MonitorEnvironment (EnvironmentName)
		VALUES (@EnvironmentName)
	ELSE
		UPDATE MonitorEnvironment SET
		EnvironmentName = @EnvironmentName
		WHERE EnvironmentKey = @EnvironmentKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorEnvironmentUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[monMonitorGetAll]

AS

SELECT * FROM [dbo].[Monitor]
GO
GRANT EXECUTE ON [dbo].[monMonitorGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorLevelDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorLevelDelete]
	@LevelKey int

AS
BEGIN
	DELETE FROM [MonitorLevel]
	WHERE LevelKey = @LevelKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorLevelDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorLevelGet]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[monMonitorLevelGet]
	@LevelKey int

AS

SELECT * FROM [dbo].[MonitorLevel]
WHERE LevelKey = @LevelKey
GO
GRANT EXECUTE ON [dbo].[monMonitorLevelGet] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorLevelGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[monMonitorLevelGetAll]

AS

SELECT * FROM [dbo].[MonitorLevel]
GO
GRANT EXECUTE ON [dbo].[monMonitorLevelGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorLevelUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorLevelUpsert]
	@LevelKey int = 0,
	@LevelName nvarchar(100)

AS
BEGIN
	IF (@LevelKey = 0)
		INSERT INTO [MonitorLevel] (LevelName)
		VALUES (@LevelName)
	ELSE
		UPDATE [MonitorLevel] SET LevelName = @LevelName
		WHERE LevelKey = @LevelKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorLevelUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorQueryDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorQueryDelete]
	@MonitorKey int,
	@QueryKey int

AS
BEGIN
	DELETE FROM [MonitorQuery]
	WHERE MonitorKey = @MonitorKey
	AND QueryKey = @QueryKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorQueryDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorQueryGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[monMonitorQueryGetAll]

AS

SELECT * FROM [dbo].[MonitorQuery]
GO
GRANT EXECUTE ON [dbo].[monMonitorQueryGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorQueryUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorQueryUpsert]
	@MonitorKey int,
	@QueryKey int

AS
BEGIN
	INSERT INTO [MonitorQuery] (MonitorKey, QueryKey)
	VALUES (@MonitorKey, @QueryKey)
END

GO
GRANT EXECUTE ON [dbo].[monMonitorQueryUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorTypeDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorTypeDelete]
	@MonitorTypeKey int

AS
BEGIN
	DELETE FROM [MonitorType]
	WHERE MonitorTypeKey = @MonitorTypeKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorTypeDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorTypeGet]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[monMonitorTypeGet]
	@MonitorTypeKey int

AS

SELECT * FROM [dbo].[MonitorType]
WHERE MonitorTypeKey = @MonitorTypeKey
GO
GRANT EXECUTE ON [dbo].[monMonitorTypeGet] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorTypeGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[monMonitorTypeGetAll]

AS

SELECT * FROM [dbo].[MonitorType]
GO
GRANT EXECUTE ON [dbo].[monMonitorTypeGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorTypeUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorTypeUpsert]
	@MonitorTypeKey int = 0,
	@MonitorTypeName nvarchar(100)

AS
BEGIN
	IF (@MonitorTypeKey = 0)
		INSERT INTO [MonitorType] (MonitorTypeName)
		VALUES (@MonitorTypeName)
	ELSE
		UPDATE [MonitorType] SET MonitorTypeName = @MonitorTypeName
		WHERE MonitorTypeKey = @MonitorTypeKey
END

GO
GRANT EXECUTE ON [dbo].[monMonitorTypeUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monMonitorUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monMonitorUpsert]
	@MonitorKey int = 0,
	@MonitorName nvarchar(100),
	@MonitorTypeKey int,
	@LevelKey int,
	@CategoryKey int

AS
BEGIN
	IF (@MonitorKey = 0)
	BEGIN
		INSERT INTO [Monitor] ([MonitorName], MonitorTypeKey, LevelKey, CategoryKey)
		VALUES (@MonitorName, @MonitorTypeKey, @LevelKey, @CategoryKey)
	END
	ELSE
	BEGIN
		UPDATE [Monitor] SET
			MonitorTypeKey = @MonitorTypeKey,
			MonitorName = @MonitorName,
			LevelKey = @LevelKey,
			CategoryKey = @CategoryKey
		WHERE MonitorKey = @MonitorKey
	END
END

GO
GRANT EXECUTE ON [dbo].[monMonitorUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monQueryDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monQueryDelete]
	@QueryKey int

AS
BEGIN
	DELETE FROM [Query]
	WHERE QueryKey = @QueryKey
END

GO
GRANT EXECUTE ON [dbo].[monQueryDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monQueryGet]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[monQueryGet]
	@QueryKey int
AS

SELECT * FROM [dbo].[Query] WHERE QueryKey = @QueryKey

GO
GRANT EXECUTE ON [dbo].[monQueryGet] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monQueryGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[monQueryGetAll]

AS

SELECT * FROM [dbo].[Query]
GO
GRANT EXECUTE ON [dbo].[monQueryGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monQueryUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monQueryUpsert]
	@QueryKey int = 0,
	@QueryName nvarchar(100),
	@QueryText nvarchar(max)

AS
BEGIN
	IF (@QueryKey = 0)
	BEGIN
		INSERT INTO [Query] ([name], QueryText)
		VALUES (@QueryName, @QueryText)
	END
	ELSE
	BEGIN
		UPDATE [Query] SET
			[name] = @QueryName,
			QueryText = @QueryText
		WHERE QueryKey = @QueryKey
	END
END

GO
GRANT EXECUTE ON [dbo].[monQueryUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monserviceTypeDelete]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monserviceTypeDelete]
	@ServiceTypeKey int

AS
BEGIN
	DELETE FROM [ServiceType]
	WHERE ServiceTypeKey = @ServiceTypeKey
END

GO
GRANT EXECUTE ON [dbo].[monserviceTypeDelete] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monserviceTypeGet]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[monserviceTypeGet]
	@ServiceTypeKey int

AS

SELECT * FROM [dbo].[ServiceType]
WHERE ServiceTypeKey = @ServiceTypeKey
GO
GRANT EXECUTE ON [dbo].[monserviceTypeGet] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monserviceTypeGetAll]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[monserviceTypeGetAll]

AS

SELECT * FROM [dbo].[ServiceType]
GO
GRANT EXECUTE ON [dbo].[monserviceTypeGetAll] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monserviceTypeUpsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monserviceTypeUpsert]
	@ServiceTypeKey int = 0,
	@ServiceTypeName nvarchar(100)

AS
BEGIN
	IF (@ServiceTypeKey = 0)
		INSERT INTO [ServiceType] (ServiceTypeName)
		VALUES (@ServiceTypeName)
	ELSE
		UPDATE [ServiceType] SET ServiceTypeName = @ServiceTypeName
		WHERE ServiceTypeKey = @ServiceTypeKey
END

GO
GRANT EXECUTE ON [dbo].[monserviceTypeUpsert] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monWaitStatsLogGet]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[monWaitStatsLogGet]
	@ServiceKey int,
	@SampleCount int = null,
	@StartDate datetime = null,
	@EndDate datetime = null

AS

BEGIN
	IF (@SampleCount IS NOT NULL)
	BEGIN
		SELECT *
		FROM WaitStatsLog A JOIN (SELECT DISTINCT TOP (@SampleCount) BatchNo
			FROM WaitStatsLog
			WHERE ServiceKey = @ServiceKey
			ORDER BY BatchNo DESC) B
		ON A.BatchNo = B.BatchNo
	END
	ELSE IF (@StartDate IS NOT NULL AND @EndDate IS NOT NULL)		
		SELECT * FROM WaitStatsLog
		WHERE ServiceKey = @ServiceKey
		AND BatchNo BETWEEN CAST(CONVERT(varchar(8), @StartDate, 112) + '0000' AS bigint)
			AND CAST(CONVERT(varchar(8), @EndDate, 112) + '9999' AS bigint)
		ORDER BY BatchNo DESC 
	ELSE IF (@StartDate IS NOT NULL AND @EndDate IS NULL)		
		SELECT * FROM WaitStatsLog
		WHERE ServiceKey = @ServiceKey
		AND BatchNo BETWEEN CAST(CONVERT(varchar(8), @StartDate, 112) + '0000' AS bigint)
			AND CAST(CONVERT(varchar(8), @StartDate, 112) + '9999' AS bigint)
		ORDER BY BatchNo DESC 
END
GO
GRANT EXECUTE ON [dbo].[monWaitStatsLogGet] TO [QIQOMonitorUser] AS [dbo]
GO
/****** Object:  StoredProcedure [dbo].[monWaitStatsLogInsert]    Script Date: 12/5/2020 2:43:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[monWaitStatsLogInsert]
	@BatchNo	bigint
	,@ServiceKey	int
	,@WaitType	nvarchar(120)
	,@WaitPercentage	decimal
	,@AvgWaitSec	decimal
	,@AvgResSec	decimal
	,@AvgSigSec	decimal
	,@WaitSec	decimal
	,@ResourceSec	decimal
	,@SignalSec	decimal
	,@WaitCount	bigint

AS
BEGIN
	DECLARE @WaitTypeKey int;
	SELECT @WaitTypeKey = WaitTypeKey FROM WaitType WHERE WaitType = @WaitType;
	INSERT INTO WaitStatsLog (BatchNo
		,ServiceKey
		,WaitTypeKey
		,WaitPercentage
		,AvgWaitSec
		,AvgResSec
		,AvgSigSec
		,WaitSec
		,ResourceSec
		,SignalSec
		,WaitCount)
	VALUES (@BatchNo
		,@ServiceKey
		,@WaitTypeKey
		,@WaitPercentage
		,@AvgWaitSec
		,@AvgResSec
		,@AvgSigSec
		,@WaitSec
		,@ResourceSec
		,@SignalSec
		,@WaitCount)
END
GO
GRANT EXECUTE ON [dbo].[monWaitStatsLogInsert] TO [QIQOMonitorUser] AS [dbo]
GO
USE [master]
GO
ALTER DATABASE [QIQOMonitor] SET  READ_WRITE 
GO
