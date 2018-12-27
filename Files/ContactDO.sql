USE [NslDbInfo]
GO

/****** Object:  Table [dbo].[Contact]    Script Date: 27.12.2018 15:53:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NOT NULL,
	[IdNumber] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Designation] [nvarchar](max) NULL,
	[DateOfJoining] [date] NULL,
	[DateOfBirth] [date] NULL,
	[BloodGroup] [nvarchar](max) NULL,
	[NidNumber] [nvarchar](max) NULL,
	[PassPortNumber] [nvarchar](max) NULL,
	[CurrentAllocation] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Photo] [image] NULL,
	[NidScan] [image] NULL,
	[PassportScan] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


