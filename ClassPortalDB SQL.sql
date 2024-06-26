USE [master]
GO
/****** Object:  Database [ClassPortalDB]    Script Date: 5/3/2024 1:56:49 AM ******/
CREATE DATABASE [ClassPortalDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ClassPortalDB', FILENAME = N'/var/opt/mssql/data/ClassPortalDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ClassPortalDB_log', FILENAME = N'/var/opt/mssql/data/ClassPortalDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ClassPortalDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ClassPortalDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ClassPortalDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ClassPortalDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ClassPortalDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ClassPortalDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ClassPortalDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ClassPortalDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ClassPortalDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ClassPortalDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ClassPortalDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ClassPortalDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ClassPortalDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ClassPortalDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ClassPortalDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ClassPortalDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ClassPortalDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ClassPortalDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ClassPortalDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ClassPortalDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ClassPortalDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ClassPortalDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ClassPortalDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ClassPortalDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ClassPortalDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ClassPortalDB] SET  MULTI_USER 
GO
ALTER DATABASE [ClassPortalDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ClassPortalDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ClassPortalDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ClassPortalDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ClassPortalDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ClassPortalDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ClassPortalDB', N'ON'
GO
ALTER DATABASE [ClassPortalDB] SET QUERY_STORE = OFF
GO
USE [ClassPortalDB]
GO
/****** Object:  Table [dbo].[tblAdmin]    Script Date: 5/3/2024 1:56:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblAdmin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[Image] [nvarchar](500) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblClass]    Script Date: 5/3/2024 1:56:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblClass](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](100) NOT NULL,
	[GradeLevel] [int] NOT NULL,
	[Image] [nvarchar](500) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[AgeGroups] [nvarchar](150) NOT NULL,
	[Fees] [decimal](18, 0) NOT NULL,
	[StartTiming] [datetime] NOT NULL,
	[EndTiming] [datetime] NOT NULL,
	[MaxClassSize] [int] NOT NULL,
	[InstructorID] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK__tblClass__3214EC07C9DB67AD] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblEnrollment]    Script Date: 5/3/2024 1:56:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblEnrollment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ClassID] [int] NOT NULL,
	[EnrollmentDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblInstructor]    Script Date: 5/3/2024 1:56:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblInstructor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[Gender] [nvarchar](30) NOT NULL,
	[Age] [int] NOT NULL,
	[DateOfJoined] [datetime] NOT NULL,
	[Qualification] [nvarchar](150) NOT NULL,
	[Image] [nvarchar](500) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 5/3/2024 1:56:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[Image] [nvarchar](500) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[CreatedBy] [int] NULL,
	[CreatedAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime] NULL,
	[DeletedBy] [int] NULL,
	[DeletedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblAdmin] ON 

INSERT [dbo].[tblAdmin] ([Id], [FullName], [Image], [Username], [Email], [Password], [Status], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [DeletedBy], [DeletedAt]) VALUES (1, N'Muhammad Ahsan', N'user.png', N'Admin', N'ahnkhan804@gmail.com', N'QqCpBeRy3fgbUsgAXmL95g==', 1, 1, CAST(N'2024-04-14T00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblAdmin] OFF
GO
SET IDENTITY_INSERT [dbo].[tblClass] ON 

INSERT [dbo].[tblClass] ([Id], [ClassName], [GradeLevel], [Image], [Description], [AgeGroups], [Fees], [StartTiming], [EndTiming], [MaxClassSize], [InstructorID], [Status], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [DeletedBy], [DeletedAt]) VALUES (1, N'Mathematics', 3, N'Class\maths.jpg', N'Teaching Mathematics for Grade 3 involves introducing foundational concepts like addition, subtraction, multiplication, and division. Students explore basic arithmetic operations through hands-on activities, visual aids, and interactive exercises. The curriculum aims to develop numeracy skills, problem-solving abilities, and mathematical reasoning in young learners. Emphasis is placed on building a strong understanding of number sense, patterns, and mathematical relationships. Engaging activities and games make learning enjoyable while fostering a solid mathematical foundation for future learning.', N'7-9', CAST(1500 AS Decimal(18, 0)), CAST(N'2024-04-24T21:30:00.000' AS DateTime), CAST(N'2024-04-24T11:00:00.000' AS DateTime), 100, 1, 1, 1, CAST(N'2024-04-23T07:12:48.063' AS DateTime), 1, CAST(N'2024-04-24T07:47:44.253' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblClass] OFF
GO
SET IDENTITY_INSERT [dbo].[tblEnrollment] ON 

INSERT [dbo].[tblEnrollment] ([Id], [UserID], [ClassID], [EnrollmentDate], [Status], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [DeletedBy], [DeletedAt]) VALUES (1, 1, 1, CAST(N'2024-04-24T00:00:00.000' AS DateTime), 1, 1, CAST(N'2024-04-24T08:28:00.460' AS DateTime), 1, CAST(N'2024-04-24T09:50:36.967' AS DateTime), NULL, NULL)
INSERT [dbo].[tblEnrollment] ([Id], [UserID], [ClassID], [EnrollmentDate], [Status], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [DeletedBy], [DeletedAt]) VALUES (4, 2, 1, CAST(N'2024-04-25T00:00:00.000' AS DateTime), 1, 1, CAST(N'2024-04-25T22:05:40.190' AS DateTime), NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblEnrollment] OFF
GO
SET IDENTITY_INSERT [dbo].[tblInstructor] ON 

INSERT [dbo].[tblInstructor] ([Id], [FullName], [Gender], [Age], [DateOfJoined], [Qualification], [Image], [Username], [Email], [Password], [Status], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [DeletedBy], [DeletedAt]) VALUES (1, N'Umair Ahmed Khan', N'Male', 28, CAST(N'2024-04-21T00:00:00.000' AS DateTime), N'Bachelor in English Linguistics', N'Instructor\Ahsan Coat.jpg', N'IUmairKhan', N'ahnkhan841@gmail.com', N'QqCpBeRy3fgbUsgAXmL95g==', 1, 1, CAST(N'2024-04-22T21:18:51.820' AS DateTime), 1, CAST(N'2024-04-22T21:53:21.987' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblInstructor] OFF
GO
SET IDENTITY_INSERT [dbo].[tblUser] ON 

INSERT [dbo].[tblUser] ([Id], [FullName], [Image], [Username], [Email], [Password], [Status], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [DeletedBy], [DeletedAt]) VALUES (1, N'Muhammad Ahsan', N'User\Ahsan Coat.jpg', N'MuhdAhsan', N'ahnkhan8041@gmail.com', N'Op6UJP1qLeTcou9XNX00Yw==', 1, 1, CAST(N'2024-04-19T20:47:44.107' AS DateTime), 1, CAST(N'2024-04-24T21:12:50.160' AS DateTime), NULL, NULL)
INSERT [dbo].[tblUser] ([Id], [FullName], [Image], [Username], [Email], [Password], [Status], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt], [DeletedBy], [DeletedAt]) VALUES (2, N'Ali Khan', N'User\pic.jpg', N'AliKhan', N'alikhan@gmail.com', N'QqCpBeRy3fgbUsgAXmL95g==', 1, 1, CAST(N'2024-04-19T20:55:56.800' AS DateTime), 1, CAST(N'2024-04-22T20:30:43.297' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblUser] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Admin_Email]    Script Date: 5/3/2024 1:56:53 AM ******/
ALTER TABLE [dbo].[tblAdmin] ADD  CONSTRAINT [UQ_Admin_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Admin_Username]    Script Date: 5/3/2024 1:56:53 AM ******/
ALTER TABLE [dbo].[tblAdmin] ADD  CONSTRAINT [UQ_Admin_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Admin_Email]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Admin_Email] ON [dbo].[tblAdmin]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Admin_Password]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Admin_Password] ON [dbo].[tblAdmin]
(
	[Password] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Admin_Status]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Admin_Status] ON [dbo].[tblAdmin]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Admin_Username]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Admin_Username] ON [dbo].[tblAdmin]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_ClassName_GradeLevel_Timing]    Script Date: 5/3/2024 1:56:53 AM ******/
