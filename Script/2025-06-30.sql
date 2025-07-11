CREATE DATABASE [projeto-pf]
GO
USE [projeto-pf]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cidades]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cidades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](80) NOT NULL,
	[DDD] [nvarchar](3) NOT NULL,
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
/****** Object:  Table [dbo].[Clientes]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TipoPessoa] [varchar](10) NOT NULL,
	[NomeRazaoSocial] [varchar](50) NOT NULL,
	[ApelidoNomeFantasia] [varchar](40) NULL,
	[DataNascimentoCriacao] [datetime] NULL,
	[CpfCnpj] [varchar](18) NULL,
	[RgInscricaoEstadual] [varchar](15) NULL,
	[Email] [varchar](100) NOT NULL,
	[Telefone] [varchar](20) NOT NULL,
	[Rua] [varchar](40) NOT NULL,
	[Numero] [varchar](10) NOT NULL,
	[Bairro] [varchar](40) NOT NULL,
	[IdCidade] [int] NOT NULL,
	[Cep] [varchar](10) NULL,
	[Classificacao] [varchar](20) NULL,
	[CondicaoPagamentoId] [int] NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Complemento] [varchar](40) NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK__Clientes__3214EC07BC2BA1E7] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CondicaoPagamentoParcelas]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CondicaoPagamentoParcelas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCondicaoPagamento] [int] NOT NULL,
	[IdFormaPagamento] [int] NOT NULL,
	[NumParcela] [int] NOT NULL,
	[Prazo] [int] NOT NULL,
	[Porcentagem] [decimal](18, 2) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK__Condicao__3214EC0714F7677A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CondicaoPagamentos]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CondicaoPagamentos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[TaxaJuros] [decimal](5, 2) NOT NULL,
	[Multa] [decimal](5, 2) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Desconto] [decimal](18, 2) NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK__Condicao__3214EC075C33FF8A] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estados]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](80) NOT NULL,
	[UF] [nvarchar](2) NOT NULL,
	[IdPais] [int] NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK__Estados__3214EC07E1F2604F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormaPagamentos]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormaPagamentos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](40) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK_FormaPagamentos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fornecedores]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fornecedores](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TipoPessoa] [varchar](10) NOT NULL,
	[NomeRazaoSocial] [varchar](50) NOT NULL,
	[ApelidoNomeFantasia] [varchar](40) NULL,
	[DataNascimentoCriacao] [datetime] NULL,
	[CpfCnpj] [varchar](18) NULL,
	[RgInscricaoEstadual] [varchar](15) NULL,
	[Email] [varchar](100) NOT NULL,
	[Telefone] [varchar](20) NOT NULL,
	[Rua] [varchar](40) NOT NULL,
	[Numero] [varchar](10) NOT NULL,
	[Bairro] [varchar](40) NOT NULL,
	[IdCidade] [int] NOT NULL,
	[Cep] [varchar](10) NULL,
	[Classificacao] [varchar](20) NULL,
	[CondicaoPagamentoId] [int] NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[ValorMinimoPedido] [decimal](18, 2) NOT NULL,
	[Complemento] [varchar](40) NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK__Forneced__3214EC07B424E11D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Funcionarios]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Funcionarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Matricula] [varchar](20) NOT NULL,
	[NomeRazaoSocial] [varchar](50) NOT NULL,
	[ApelidoNomeFantasia] [varchar](40) NULL,
	[DataNascimentoCriacao] [datetime] NULL,
	[Email] [varchar](100) NOT NULL,
	[Telefone] [varchar](20) NOT NULL,
	[Rua] [varchar](40) NOT NULL,
	[Numero] [varchar](10) NOT NULL,
	[Bairro] [varchar](40) NOT NULL,
	[Cep] [varchar](10) NULL,
	[IdCidade] [int] NOT NULL,
	[CpfCnpj] [varchar](18) NULL,
	[RgInscricaoEstadual] [varchar](15) NULL,
	[Classificacao] [varchar](20) NULL,
	[Cargo] [varchar](20) NOT NULL,
	[Salario] [decimal](10, 2) NOT NULL,
	[DataAdmissao] [date] NOT NULL,
	[Turno] [varchar](20) NOT NULL,
	[CargaHoraria] [varchar](20) NOT NULL,
	[DataDemissao] [date] NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[COMPLEMENTO] [varchar](40) NULL,
	[TipoPessoa] [varchar](20) NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK__Funciona__3214EC07CEB30AE5] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marcas]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marcas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paises]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paises](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](40) NOT NULL,
	[Sigla] [nvarchar](5) NOT NULL,
	[DDI] [nvarchar](5) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produtos]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produtos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[IdUnidadeMedida] [int] NOT NULL,
	[IdCategoria] [int] NOT NULL,
	[IdFornecedor] [int] NOT NULL,
	[IdMarca] [int] NULL,
	[Estoque] [decimal](10, 2) NOT NULL,
	[PrecoCusto] [decimal](10, 2) NOT NULL,
	[PrecoVenda] [decimal](10, 2) NOT NULL,
	[CustoUltimaCompra] [decimal](10, 2) NULL,
	[Observacao] [text] NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
	[MargemLucro] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UnidadeMedidas]    Script Date: 30/06/2025 19:51:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnidadeMedidas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](50) NOT NULL,
	[DataCriacao] [datetime] NOT NULL,
	[DataAtualizacao] [datetime] NULL,
	[Ativo] [bit] NOT NULL,
	[Sigla] [varchar](3) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Cidades] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Clientes] ADD  CONSTRAINT [DF__Clientes__DataCr__2645B050]  DEFAULT (getdate()) FOR [DataCriacao]
GO
ALTER TABLE [dbo].[Clientes] ADD  CONSTRAINT [DF__Clientes__DataAt__2739D489]  DEFAULT (getdate()) FOR [DataAtualizacao]
GO
ALTER TABLE [dbo].[Clientes] ADD  CONSTRAINT [DF__Clientes__Ativo__3493CFA7]  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[CondicaoPagamentoParcelas] ADD  CONSTRAINT [DF__CondicaoP__Ativo__3A4CA8FD]  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  CONSTRAINT [DF__CondicaoP__TaxaJ__6383C8BA]  DEFAULT ((0.00)) FOR [TaxaJuros]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  CONSTRAINT [DF__CondicaoP__Multa__6477ECF3]  DEFAULT ((0.00)) FOR [Multa]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  CONSTRAINT [DF__CondicaoP__DataC__656C112C]  DEFAULT (getdate()) FOR [DataCriacao]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  CONSTRAINT [DF__CondicaoP__DataA__66603565]  DEFAULT (getdate()) FOR [DataAtualizacao]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  CONSTRAINT [DF__CondicaoP__Desco__30C33EC3]  DEFAULT ((0)) FOR [Desconto]
GO
ALTER TABLE [dbo].[CondicaoPagamentos] ADD  CONSTRAINT [DF__CondicaoP__Ativo__3587F3E0]  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Estados] ADD  CONSTRAINT [DF__Estados__Ativo__367C1819]  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[FormaPagamentos] ADD  CONSTRAINT [DF__FormaPaga__Ativo__32AB8735]  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Fornecedores] ADD  CONSTRAINT [DF__Fornecedo__Ativo__37703C52]  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Funcionarios] ADD  CONSTRAINT [DF__Funcionar__Ativo__3864608B]  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Paises] ADD  DEFAULT ((1)) FOR [Ativo]
GO
ALTER TABLE [dbo].[Produtos] ADD  DEFAULT ((0)) FOR [MargemLucro]
GO
ALTER TABLE [dbo].[UnidadeMedidas] ADD  DEFAULT ('') FOR [Sigla]
GO
ALTER TABLE [dbo].[Cidades]  WITH CHECK ADD  CONSTRAINT [FK__Cidades__IdEstad__07C12930] FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estados] ([Id])
GO
ALTER TABLE [dbo].[Cidades] CHECK CONSTRAINT [FK__Cidades__IdEstad__07C12930]
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
ALTER TABLE [dbo].[Estados]  WITH CHECK ADD  CONSTRAINT [FK__Estados__IdPais__04E4BC85] FOREIGN KEY([IdPais])
REFERENCES [dbo].[Paises] ([Id])
GO
ALTER TABLE [dbo].[Estados] CHECK CONSTRAINT [FK__Estados__IdPais__04E4BC85]
GO
ALTER TABLE [dbo].[Fornecedores]  WITH CHECK ADD  CONSTRAINT [FK_Fornecedores_Cidades] FOREIGN KEY([IdCidade])
REFERENCES [dbo].[Cidades] ([Id])
GO
ALTER TABLE [dbo].[Fornecedores] CHECK CONSTRAINT [FK_Fornecedores_Cidades]
GO
ALTER TABLE [dbo].[Fornecedores]  WITH CHECK ADD  CONSTRAINT [FK_Fornecedores_CondicaoPagamentos] FOREIGN KEY([CondicaoPagamentoId])
REFERENCES [dbo].[CondicaoPagamentos] ([Id])
GO
ALTER TABLE [dbo].[Fornecedores] CHECK CONSTRAINT [FK_Fornecedores_CondicaoPagamentos]
GO
ALTER TABLE [dbo].[Funcionarios]  WITH CHECK ADD  CONSTRAINT [FK_Funcionarios_Cidades] FOREIGN KEY([IdCidade])
REFERENCES [dbo].[Cidades] ([Id])
GO
ALTER TABLE [dbo].[Funcionarios] CHECK CONSTRAINT [FK_Funcionarios_Cidades]
GO
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD  CONSTRAINT [FK_Produtos_Categoria] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categorias] ([Id])
GO
ALTER TABLE [dbo].[Produtos] CHECK CONSTRAINT [FK_Produtos_Categoria]
GO
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD  CONSTRAINT [FK_Produtos_Fornecedor] FOREIGN KEY([IdFornecedor])
REFERENCES [dbo].[Fornecedores] ([Id])
GO
ALTER TABLE [dbo].[Produtos] CHECK CONSTRAINT [FK_Produtos_Fornecedor]
GO
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD  CONSTRAINT [FK_Produtos_Marca] FOREIGN KEY([IdMarca])
REFERENCES [dbo].[Marcas] ([Id])
GO
ALTER TABLE [dbo].[Produtos] CHECK CONSTRAINT [FK_Produtos_Marca]
GO
ALTER TABLE [dbo].[Produtos]  WITH CHECK ADD  CONSTRAINT [FK_Produtos_UnidadeMedida] FOREIGN KEY([IdUnidadeMedida])
REFERENCES [dbo].[UnidadeMedidas] ([Id])
GO
ALTER TABLE [dbo].[Produtos] CHECK CONSTRAINT [FK_Produtos_UnidadeMedida]
GO
