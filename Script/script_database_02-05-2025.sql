USE [master]
GO
/****** Object:  Database [projeto-pf]    Script Date: 02/05/2025 15:37:57 ******/
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
/****** Object:  Table [dbo].[Cidades]    Script Date: 02/05/2025 15:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cidades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[DDD] [nvarchar](5) NOT NULL,
	[IdEstado] [int] NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 02/05/2025 15:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TipoPessoa] [varchar](10) NOT NULL,
	[NomeRazaoSocial] [varchar](150) NOT NULL,
	[ApelidoNomeFantasia] [varchar](150) NULL,
	[DataNascimentoCriacao] [datetime] NULL,
	[CpfCnpj] [varchar](20) NULL,
	[RgInscricaoEstadual] [varchar](20) NULL,
	[Email] [varchar](100) NULL,
	[Telefone] [varchar](20) NULL,
	[Rua] [varchar](150) NULL,
	[Numero] [varchar](10) NULL,
	[Bairro] [varchar](100) NULL,
	[IdCidade] [int] NULL,
	[Cep] [varchar](10) NULL,
	[Classificacao] [varchar](50) NULL,
	[CondicaoPagamentoId] [int] NULL,
	[DataCriacao] [datetime] NULL,
	[DataAtualizacao] [datetime] NULL,
	[Complemento] [varchar](100) NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CondicaoPagamentoParcelas]    Script Date: 02/05/2025 15:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CondicaoPagamentoParcelas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCondicaoPagamento] [int] NULL,
	[IdFormaPagamento] [int] NULL,
	[NumParcela] [int] NULL,
	[Prazo] [int] NULL,
	[Porcentagem] [decimal](18, 2) NULL,
	[DataCriacao] [datetime] NULL,
	[DataAtualizacao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CondicaoPagamentos]    Script Date: 02/05/2025 15:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CondicaoPagamentos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](100) NOT NULL,
	[TaxaJuros] [decimal](5, 2) NULL,
	[Multa] [decimal](5, 2) NULL,
	[DataCriacao] [datetime] NULL,
	[DataAtualizacao] [datetime] NULL,
	[Desconto] [decimal](18, 2) NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estados]    Script Date: 02/05/2025 15:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[UF] [nvarchar](2) NOT NULL,
	[IdPais] [int] NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NOT NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormaPagamentos]    Script Date: 02/05/2025 15:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormaPagamentos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](150) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK_FormaPagamentos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fornecedores]    Script Date: 02/05/2025 15:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fornecedores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TipoPessoa] [varchar](10) NOT NULL,
	[NomeRazaoSocial] [varchar](150) NULL,
	[ApelidoNomeFantasia] [varchar](150) NULL,
	[DataNascimentoCriacao] [datetime] NULL,
	[CpfCnpj] [varchar](20) NULL,
	[RgInscricaoEstadual] [varchar](20) NULL,
	[Email] [varchar](100) NULL,
	[Telefone] [varchar](20) NULL,
	[Rua] [varchar](150) NULL,
	[Numero] [varchar](10) NULL,
	[Bairro] [varchar](100) NULL,
	[IdCidade] [int] NULL,
	[Cep] [varchar](10) NULL,
	[Classificacao] [varchar](50) NULL,
	[CondicaoPagamentoId] [int] NOT NULL,
	[DataCriacao] [datetime] NULL,
	[DataAtualizacao] [datetime] NULL,
	[ValorMinimoPedido] [decimal](18, 2) NULL,
	[Complemento] [varchar](100) NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Funcionarios]    Script Date: 02/05/2025 15:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Funcionarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Matricula] [varchar](20) NOT NULL,
	[NomeRazaoSocial] [varchar](150) NOT NULL,
	[ApelidoNomeFantasia] [varchar](100) NULL,
	[DataNascimentoCriacao] [datetime] NULL,
	[Email] [varchar](100) NOT NULL,
	[Telefone] [varchar](20) NOT NULL,
	[Rua] [varchar](100) NOT NULL,
	[Numero] [varchar](20) NOT NULL,
	[Bairro] [varchar](60) NOT NULL,
	[Cep] [varchar](10) NOT NULL,
	[IdCidade] [int] NOT NULL,
	[CpfCnpj] [varchar](20) NULL,
	[RgInscricaoEstadual] [varchar](30) NULL,
	[Classificacao] [varchar](20) NULL,
	[Cargo] [varchar](50) NOT NULL,
	[Salario] [decimal](10, 2) NOT NULL,
	[DataAdmissao] [date] NOT NULL,
	[Turno] [varchar](20) NOT NULL,
	[CargaHoraria] [varchar](20) NOT NULL,
	[DataDemissao] [date] NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NOT NULL,
	[COMPLEMENTO] [varchar](100) NULL,
	[TipoPessoa] [varchar](20) NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paises]    Script Date: 02/05/2025 15:37:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paises](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](100) NOT NULL,
	[Sigla] [nvarchar](10) NOT NULL,
	[DDI] [nvarchar](5) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NOT NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cidades] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT (getdate()) FOR [DataCriacao]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT (getdate()) FOR [DataAtualizacao]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[CondicaoPagamentoParcelas] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  DEFAULT ((0.00)) FOR [TaxaJuros]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  DEFAULT ((0.00)) FOR [Multa]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  DEFAULT (getdate()) FOR [DataCriacao]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  DEFAULT (getdate()) FOR [DataAtualizacao]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  DEFAULT ((0)) FOR [Desconto]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Estados] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[FormaPagamentos] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Fornecedores] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Funcionarios] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Paises] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Cidades]  WITH CHECK ADD FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estados] ([Id])
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Cidade] FOREIGN KEY([IdCidade])
REFERENCES [dbo].[Cidades] ([Id])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Cidade]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_CondicaoPagamentoCliente] FOREIGN KEY([CondicaoPagamentoId])
REFERENCES [dbo].[CondicaoPagamentos] ([Id])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_CondicaoPagamentoCliente]
GO
ALTER TABLE [dbo].[CondicaoPagamentoParcelas]  WITH CHECK ADD  CONSTRAINT [FK_CondicaoPagamento] FOREIGN KEY([IdCondicaoPagamento])
REFERENCES [dbo].[CondicaoPagamentos] ([Id])
GO
ALTER TABLE [dbo].[CondicaoPagamentoParcelas] CHECK CONSTRAINT [FK_CondicaoPagamento]
GO
ALTER TABLE [dbo].[CondicaoPagamentoParcelas]  WITH CHECK ADD  CONSTRAINT [FK_FormaPagamento] FOREIGN KEY([IdFormaPagamento])
REFERENCES [dbo].[FormaPagamentos] ([Id])
GO
ALTER TABLE [dbo].[CondicaoPagamentoParcelas] CHECK CONSTRAINT [FK_FormaPagamento]
GO
ALTER TABLE [dbo].[Estados]  WITH CHECK ADD FOREIGN KEY([IdPais])
REFERENCES [dbo].[Paises] ([Id])
GO
ALTER TABLE [dbo].[Funcionarios]  WITH CHECK ADD  CONSTRAINT [FK_Funcionarios_Cidades] FOREIGN KEY([IdCidade])
REFERENCES [dbo].[Cidades] ([Id])
GO
ALTER TABLE [dbo].[Funcionarios] CHECK CONSTRAINT [FK_Funcionarios_Cidades]
GO
USE [master]
GO
ALTER DATABASE [projeto-pf] SET  READ_WRITE 
GO
