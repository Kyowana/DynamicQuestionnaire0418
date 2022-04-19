USE [DynamicQuestionnaire]
GO
/****** Object:  Table [dbo].[QSummarys]    Script Date: 2022/4/19 下午 03:23:43 ******/
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
/****** Object:  Table [dbo].[Questions]    Script Date: 2022/4/19 下午 03:23:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questions](
	[QuestionID] [uniqueidentifier] NULL,
	[QID] [uniqueidentifier] NOT NULL,
	[QuestionNumber] [int] NULL,
	[Question] [nvarchar](100) NOT NULL,
	[QType] [int] NOT NULL,
	[isRequired] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[QSummarys] ON 

INSERT [dbo].[QSummarys] ([QID], [SerialNumber], [Caption], [Description], [StartDate], [EndDate], [ViewLimit]) VALUES (N'1eccc01c-eeeb-4e11-84ff-79b9d1a3f6fe', 1, N'測試1', N'12345', CAST(N'2022-04-19T00:00:00.000' AS DateTime), CAST(N'2022-05-06T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[QSummarys] OFF
GO
INSERT [dbo].[Questions] ([QuestionID], [QID], [QuestionNumber], [Question], [QType], [isRequired], [CreateDate]) VALUES (N'9e263cdf-57b4-43a2-9106-b11e820f1f11', N'1eccc01c-eeeb-4e11-84ff-79b9d1a3f6fe', NULL, N'111', 1, 1, CAST(N'2022-04-19T15:19:00.403' AS DateTime))
GO
ALTER TABLE [dbo].[QSummarys] ADD  CONSTRAINT [DF_QSummary_ID]  DEFAULT (newid()) FOR [QID]
GO
ALTER TABLE [dbo].[Questions] ADD  CONSTRAINT [DF_Questions_QuestionID]  DEFAULT (newid()) FOR [QuestionID]
GO
ALTER TABLE [dbo].[Questions]  WITH CHECK ADD  CONSTRAINT [FK_Questions_QSummarys] FOREIGN KEY([QID])
REFERENCES [dbo].[QSummarys] ([QID])
GO
ALTER TABLE [dbo].[Questions] CHECK CONSTRAINT [FK_Questions_QSummarys]
GO
