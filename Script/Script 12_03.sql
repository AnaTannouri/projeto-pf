USE [master]
GO
/****** Object:  Database [projeto-pf]    Script Date: 12/03/2025 20:22:24 ******/
CREATE DATABASE [projeto-pf]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'projeto-pf', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\projeto-pf.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'projeto-pf_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\projeto-pf_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [projeto-pf] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [projeto-pf].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [projeto-pf] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [projeto-pf] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [projeto-pf] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [projeto-pf] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [projeto-pf] SET ARITHABORT OFF 
GO
ALTER DATABASE [projeto-pf] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [projeto-pf] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [projeto-pf] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [projeto-pf] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [projeto-pf] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [projeto-pf] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [projeto-pf] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [projeto-pf] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [projeto-pf] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [projeto-pf] SET  DISABLE_BROKER 
GO
ALTER DATABASE [projeto-pf] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [projeto-pf] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [projeto-pf] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [projeto-pf] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [projeto-pf] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [projeto-pf] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [projeto-pf] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [projeto-pf] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [projeto-pf] SET  MULTI_USER 
GO
ALTER DATABASE [projeto-pf] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [projeto-pf] SET DB_CHAINING OFF 
GO
ALTER DATABASE [projeto-pf] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [projeto-pf] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [projeto-pf] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [projeto-pf] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [projeto-pf] SET QUERY_STORE = ON
GO
ALTER DATABASE [projeto-pf] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [projeto-pf]
GO
/****** Object:  Table [dbo].[FormaPagamentos]    Script Date: 12/03/2025 20:22:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormaPagamentos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](150) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
 CONSTRAINT [PK_FormaPagamentos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [projeto-pf] SET  READ_WRITE 
GO
