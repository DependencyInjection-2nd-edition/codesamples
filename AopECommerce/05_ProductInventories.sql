SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductInventories](
	[Id] [uniqueidentifier] NOT NULL,
	[Quantity] [int] NOT NULL,
CONSTRAINT [PK_ProductInventories] PRIMARY KEY CLUSTERED ([Id] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ProductInventories]  WITH CHECK ADD  CONSTRAINT [FK_ProductInventories_Products] FOREIGN KEY([Id]) REFERENCES [dbo].[Products] ([Id])

GO

ALTER TABLE [dbo].[ProductInventories] CHECK CONSTRAINT [FK_ProductInventories_Products]

GO


