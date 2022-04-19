USE [DynamicQuestionnaire]
GO
/****** Object:  Table [dbo].[QSummarys]    Script Date: 2022/4/19 下午 04:46:32 ******/
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
/****** Object:  Table [dbo].[Questions]    Script Date: 2022/4/19 下午 04:46:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionID] [uniqueidentifier] NOT NULL,
	[QID] [uniqueidentifier] NOT NULL,
	[QuestionNumber] [int] NULL,
	[Question] [nvarchar](100) NOT NULL,
	[QType] [int] NOT NULL,
	[isRequired] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[QSummarys] ON 

INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'5bc5b30e-9e1e-4cb8-8338-02da283592d9', 7, N'測試2', N'456', CAST(N'2022-04-14T00:00:00.000' AS DateTime), CAST(N'2022-04-22T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'162cf0c8-843d-4947-853f-53d8af02e678', 5, N'測試2', N'456', CAST(N'2022-04-28T00:00:00.000' AS DateTime), CAST(N'2022-04-29T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'538295af-73cf-4ebf-a6a4-731339452861', 3, N'測試2', N'456', CAST(N'2022-04-26T00:00:00.000' AS DateTime), CAST(N'2022-04-28T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'1eccc01c-eeeb-4e11-84ff-79b9d1a3f6fe', 1, N'測試1', N'12345', CAST(N'2022-04-19T00:00:00.000' AS DateTime), CAST(N'2022-05-06T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'11197eca-7120-4a91-ba9b-af205901a6e7', 6, N'測試2', N'465', CAST(N'2022-04-06T00:00:00.000' AS DateTime), CAST(N'2022-04-28T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'c5aa68c6-564e-4c5a-9e85-cbb9470434f2', 4, N'測試2', N'456', CAST(N'2022-04-20T00:00:00.000' AS DateTime), CAST(N'2022-04-27T00:00:00.000' AS DateTime), 0)
INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'9ae8e996-e457-4399-b6d8-ef920c85ce3c', 2, N'測試2', N'456', CAST(N'2022-04-06T00:00:00.000' AS DateTime), CAST(N'2022-04-22T00:00:00.000' AS DateTime), 0)
SET IDENTITY_INSERT [dbo].[QSummarys] OFF
GO
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [QType], [isRequired], [CreateDate]) VALUES (N'9e263cdf-57b4-43a2-9106-b11e820f1f11', N'1eccc01c-eeeb-4e11-84ff-79b9d1a3f6fe', NULL, N'111', 1, 1, CAST(N'2022-04-19T15:19:00.403' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [QType], [isRequired], [CreateDate]) VALUES (N'9e16f90f-2344-4452-9447-d52ece620e9f', N'11197eca-7120-4a91-ba9b-af205901a6e7', NULL, N'222', 1, 0, CAST(N'2022-04-19T16:33:11.167' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [QType], [isRequired], [CreateDate]) VALUES (N'1f60b7d0-aac1-4518-83e6-38780235c214', N'5bc5b30e-9e1e-4cb8-8338-02da283592d9', NULL, N'222', 1, 0, CAST(N'2022-04-19T16:40:06.677' AS DateTime))
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [QType], [isRequired], [CreateDate]) VALUES (N'86937d60-6bf5-4995-a93c-4a1d4ba6e063', N'5bc5b30e-9e1e-4cb8-8338-02da283592d9', NULL, N'22222', 1, 0, CAST(N'2022-04-19T16:40:09.697' AS DateTime))
GO
ALTER TABLE [dbo].[QSummarys] ADD  CONSTRAINT [DF_QSummary_ID]  DEFAULT (newid()) FOR [QID]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_QSummarys] FOREIGN KEY([QID])
REFERENCES [dbo].[QSummarys] ([QID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_QSummarys]
GO