ALTER TABLE [dbo].[tblClass] ADD  CONSTRAINT [UQ_ClassName_GradeLevel_Timing] UNIQUE NONCLUSTERED 
(
	[ClassName] ASC,
	[GradeLevel] ASC,
	[StartTiming] ASC,
	[EndTiming] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Class_ClassName]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Class_ClassName] ON [dbo].[tblClass]
(
	[ClassName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Class_GradeLevel]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Class_GradeLevel] ON [dbo].[tblClass]
(
	[GradeLevel] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Class_MaxClassSize]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Class_MaxClassSize] ON [dbo].[tblClass]
(
	[MaxClassSize] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Class_Status]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Class_Status] ON [dbo].[tblClass]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_Enrollment_User_Class]    Script Date: 5/3/2024 1:56:53 AM ******/
ALTER TABLE [dbo].[tblEnrollment] ADD  CONSTRAINT [UQ_Enrollment_User_Class] UNIQUE NONCLUSTERED 
(
	[UserID] ASC,
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enrollment_ClassID]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Enrollment_ClassID] ON [dbo].[tblEnrollment]
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enrollment_EnrollmentDate]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Enrollment_EnrollmentDate] ON [dbo].[tblEnrollment]
(
	[EnrollmentDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enrollment_Status]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Enrollment_Status] ON [dbo].[tblEnrollment]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Enrollment_UserID]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Enrollment_UserID] ON [dbo].[tblEnrollment]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Instructor_Email]    Script Date: 5/3/2024 1:56:53 AM ******/
