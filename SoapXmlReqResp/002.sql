/****** Object:  Table [dbo].[SoapReqResp]    Script Date: 4/13/2017 12:33:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[XmlSoapRequestResponse](
	[UniqueId] [bigint] NOT NULL IDENTITY(1,1),
	[ApiMethod] [nvarchar](50) NULL,
	[StackMethod] [nvarchar](200) NULL,
	[ApiAction] [nvarchar](100) NULL,
	[ApiOptions] [xml] NULL,
	[ApiObjects] [xml] NULL,
	[ApiResults] [xml] NULL,
	[AdditionalInformation] [xml] NULL,
	[EventDatetime] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO