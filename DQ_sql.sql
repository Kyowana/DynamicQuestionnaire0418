USE [DynamicQuestionnaire]
GO
/****** Object:  Table [dbo].[AnsContents]    Script Date: 2022/5/11 下午 06:07:02 ******/
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
/****** Object:  Table [dbo].[AnsSummarys]    Script Date: 2022/5/11 下午 06:07:02 ******/
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
/****** Object:  Table [dbo].[Faqs]    Script Date: 2022/5/11 下午 06:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faqs](
	[FaqID] [uniqueidentifier] NOT NULL,
	[QuestionNumber] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](100) NOT NULL,
	[AnswerOption] [nvarchar](500) NULL,
	[Qtype] [int] NOT NULL,
	[IsRequired] [bit] NOT NULL,
 CONSTRAINT [PK_Faqs] PRIMARY KEY CLUSTERED 
(
	[FaqID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QSummarys]    Script Date: 2022/5/11 下午 06:07:02 ******/
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
/****** Object:  Table [dbo].[Questions]    Script Date: 2022/5/11 下午 06:07:02 ******/
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
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'234c3e2e-0f25-4b62-b7b6-1d43612d7961', N'add204b5-ce66-4a17-8ab4-df4d9355965a', N'8527eb4c-9f67-4fde-ae99-8102e1c39ad6', N'888')
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'9e8ad607-8478-42d4-b2a8-3b4cafb764f0', N'3d9ccad9-2c41-493f-9e16-153a0fb16a6a', N'8527eb4c-9f67-4fde-ae99-8102e1c39ad6', N'555')
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'22ed6277-d7ab-4fa3-a3fe-4880e7640215', N'3ebd636c-4111-4f59-8592-244a913a3830', N'a63f513d-cc29-41df-a99f-2057aea77636', N'a63f513d-cc29-41df-a99f-2057aea77636_AnsRdbOption1;')
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'bbc152e4-789c-43e0-9666-4d2646ffff59', N'add204b5-ce66-4a17-8ab4-df4d9355965a', N'a63f513d-cc29-41df-a99f-2057aea77636', N'a63f513d-cc29-41df-a99f-2057aea77636_AnsRdbOption1;')
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'9e28fda0-7332-473e-8e7e-62898e3937d8', N'3ebd636c-4111-4f59-8592-244a913a3830', N'8527eb4c-9f67-4fde-ae99-8102e1c39ad6', N'777')
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'875c2021-774a-4a54-8313-72adc111e09f', N'3a3ae91f-a1af-4b1d-822a-2ad7221c5f54', N'6ce171ef-f3ff-43c4-b48f-f684e9280711', N'6ce171ef-f3ff-43c4-b48f-f684e9280711_AnsCkbOption0;6ce171ef-f3ff-43c4-b48f-f684e9280711_AnsCkbOption2;')
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'898b672f-1c29-406f-b1af-792fcad86fb7', N'fdf2222a-ae04-4631-afa1-8b7f9941d2f4', N'a63f513d-cc29-41df-a99f-2057aea77636', N'a63f513d-cc29-41df-a99f-2057aea77636_AnsRdbOption0;')
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'8e5d7229-4c27-4f4d-93db-ad831ea8eb36', N'fdf2222a-ae04-4631-afa1-8b7f9941d2f4', N'8527eb4c-9f67-4fde-ae99-8102e1c39ad6', N'666')
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'a61acc42-c5e1-475e-b062-b7443d94a9a8', N'3a3ae91f-a1af-4b1d-822a-2ad7221c5f54', N'be927706-bd5b-4bd1-8fbc-20b6462bafd7', N'be927706-bd5b-4bd1-8fbc-20b6462bafd7_AnsCkbOption1;be927706-bd5b-4bd1-8fbc-20b6462bafd7_AnsCkbOption2;')
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'aba81ba9-41a9-46be-acd1-bc186bc4b68d', N'2b8d6596-b741-4ae5-b895-8d1d3dbba4b4', N'6ce171ef-f3ff-43c4-b48f-f684e9280711', N'')
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'9bd77178-830f-41ec-9bac-e0ebc6197ca3', N'2b8d6596-b741-4ae5-b895-8d1d3dbba4b4', N'be927706-bd5b-4bd1-8fbc-20b6462bafd7', N'be927706-bd5b-4bd1-8fbc-20b6462bafd7_AnsCkbOption1;')
INSERT [dbo].[AnsContents] ([ID], [AnswerID], [QuestionID], [Answer]) VALUES (N'6e73bff8-6529-4019-8b4a-fdc2dfcd8de8', N'3d9ccad9-2c41-493f-9e16-153a0fb16a6a', N'a63f513d-cc29-41df-a99f-2057aea77636', N'a63f513d-cc29-41df-a99f-2057aea77636_AnsRdbOption1;')
GO
INSERT [dbo].[AnsSummarys] ([AnswerID], [QID], [Name], [Phone], [Email], [Age], [SubmitDate]) VALUES (N'3d9ccad9-2c41-493f-9e16-153a0fb16a6a', N'1da4e160-af19-480c-8efa-b0528f032463', N'haha', N'09', N'aaa@aaa', 20, CAST(N'2022-05-03T17:44:56.677' AS DateTime))
INSERT [dbo].[AnsSummarys] ([AnswerID], [QID], [Name], [Phone], [Email], [Age], [SubmitDate]) VALUES (N'3ebd636c-4111-4f59-8592-244a913a3830', N'1da4e160-af19-480c-8efa-b0528f032463', N'haha2', N'09', N'aaa@aaa', 20, CAST(N'2022-05-06T15:20:50.607' AS DateTime))
INSERT [dbo].[AnsSummarys] ([AnswerID], [QID], [Name], [Phone], [Email], [Age], [SubmitDate]) VALUES (N'3a3ae91f-a1af-4b1d-822a-2ad7221c5f54', N'20d9a6e5-a125-4fb1-8618-104a7d0a5873', N'haha45', N'09', N'aaa@aaa', 20, CAST(N'2022-05-08T19:25:34.837' AS DateTime))
INSERT [dbo].[AnsSummarys] ([AnswerID], [QID], [Name], [Phone], [Email], [Age], [SubmitDate]) VALUES (N'fdf2222a-ae04-4631-afa1-8b7f9941d2f4', N'1da4e160-af19-480c-8efa-b0528f032463', N'haha1', N'09', N'aaa@aaa', 20, CAST(N'2022-05-06T15:17:57.463' AS DateTime))
INSERT [dbo].[AnsSummarys] ([AnswerID], [QID], [Name], [Phone], [Email], [Age], [SubmitDate]) VALUES (N'2b8d6596-b741-4ae5-b895-8d1d3dbba4b4', N'20d9a6e5-a125-4fb1-8618-104a7d0a5873', N'haha789', N'09', N'aaa@aaa', 20, CAST(N'2022-05-08T19:03:06.960' AS DateTime))
INSERT [dbo].[AnsSummarys] ([AnswerID], [QID], [Name], [Phone], [Email], [Age], [SubmitDate]) VALUES (N'add204b5-ce66-4a17-8ab4-df4d9355965a', N'1da4e160-af19-480c-8efa-b0528f032463', N'haha4', N'09', N'aaa@aaa', 20, CAST(N'2022-05-06T15:21:46.283' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Faqs] ON 

INSERT [dbo].[Faqs] ([FaqID], [QuestionNumber], [Question], [AnswerOption], [Qtype], [IsRequired]) VALUES (N'36f97ad0-87c5-47fe-b8c0-3afefed09623', 1, N'吃飽沒', N'還沒;吃飽了', 1, 1)
INSERT [dbo].[Faqs] ([FaqID], [QuestionNumber], [Question], [AnswerOption], [Qtype], [IsRequired]) VALUES (N'c6da4c42-1bdb-4b3c-a5ce-a502c8679e85', 2, N'芒芭柳', N'芒;芭;柳', 2, 1)
SET IDENTITY_INSERT [dbo].[Faqs] OFF
GO
SET IDENTITY_INSERT [dbo].[QSummarys] ON 

INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'20d9a6e5-a125-4fb1-8618-104a7d0a5873', 12, N'測試58', N'勾勾勾', CAST(N'2022-05-08T00:00:00.000' AS DateTime), CAST(N'2022-05-12T00:00:00.000' AS DateTime), 1)
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
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'a63f513d-cc29-41df-a99f-2057aea77636', N'1da4e160-af19-480c-8efa-b0528f032463', NULL, N'222', N'123;456', 1, 0, CAST(N'2022-04-20T15:49:32.323' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'be927706-bd5b-4bd1-8fbc-20b6462bafd7', N'20d9a6e5-a125-4fb1-8618-104a7d0a5873', NULL, N'勾起來', N'不管是阿Q;還是來一客;都是統一', 2, 1, CAST(N'2022-05-08T18:41:29.500' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'5fd84cd5-b11c-45c7-bb39-22fdabcf68c2', N'f8eb4416-48aa-4fcd-9a31-faea52f378a1', NULL, N'444555', N'123;456', 2, 1, CAST(N'2022-04-20T16:14:13.090' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'ca17a438-6982-48d8-b739-3a3739b7056e', N'c6e1683c-6f9a-4d1f-8509-a31f3431eada', NULL, N'哈囉', N'123;456', 1, 1, CAST(N'2022-04-21T14:21:51.630' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'd93e3e8b-24e2-408c-ad35-6efd890633f5', N'761d4c3e-7b16-4e17-a994-672d38944fe9', NULL, N'111222', N'', 3, 1, CAST(N'2022-04-22T17:10:12.867' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'0fb8e0a6-6e33-4e39-ace4-809b5e59db28', N'c6e1683c-6f9a-4d1f-8509-a31f3431eada', NULL, N'嗨', N'', 3, 1, CAST(N'2022-04-21T14:22:07.283' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'8527eb4c-9f67-4fde-ae99-8102e1c39ad6', N'1da4e160-af19-480c-8efa-b0528f032463', NULL, N'111', N'', 3, 1, CAST(N'2022-04-22T14:32:05.190' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'9e263cdf-57b4-43a2-9106-b11e820f1f11', N'1eccc01c-eeeb-4e11-84ff-79b9d1a3f6fe', NULL, N'111', N'456;789', 1, 1, CAST(N'2022-04-19T15:19:00.403' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'e9bb06e3-fcf3-4787-9177-b7085a585498', N'761d4c3e-7b16-4e17-a994-672d38944fe9', NULL, N'111', N'', 3, 1, CAST(N'2022-04-21T14:06:06.417' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'9e16f90f-2344-4452-9447-d52ece620e9f', N'11197eca-7120-4a91-ba9b-af205901a6e7', NULL, N'222', N'456;789', 1, 0, CAST(N'2022-04-19T16:33:11.167' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'07417031-8931-41dd-84f5-dc9ba8f753be', N'761d4c3e-7b16-4e17-a994-672d38944fe9', NULL, N'2223456', N'45;678', 2, 1, CAST(N'2022-04-21T14:06:44.033' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [AnswerOption], [QType], [isRequired], [CreateDate]) VALUES (N'6ce171ef-f3ff-43c4-b48f-f684e9280711', N'20d9a6e5-a125-4fb1-8618-104a7d0a5873', NULL, N'可以不用勾', N'123;456;789', 2, 0, CAST(N'2022-05-08T18:41:47.283' AS DateTime))
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