ALTER TABLE [dbo].[tblInstructor] ADD  CONSTRAINT [UQ_Instructor_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Instructor_Username]    Script Date: 5/3/2024 1:56:53 AM ******/
ALTER TABLE [dbo].[tblInstructor] ADD  CONSTRAINT [UQ_Instructor_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Instructor_Email]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Instructor_Email] ON [dbo].[tblInstructor]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Instructor_Password]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Instructor_Password] ON [dbo].[tblInstructor]
(
	[Password] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Instructor_Status]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Instructor_Status] ON [dbo].[tblInstructor]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Instructor_Username]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_Instructor_Username] ON [dbo].[tblInstructor]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_User_Email]    Script Date: 5/3/2024 1:56:53 AM ******/
ALTER TABLE [dbo].[tblUser] ADD  CONSTRAINT [UQ_User_Email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_User_Username]    Script Date: 5/3/2024 1:56:53 AM ******/
ALTER TABLE [dbo].[tblUser] ADD  CONSTRAINT [UQ_User_Username] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_Email]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_User_Email] ON [dbo].[tblUser]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_Password]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_User_Password] ON [dbo].[tblUser]
(
	[Password] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_User_Status]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_User_Status] ON [dbo].[tblUser]
(
	[Status] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_User_Username]    Script Date: 5/3/2024 1:56:53 AM ******/
CREATE NONCLUSTERED INDEX [IX_User_Username] ON [dbo].[tblUser]
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblAdmin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_Admin_Created] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblAdmin] CHECK CONSTRAINT [FK_Admin_Admin_Created]
GO
ALTER TABLE [dbo].[tblAdmin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_Admin_Deleted] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblAdmin] CHECK CONSTRAINT [FK_Admin_Admin_Deleted]
GO
ALTER TABLE [dbo].[tblAdmin]  WITH CHECK ADD  CONSTRAINT [FK_Admin_Admin_Updated] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblAdmin] CHECK CONSTRAINT [FK_Admin_Admin_Updated]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [FK_Class_Admin_Created] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [FK_Class_Admin_Created]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [FK_Class_Admin_Deleted] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [FK_Class_Admin_Deleted]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [FK_Class_Admin_Updated] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [FK_Class_Admin_Updated]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [FK_Class_Instructor] FOREIGN KEY([InstructorID])
REFERENCES [dbo].[tblInstructor] ([Id])
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [FK_Class_Instructor]
GO
ALTER TABLE [dbo].[tblEnrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Admin_Created] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblEnrollment] CHECK CONSTRAINT [FK_Enrollment_Admin_Created]
GO
ALTER TABLE [dbo].[tblEnrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Admin_Deleted] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblEnrollment] CHECK CONSTRAINT [FK_Enrollment_Admin_Deleted]
GO
ALTER TABLE [dbo].[tblEnrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Admin_Updated] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblEnrollment] CHECK CONSTRAINT [FK_Enrollment_Admin_Updated]
GO
ALTER TABLE [dbo].[tblEnrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_Class] FOREIGN KEY([ClassID])
REFERENCES [dbo].[tblClass] ([Id])
GO
ALTER TABLE [dbo].[tblEnrollment] CHECK CONSTRAINT [FK_Enrollment_Class]
GO
ALTER TABLE [dbo].[tblEnrollment]  WITH CHECK ADD  CONSTRAINT [FK_Enrollment_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUser] ([Id])
GO
ALTER TABLE [dbo].[tblEnrollment] CHECK CONSTRAINT [FK_Enrollment_User]
GO
ALTER TABLE [dbo].[tblInstructor]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_Admin_Created] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblInstructor] CHECK CONSTRAINT [FK_Instructor_Admin_Created]
GO
ALTER TABLE [dbo].[tblInstructor]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_Admin_Deleted] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblInstructor] CHECK CONSTRAINT [FK_Instructor_Admin_Deleted]
GO
ALTER TABLE [dbo].[tblInstructor]  WITH CHECK ADD  CONSTRAINT [FK_Instructor_Admin_Updated] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblInstructor] CHECK CONSTRAINT [FK_Instructor_Admin_Updated]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_User_Admin_Created] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_User_Admin_Created]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_User_Admin_Deleted] FOREIGN KEY([DeletedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_User_Admin_Deleted]
GO
ALTER TABLE [dbo].[tblUser]  WITH CHECK ADD  CONSTRAINT [FK_User_Admin_Updated] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[tblAdmin] ([Id])
GO
ALTER TABLE [dbo].[tblUser] CHECK CONSTRAINT [FK_User_Admin_Updated]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [CHK_EndTiming] CHECK  (([EndTiming]>getdate()))
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [CHK_EndTiming]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [CHK_GradeLevel] CHECK  (([GradeLevel]>(0)))
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [CHK_GradeLevel]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [CHK_MaxClassSize] CHECK  (([MaxClassSize]>(0)))
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [CHK_MaxClassSize]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [CHK_StartTiming] CHECK  (([StartTiming]>getdate()))
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [CHK_StartTiming]
GO
USE [master]
GO
ALTER DATABASE [ClassPortalDB] SET  READ_WRITE 
GO
