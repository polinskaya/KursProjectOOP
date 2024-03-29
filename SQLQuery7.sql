USE [master]
GO
/****** Object:  Database [Электронный журнал]    Script Date: 5/29/2019 3:30:39 PM ******/
CREATE DATABASE [Электронный журнал]
GO
USE [Электронный журнал]
GO
/****** Object:  Table [dbo].[Аудитории]    Script Date: 5/29/2019 3:30:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Аудитории](
	[ID аудитории] [int] IDENTITY(1,1) NOT NULL,
	[Типа аудитории]]] [int] NOT NULL,
	[Вместимость] [int] NULL,
	[Название] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Аудитори__62E8EE8B2CFD4708] PRIMARY KEY CLUSTERED 
(
	[ID аудитории] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Группы]    Script Date: 5/29/2019 3:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Группы](
	[ID группы] [int] IDENTITY(1,1) NOT NULL,
	[Курс]  AS (datepart(year,getdate())-[Год поступления]),
	[Тип] [int] NULL,
	[Номер группы] [nvarchar](100) NOT NULL,
	[Код факультета] [char](10) NOT NULL,
	[Код специальности] [char](20) NOT NULL,
	[Год поступления] [bigint] NULL,
 CONSTRAINT [PK__Группы__384C2F294F94D795] PRIMARY KEY CLUSTERED 
(
	[ID группы] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Дисциплины]    Script Date: 5/29/2019 3:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Дисциплины](
	[ID дисциплины] [int] IDENTITY(1,1) NOT NULL,
	[Название дисциплины] [nvarchar](100) NOT NULL,
	[ID преподавателя] [int] NULL,
	[Код кафедры] [char](20) NOT NULL,
 CONSTRAINT [PK__Дисципли__15CA0331990FFABF] PRIMARY KEY CLUSTERED 
(
	[ID дисциплины] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Журнал]    Script Date: 5/29/2019 3:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Журнал](
	[ID Учащегося] [int] NULL,
	[ID дисциплины] [int] NULL,
	[ID преподавателя] [int] NULL,
	[Дата] [datetime] NOT NULL,
	[Оценки] [int] NOT NULL,
	[Пропуски] [varchar](100) NOT NULL,
	[ID журнала] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Журнал] PRIMARY KEY CLUSTERED 
(
	[ID журнала] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Занятие]    Script Date: 5/29/2019 3:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Занятие](
	[ID Занятия] [int] IDENTITY(1,1) NOT NULL,
	[[ID Расписания] [int] NOT NULL,
	[Неделя] [int] NOT NULL,
	[ID Дисциплины] [int] NOT NULL,
	[ID Группы] [int] NOT NULL,
	[ДатаИВремя] [datetime] NOT NULL,
	[ID Аудитории] [int] NULL,
 CONSTRAINT [PK_Занятие] PRIMARY KEY CLUSTERED 
(
	[ID Занятия] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Кафедры]    Script Date: 5/29/2019 3:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Кафедры](
	[Код кафедры] [char](20) NOT NULL,
	[Наименование кафедры] [varchar](100) NULL,
	[Код факультета] [char](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Код кафедры] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Преподаватели]    Script Date: 5/29/2019 3:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Преподаватели](
	[ID преподавателя] [int] IDENTITY(1,1) NOT NULL,
	[Фамилия преподавателя] [nvarchar](20) NULL,
	[Имя преподавателя] [nvarchar](20) NULL,
	[Отчество преподавателя] [nvarchar](20) NULL,
	[Пол] [int] NULL,
	[Код кафедры] [char](20) NULL,
 CONSTRAINT [PK__Преподав__122DA71D03257A1B] PRIMARY KEY CLUSTERED 
(
	[ID преподавателя] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Расписание]    Script Date: 5/29/2019 3:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Расписание](
	[ID Расписания] [int] IDENTITY(1,1) NOT NULL,
	[ID Преподавателя] [int] NOT NULL,
 CONSTRAINT [PK_Расписание] PRIMARY KEY CLUSTERED 
(
	[ID Расписания] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Регистрация]    Script Date: 5/29/2019 3:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Регистрация](
	[Логин] [nvarchar](30) NULL,
	[Пароль] [nvarchar](30) NULL,
	[Роли] [int] NULL,
	[Ид пользователя] [int] NULL,
	[ID регистрации] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Регистрация] PRIMARY KEY CLUSTERED 
(
	[ID регистрации] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Специальности]    Script Date: 5/29/2019 3:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Специальности](
	[Код специальности] [char](20) NOT NULL,
	[Код факультета] [char](10) NOT NULL,
	[Наименование специальности] [nvarchar](100) NULL,
	[Квалификация] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Код специальности] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Учащиеся]    Script Date: 5/29/2019 3:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Учащиеся](
	[ID Учащегося] [int] IDENTITY(1,1) NOT NULL,
	[Фамилия Учащегося] [nvarchar](20) NOT NULL,
	[Имя Учащегося] [nvarchar](20) NOT NULL,
	[Отчество Учащегося] [nvarchar](20) NOT NULL,
	[ID группы] [int] NULL,
	[Дополнительная информация] [xml] NULL,
	[Пол] [int] NULL,
 CONSTRAINT [PK__Учащиеся__D41D2400C5FEA763] PRIMARY KEY CLUSTERED 
(
	[ID Учащегося] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Факультет]    Script Date: 5/29/2019 3:30:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Факультет](
	[Код факультета] [char](10) NOT NULL,
	[Наименование факультета] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Код факультета] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Аудитории] ADD  CONSTRAINT [DF__Аудитории__Вмест__59FA5E80]  DEFAULT ((1)) FOR [Вместимость]
GO
ALTER TABLE [dbo].[Группы] ADD  CONSTRAINT [DF__Группы__Тип__4CA06362]  DEFAULT ('очное') FOR [Тип]
GO
ALTER TABLE [dbo].[Учащиеся] ADD  CONSTRAINT [DF__Учащиеся__Дополн__4E88ABD4]  DEFAULT (NULL) FOR [Дополнительная информация]
GO
ALTER TABLE [dbo].[Факультет] ADD  DEFAULT ('???') FOR [Наименование факультета]
GO
ALTER TABLE [dbo].[Группы]  WITH CHECK ADD  CONSTRAINT [FK__Группы__Код факу__5165187F] FOREIGN KEY([Код факультета])
REFERENCES [dbo].[Факультет] ([Код факультета])
GO
ALTER TABLE [dbo].[Группы] CHECK CONSTRAINT [FK__Группы__Код факу__5165187F]
GO
ALTER TABLE [dbo].[Дисциплины]  WITH CHECK ADD  CONSTRAINT [FK__Дисциплин__ID пр__52593CB8] FOREIGN KEY([ID преподавателя])
REFERENCES [dbo].[Преподаватели] ([ID преподавателя])
GO
ALTER TABLE [dbo].[Дисциплины] CHECK CONSTRAINT [FK__Дисциплин__ID пр__52593CB8]
GO
ALTER TABLE [dbo].[Дисциплины]  WITH CHECK ADD  CONSTRAINT [FK__Дисциплин__Код к__534D60F1] FOREIGN KEY([Код кафедры])
REFERENCES [dbo].[Кафедры] ([Код кафедры])
GO
ALTER TABLE [dbo].[Дисциплины] CHECK CONSTRAINT [FK__Дисциплин__Код к__534D60F1]
GO
ALTER TABLE [dbo].[Журнал]  WITH CHECK ADD  CONSTRAINT [FK__Журнал__ID дисци__5441852A] FOREIGN KEY([ID дисциплины])
REFERENCES [dbo].[Дисциплины] ([ID дисциплины])
GO
ALTER TABLE [dbo].[Журнал] CHECK CONSTRAINT [FK__Журнал__ID дисци__5441852A]
GO
ALTER TABLE [dbo].[Журнал]  WITH CHECK ADD  CONSTRAINT [FK__Журнал__ID препо__5535A963] FOREIGN KEY([ID преподавателя])
REFERENCES [dbo].[Преподаватели] ([ID преподавателя])
GO
ALTER TABLE [dbo].[Журнал] CHECK CONSTRAINT [FK__Журнал__ID препо__5535A963]
GO
ALTER TABLE [dbo].[Журнал]  WITH CHECK ADD  CONSTRAINT [FK__Журнал__ID Учаще__5629CD9C] FOREIGN KEY([ID Учащегося])
REFERENCES [dbo].[Учащиеся] ([ID Учащегося])
GO
ALTER TABLE [dbo].[Журнал] CHECK CONSTRAINT [FK__Журнал__ID Учаще__5629CD9C]
GO
ALTER TABLE [dbo].[Занятие]  WITH CHECK ADD  CONSTRAINT [FK_Занятие_Аудитории] FOREIGN KEY([ID Аудитории])
REFERENCES [dbo].[Аудитории] ([ID аудитории])
GO
ALTER TABLE [dbo].[Занятие] CHECK CONSTRAINT [FK_Занятие_Аудитории]
GO
ALTER TABLE [dbo].[Занятие]  WITH CHECK ADD  CONSTRAINT [FK_Занятие_Группы] FOREIGN KEY([ID Группы])
REFERENCES [dbo].[Группы] ([ID группы])
GO
ALTER TABLE [dbo].[Занятие] CHECK CONSTRAINT [FK_Занятие_Группы]
GO
ALTER TABLE [dbo].[Занятие]  WITH CHECK ADD  CONSTRAINT [FK_Занятие_Дисциплины] FOREIGN KEY([ID Дисциплины])
REFERENCES [dbo].[Дисциплины] ([ID дисциплины])
GO
ALTER TABLE [dbo].[Занятие] CHECK CONSTRAINT [FK_Занятие_Дисциплины]
GO
ALTER TABLE [dbo].[Занятие]  WITH CHECK ADD  CONSTRAINT [FK_Занятие_Расписание] FOREIGN KEY([[ID Расписания])
REFERENCES [dbo].[Расписание] ([ID Расписания])
GO
ALTER TABLE [dbo].[Занятие] CHECK CONSTRAINT [FK_Занятие_Расписание]
GO
ALTER TABLE [dbo].[Кафедры]  WITH CHECK ADD FOREIGN KEY([Код факультета])
REFERENCES [dbo].[Факультет] ([Код факультета])
GO
ALTER TABLE [dbo].[Преподаватели]  WITH CHECK ADD  CONSTRAINT [FK__Преподава__Код к__5BE2A6F2] FOREIGN KEY([Код кафедры])
REFERENCES [dbo].[Кафедры] ([Код кафедры])
GO
ALTER TABLE [dbo].[Преподаватели] CHECK CONSTRAINT [FK__Преподава__Код к__5BE2A6F2]
GO
ALTER TABLE [dbo].[Расписание]  WITH CHECK ADD  CONSTRAINT [FK_Расписание_Преподаватели] FOREIGN KEY([ID Преподавателя])
REFERENCES [dbo].[Преподаватели] ([ID преподавателя])
GO
ALTER TABLE [dbo].[Расписание] CHECK CONSTRAINT [FK_Расписание_Преподаватели]
GO
ALTER TABLE [dbo].[Специальности]  WITH CHECK ADD FOREIGN KEY([Код факультета])
REFERENCES [dbo].[Факультет] ([Код факультета])
GO
ALTER TABLE [dbo].[Учащиеся]  WITH CHECK ADD  CONSTRAINT [FK__Учащиеся__ID гру__5EBF139D] FOREIGN KEY([ID группы])
REFERENCES [dbo].[Группы] ([ID группы])
GO
ALTER TABLE [dbo].[Учащиеся] CHECK CONSTRAINT [FK__Учащиеся__ID гру__5EBF139D]
GO
USE [master]
GO
ALTER DATABASE [Электронный журнал] SET  READ_WRITE 
GO
