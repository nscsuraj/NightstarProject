USE [SMN2016]
GO

/****** Object:  Table [dbo].[OurIntegrations]    Script Date: 06/18/2019 10:55:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OurIntegrations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CMSContent] [nvarchar](max) NULL,
	[Title] [nvarchar](max) NULL,
	[OrderBy] [int] NULL,
 CONSTRAINT [PK_OurIntegrations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


GO


