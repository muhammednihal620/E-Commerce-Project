USE [CheepCart]
GO

/****** Object:  Table [dbo].[ProductVariants]    Script Date: 21-10-2026 01:50:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductVariants](
	[VariantID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[Color] [varchar](50) NULL,
	[Size] [varchar](50) NULL,
	[Model] [varchar](100) NULL,
	[Price] [decimal](10, 2) NULL,
	[Stock] [int] NULL,
	[MRP] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[VariantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

