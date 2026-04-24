USE [PIME_SITES]
GO

/****** Object:  Table [dbo].[aca_academias]    Script Date: 20/04/2026 11:23:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[aca_academias](
	[aca_id] [int] IDENTITY(1,1) NOT NULL,
	[aca_nombre] [varchar](255) NOT NULL,
	[aca_descripcion] [varchar](max) NULL,
	[aca_url] [varchar](255) NULL,
	[aca_facebook] [varchar](255) NULL,
	[aca_instagram] [varchar](255) NULL,
	[aca_poblacion] [varchar](255) NULL,
	[aca_twitter] [varchar](255) NULL,
	[aca_logo] [varchar](255) NULL,
 CONSTRAINT [PK_aca_academias] PRIMARY KEY CLUSTERED 
(
	[aca_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


USE [PIME_SITES]
GO

/****** Object:  Table [dbo].[aca_academias_categorias]    Script Date: 20/04/2026 11:23:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[aca_academias_categorias](
	[aca_id] [int] NOT NULL,
	[cat_id] [int] NOT NULL,
 CONSTRAINT [PK_aca_academias_categorias] PRIMARY KEY CLUSTERED 
(
	[aca_id] ASC,
	[cat_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[aca_academias_categorias]  WITH CHECK ADD  CONSTRAINT [FK_aca_academias_categorias_aca_academias] FOREIGN KEY([aca_id])
REFERENCES [dbo].[aca_academias] ([aca_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[aca_academias_categorias] CHECK CONSTRAINT [FK_aca_academias_categorias_aca_academias]
GO

ALTER TABLE [dbo].[aca_academias_categorias]  WITH CHECK ADD  CONSTRAINT [FK_aca_academias_categorias_aca_categorias] FOREIGN KEY([cat_id])
REFERENCES [dbo].[aca_categorias] ([cat_id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[aca_academias_categorias] CHECK CONSTRAINT [FK_aca_academias_categorias_aca_categorias]
GO

USE [PIME_SITES]
GO

/****** Object:  Table [dbo].[aca_academias_imagenes]    Script Date: 20/04/2026 11:23:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[aca_academias_imagenes](
	[img_id] [bigint] IDENTITY(1,1) NOT NULL,
	[aca_id] [nvarchar](150) NOT NULL
) ON [PRIMARY]
GO


USE [PIME_SITES]
GO

/****** Object:  Table [dbo].[aca_categorias]    Script Date: 20/04/2026 11:23:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[aca_categorias](
	[cat_id] [int] IDENTITY(1,1) NOT NULL,
	[cat_nombre] [nvarchar](255) NOT NULL,
	[cat_descripcion] [nvarchar](255) NULL,
	[cat_materia] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_aca_categorias] PRIMARY KEY CLUSTERED 
(
	[cat_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO



USE [PIME_SITES]
GO

/****** Object:  Table [dbo].[aca_direcciones]    Script Date: 20/04/2026 11:24:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[aca_direcciones](
	[dir_id] [int] IDENTITY(1,1) NOT NULL,
	[dir_aca_id] [int] NULL,
	[dir_calle] [varchar](255) NULL,
	[dir_numero] [varchar](20) NULL,
	[dir_ciudad] [varchar](255) NULL,
	[dir_provincia] [varchar](255) NULL,
	[dir_codigo_postal] [varchar](10) NULL,
	[dir_longitud] [decimal](10, 6) NULL,
	[dir_latitud] [decimal](10, 6) NULL,
 CONSTRAINT [PK_aca_direcciones] PRIMARY KEY CLUSTERED 
(
	[dir_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[aca_direcciones]  WITH CHECK ADD  CONSTRAINT [FK__aca_direc__dir_a__06EE9736] FOREIGN KEY([dir_aca_id])
REFERENCES [dbo].[aca_academias] ([aca_id])
GO

ALTER TABLE [dbo].[aca_direcciones] CHECK CONSTRAINT [FK__aca_direc__dir_a__06EE9736]
GO

USE [PIME_SITES]
GO

/****** Object:  Table [dbo].[aca_imagenes]    Script Date: 20/04/2026 11:25:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[aca_imagenes](
	[img_id] [bigint] IDENTITY(1,1) NOT NULL,
	[img_path] [nvarchar](150) NULL,
	[img_fecha] [datetime] NULL,
	[img_nombre] [nvarchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[img_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [PIME_SITES]
GO

/****** Object:  Table [dbo].[aca_servicios]    Script Date: 20/04/2026 11:25:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[aca_servicios](
	[ser_id] [int] NOT NULL,
	[ser_nombre] [varchar](255) NOT NULL,
	[ser_descripcion] [varchar](max) NULL,
	[ser_materia] [varchar](50) NOT NULL,
	[ser_aca_id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ser_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[aca_servicios]  WITH CHECK ADD  CONSTRAINT [FK_aca_servicios_aca_academias] FOREIGN KEY([ser_aca_id])
REFERENCES [dbo].[aca_academias] ([aca_id])
GO

ALTER TABLE [dbo].[aca_servicios] CHECK CONSTRAINT [FK_aca_servicios_aca_academias]
GO

ALTER TABLE [dbo].[aca_servicios]  WITH CHECK ADD CHECK  (([ser_materia]='Otros' OR [ser_materia]='Refuerzo Escolar' OR [ser_materia]='Informatica' OR [ser_materia]='Idiomas'))
GO


USE [PIME_SITES]
GO

/****** Object:  Table [dbo].[aca_telefonos]    Script Date: 20/04/2026 11:25:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[aca_telefonos](
	[tel_id] [int] IDENTITY(1,1) NOT NULL,
	[tel_aca_id] [int] NOT NULL,
	[tel_numero] [varchar](20) NOT NULL,
	[tel_nombre] [varchar](100) NULL,
 CONSTRAINT [PK_aca_telefono] PRIMARY KEY CLUSTERED 
(
	[tel_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

