USE [SMN2016]
GO

/****** Object:  Table [dbo].[ServingSolutions]    Script Date: 06/13/2019 9:29:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ServingSolutions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TitleText] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[TitleTextColor] [nvarchar](max) NULL,
	[TitleTextHoverColor] [nvarchar](max) NULL,
	[HoverImagePath] [nvarchar](max) NULL,
	[LinkUrl] [nvarchar](max) NULL,
	[OrderBy] [int] NULL,
 CONSTRAINT [PK_ServingSolutions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
