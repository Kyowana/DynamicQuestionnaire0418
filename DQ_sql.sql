USE [DynamicQuestionnaire]
GO
/****** Object:  Table [dbo].[AnsContents]    Script Date: 2022/4/26 下午 04:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnsContents](
	[ID] [uniqueidentifier] NOT NULL,
	[AnswerID] [uniqueidentifier] NOT NULL,
	[QuestionID] [uniqueidentifier] NOT NULL,
	[Answer] [nvarchar](max) NULL,
 CONSTRAINT [PK_AnsContents] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnsSummarys]    Script Date: 2022/4/26 下午 04:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnsSummarys](
	[AnswerID] [uniqueidentifier] NOT NULL,
	[QID] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Age] [int] NOT NULL,
	[SubmitDate] [datetime] NOT NULL,
 CONSTRAINT [PK_AnsSummarys] PRIMARY KEY CLUSTERED 
(
	[AnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QSummarys]    Script Date: 2022/4/26 下午 04:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QSummarys](
	[QID] [uniqueidentifier] NOT NULL,
	[SerialNumber] [int] IDENTITY(1,1) NOT NULL,
	[Caption] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[ViewLimit] [bit] NOT NULL,
 CONSTRAINT [PK_QSummary] PRIMARY KEY CLUSTERED 
(
	[QID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 2022/4/26 下午 04:29:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionID] [uniqueidentifier] NOT NULL,
	[QID] [uniqueidentifier] NOT NULL,
	[QuestionNumber] [int] NULL,
	[Question] [nvarchar](100) NOT NULL,
	[AnswerOption] [nvarchar](500) NULL,
	[QType] [int] NOT NULL,
	[isRequired] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[QuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[QSummarys] ON 

INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'761d4c3e-7b16-4e17-a994-672d38944fe9', 10, N'測試6', N'4560', CAST(N'2022-04-12T00:00:00.000' AS DateTime), CAST(N'2022-04-29T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'538295af-73cf-4ebf-a6a4-731339452861', 3, N'測試2', N'456', CAST(N'2022-04-26T00:00:00.000' AS DateTime), CAST(N'2022-04-28T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'1eccc01c-eeeb-4e11-84ff-79b9d1a3f6fe', 1, N'測試1', N'12345', CAST(N'2022-04-19T00:00:00.000' AS DateTime), CAST(N'2022-05-06T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'c6e1683c-6f9a-4d1f-8509-a31f3431eada', 11, N'測試7', N'789', CAST(N'2022-04-21T00:00:00.000' AS DateTime), CAST(N'2022-04-28T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'11197eca-7120-4a91-ba9b-af205901a6e7', 6, N'測試2', N'465', CAST(N'2022-04-06T00:00:00.000' AS DateTime), CAST(N'2022-04-28T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'1da4e160-af19-480c-8efa-b0528f032463', 8, N'測試3333', N'7890', CAST(N'2022-04-20T00:00:00.000' AS DateTime), CAST(N'2022-04-21T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'c5aa68c6-564e-4c5a-9e85-cbb9470434f2', 4, N'測試2', N'456', CAST(N'2022-04-20T00:00:00.000' AS DateTime), CAST(N'2022-04-27T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'9ae8e996-e457-4399-b6d8-ef920c85ce3c', 2, N'測試2', N'456', CAST(N'2022-04-06T00:00:00.000' AS DateTime), CAST(N'2022-04-22T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'f8eb4416-48aa-4fcd-9a31-faea52f378a1', 9, N'測試4', N'789', CAST(N'2022-04-21T00:00:00.000' AS DateTime), CAST(N'2022-04-28T00:00:00.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[QSummarys] OFF
GO
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'153da835-6ae2-41f0-a7f8-1d53af8cd8fc', N'f8eb4416-48aa-4fcd-9a31-faea52f378a1', NULL, N'444', N'123', 1, 1, CAST(N'2022-04-20T16:13:51.290' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'a63f513d-cc29-41df-a99f-2057aea77636', N'1da4e160-af19-480c-8efa-b0528f032463', NULL, N'222', N'123;456', 1, 1, CAST(N'2022-04-20T15:49:32.323' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'5fd84cd5-b11c-45c7-bb39-22fdabcf68c2', N'f8eb4416-48aa-4fcd-9a31-faea52f378a1', NULL, N'444555', N'123;456', 2, 1, CAST(N'2022-04-20T16:14:13.090' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'ca17a438-6982-48d8-b739-3a3739b7056e', N'c6e1683c-6f9a-4d1f-8509-a31f3431eada', NULL, N'哈囉', N'123;456', 1, 1, CAST(N'2022-04-21T14:21:51.630' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'd93e3e8b-24e2-408c-ad35-6efd890633f5', N'761d4c3e-7b16-4e17-a994-672d38944fe9', NULL, N'111222', N'', 3, 1, CAST(N'2022-04-22T17:10:12.867' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'0fb8e0a6-6e33-4e39-ace4-809b5e59db28', N'c6e1683c-6f9a-4d1f-8509-a31f3431eada', NULL, N'嗨', N'', 3, 1, CAST(N'2022-04-21T14:22:07.283' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'8527eb4c-9f67-4fde-ae99-8102e1c39ad6', N'1da4e160-af19-480c-8efa-b0528f032463', NULL, N'111', N'', 3, 1, CAST(N'2022-04-22T14:32:05.190' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'9e263cdf-57b4-43a2-9106-b11e820f1f11', N'1eccc01c-eeeb-4e11-84ff-79b9d1a3f6fe', NULL, N'111', N'456;789', 1, 1, CAST(N'2022-04-19T15:19:00.403' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'e9bb06e3-fcf3-4787-9177-b7085a585498', N'761d4c3e-7b16-4e17-a994-672d38944fe9', NULL, N'111', N'', 3, 1, CAST(N'2022-04-21T14:06:06.417' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'9e16f90f-2344-4452-9447-d52ece620e9f', N'11197eca-7120-4a91-ba9b-af205901a6e7', NULL, N'222', N'456;789', 1, 0, CAST(N'2022-04-19T16:33:11.167' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'07417031-8931-41dd-84f5-dc9ba8f753be', N'761d4c3e-7b16-4e17-a994-672d38944fe9', NULL, N'2223456', N'45;678', 2, 1, CAST(N'2022-04-21T14:06:44.033' AS DateTime))
GO
ALTER TABLE [dbo].[QSummarys] ADD  CONSTRAINT [DF_QSummary_ID]  DEFAULT (newid()) FOR [QID]
GO
ALTER TABLE [dbo].[AnsContents]  WITH CHECK ADD  CONSTRAINT [FK_AnsContents_AnsContents] FOREIGN KEY([AnswerID])
REFERENCES [dbo].[AnsSummarys] ([AnswerID])
GO
ALTER TABLE [dbo].[AnsContents] CHECK CONSTRAINT [FK_AnsContents_AnsContents]
GO
ALTER TABLE [dbo].[AnsContents]  WITH CHECK ADD  CONSTRAINT [FK_AnsContents_Questions] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Questions] ([QuestionID])
GO
ALTER TABLE [dbo].[AnsContents] CHECK CONSTRAINT [FK_AnsContents_Questions]
GO
ALTER TABLE [dbo].[AnsSummarys]  WITH CHECK ADD  CONSTRAINT [FK_AnsSummarys_QSummarys] FOREIGN KEY([QID])
REFERENCES [dbo].[QSummarys] ([QID])
GO
ALTER TABLE [dbo].[AnsSummarys] CHECK CONSTRAINT [FK_AnsSummarys_QSummarys]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_QSummarys] FOREIGN KEY([QID])
REFERENCES [dbo].[QSummarys] ([QID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_QSummarys]
GO
