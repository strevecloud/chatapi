USE [chatapi]
GO
/****** Object:  Table [dbo].[Conversation]    Script Date: 04/04/2017 20.54.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conversation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[From_Id] [int] NOT NULL,
	[To_Id] [int] NOT NULL,
	[Timestamp] [varchar](50) NOT NULL,
	[Con_Id] [int] NOT NULL,
 CONSTRAINT [PK_Conversation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ConversationReply]    Script Date: 04/04/2017 20.54.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConversationReply](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Reply] [text] NOT NULL,
	[From_Id] [int] NOT NULL,
	[To_Id] [int] NOT NULL,
	[Timestamp] [varchar](50) NOT NULL,
	[Con_Id] [int] NOT NULL,
 CONSTRAINT [PK_ConversationReply] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tbl_Account]    Script Date: 04/04/2017 20.54.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Account](
	[ID] [varchar](10) NOT NULL,
	[USERNAME] [varchar](20) NULL,
	[PASSWORD] [text] NULL,
	[FULLNAME] [varchar](50) NULL,
 CONSTRAINT [PK_Tbl_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tbl_Conversation]    Script Date: 04/04/2017 20.54.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Conversation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ACCOUNT_FROM] [varchar](10) NULL,
	[ACCOUNT_TO] [varchar](10) NULL,
	[MESSAGE] [text] NULL,
	[POSTED] [datetime] NULL,
	[STATUS] [bit] NULL,
 CONSTRAINT [PK_Tbl_Conversation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tbl_Group]    Script Date: 04/04/2017 20.54.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_Group](
	[ID] [varchar](10) NOT NULL,
	[NAME] [varchar](50) NULL,
 CONSTRAINT [PK_Tbl_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tbl_GroupMember]    Script Date: 04/04/2017 20.54.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tbl_GroupMember](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[GROUP_ID] [varchar](10) NULL,
	[ACCOUNT_MEMBER] [varchar](10) NULL,
 CONSTRAINT [PK_Tbl_GroupMember] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TblUser]    Script Date: 04/04/2017 20.54.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TblUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Photo] [nchar](10) NULL,
	[Status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Conversation] ON 

INSERT [dbo].[Conversation] ([Id], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (3, 2, 1, N'2017-03-24 18:46:19.097', 2)
INSERT [dbo].[Conversation] ([Id], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (5, 3, 1, N'2017-04-01 18:46:19.097', 3)
INSERT [dbo].[Conversation] ([Id], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (7, 3, 2, N'2017-04-01 18:46:19.097', 4)
SET IDENTITY_INSERT [dbo].[Conversation] OFF
SET IDENTITY_INSERT [dbo].[ConversationReply] ON 

INSERT [dbo].[ConversationReply] ([Id], [Reply], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (1, N'Hahaha', 2, 1, N'2017-03-24 18:46:19.097', 2)
INSERT [dbo].[ConversationReply] ([Id], [Reply], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (3, N'Bangsad', 2, 1, N'2017-03-24 18:46:19.097', 2)
INSERT [dbo].[ConversationReply] ([Id], [Reply], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (4, N'Kampret', 1, 2, N'2017-03-24 18:46:19.097', 2)
INSERT [dbo].[ConversationReply] ([Id], [Reply], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (7, N'Koplak', 1, 2, N'2017-03-24 18:46:19.097', 2)
INSERT [dbo].[ConversationReply] ([Id], [Reply], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (9, N'Yoi Bro', 1, 2, N'2017-03-24 18:46:19.097', 2)
INSERT [dbo].[ConversationReply] ([Id], [Reply], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (10, N'Yoben', 2, 1, N'2017-03-24 18:46:19.097', 2)
INSERT [dbo].[ConversationReply] ([Id], [Reply], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (11, N'hallo', 3, 1, N'2017-03-24 18:46:19.097', 3)
INSERT [dbo].[ConversationReply] ([Id], [Reply], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (12, N'test', 3, 2, N'2017-04-01 18:46:19.097', 4)
INSERT [dbo].[ConversationReply] ([Id], [Reply], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (13, N'haha cen', 1, 2, N'2017-04-01 18:46:19.097', 2)
INSERT [dbo].[ConversationReply] ([Id], [Reply], [From_Id], [To_Id], [Timestamp], [Con_Id]) VALUES (14, N'pret', 1, 3, N'2017-04-01 18:46:19.097', 3)
SET IDENTITY_INSERT [dbo].[ConversationReply] OFF
SET IDENTITY_INSERT [dbo].[TblUser] ON 

INSERT [dbo].[TblUser] ([Id], [Name], [Password], [Photo], [Status]) VALUES (1, N'Sefriandsz', N'1234', N'img       ', N'active')
INSERT [dbo].[TblUser] ([Id], [Name], [Password], [Photo], [Status]) VALUES (2, N'Shiro', N'1234', N'gmb       ', N'active')
INSERT [dbo].[TblUser] ([Id], [Name], [Password], [Photo], [Status]) VALUES (3, N'Streve', N'1234', N'pic       ', N'active')
SET IDENTITY_INSERT [dbo].[TblUser] OFF
/****** Object:  StoredProcedure [dbo].[GetlastChat]    Script Date: 04/04/2017 20.54.50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE PROCEDURE [dbo].[GetlastChat]
    @iduser int
	AS   
    SET NOCOUNT ON;  
    select us.Name,usr.Name as Name2,u.Photo,c.Con_Id,cr.Reply,cr.Timestamp,c.From_Id,c.To_Id
	from TblUser u join ConversationReply cr on cr.From_Id=u.Id or cr.To_Id=u.Id
join Conversation c on c.Con_Id=cr.Con_Id 
join TblUser us on c.From_Id = us.Id
join TblUser usr on c.To_Id = usr.Id
where  cr.Id in 
(select max(Id) from ConversationReply cd group by Con_Id)
and u.Id=@iduser

GO
