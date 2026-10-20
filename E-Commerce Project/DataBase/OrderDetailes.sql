USE [CheepCart]
GO

/****** Object:  Table [dbo].[OrderDetails]    Script Date: 21-10-2026 01:49:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OrderDetails](
	[OrderID] [int] IDENTITY(100,1) NOT NULL,
	[CustomerName] [varchar](100) NULL,
	[Address] [varchar](500) NULL,
	[Phone] [varchar](100) NULL,
	[ProductName] [varchar](100) NULL,
	[Price] [int] NULL,
	[Quantity] [int] NULL,
	[Total] [int] NULL,
	[OrderDate] [datetime] NULL,
	[PaymentMethod] [varchar](30) NULL,
	[Pincode] [varchar](10) NULL,
	[Landmark] [varchar](200) NULL,
	[CustomerID] [int] NULL,
	[OrderStatus] [varchar](30) NULL
) ON [PRIMARY]
GO

