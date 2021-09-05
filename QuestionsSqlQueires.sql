USE [master]
GO

/****** Object:  Database [QuestionsDB]    Script Date: 8/31/2021 5:28:09 PM ******/
CREATE DATABASE [QuestionsDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuestionsDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuestionsDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuestionsDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\QuestionsDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuestionsDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [QuestionsDB] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [QuestionsDB] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [QuestionsDB] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [QuestionsDB] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [QuestionsDB] SET ARITHABORT OFF 
GO

ALTER DATABASE [QuestionsDB] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [QuestionsDB] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [QuestionsDB] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [QuestionsDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [QuestionsDB] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [QuestionsDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [QuestionsDB] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [QuestionsDB] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [QuestionsDB] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [QuestionsDB] SET  DISABLE_BROKER 
GO

ALTER DATABASE [QuestionsDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [QuestionsDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [QuestionsDB] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [QuestionsDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [QuestionsDB] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [QuestionsDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [QuestionsDB] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [QuestionsDB] SET RECOVERY FULL 
GO

ALTER DATABASE [QuestionsDB] SET  MULTI_USER 
GO

ALTER DATABASE [QuestionsDB] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [QuestionsDB] SET DB_CHAINING OFF 
GO

ALTER DATABASE [QuestionsDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [QuestionsDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [QuestionsDB] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [QuestionsDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [QuestionsDB] SET QUERY_STORE = OFF
GO

ALTER DATABASE [QuestionsDB] SET  READ_WRITE 
GO

CREATE TABLE [dbo].[AllQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OriginalId] [int] NOT NULL,
	[Text] [varchar](250) NOT NULL,
	[Order] [int] NOT NULL,
	[Type] [varchar](50) NOT NULL,
 CONSTRAINT [PK_AllQuestions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SliderQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](250) NOT NULL,
	[QOrder] [int] NOT NULL,
	[StartValue] [int] NOT NULL,
	[EndValue] [int] NOT NULL,
	[StartValueCaption] [varchar](250) NOT NULL,
	[EndValueCaption] [varchar](250) NOT NULL,
 CONSTRAINT [PK_SliderQuestions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SmileyQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](255) NOT NULL,
	[QOrder] [int] NOT NULL,
	[NumOfSmiley] [int] NOT NULL,
 CONSTRAINT [PK_SmileyQuestions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[StarQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](250) NOT NULL,
	[QOrder] [int] NOT NULL,
	[NumOfStars] [int] NOT NULL,
 CONSTRAINT [PK_StarsQuestions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


 CREATE PROCEDURE Add_StarQuestions
 (@Text VARCHAR(255),    @QOrder  INT,    @NumOfStars INT  )
 AS  
 BEGIN   
	SET XACT_ABORT ON;      
	INSERT INTO StarQuestions VALUES(@Text, @QOrder, @NumOfStars);
	INSERT INTO AllQuestions VALUES(SCOPE_IDENTITY(), @Text, @QOrder, 'Star');
END  
GO

 CREATE PROCEDURE Add_SliderQuestions  
 (    @Text VARCHAR(250),    @QOrder  INT,    @StartValue INT,    @EndValue INT,    @StartValueCaption VARCHAR(250),    @EndValueCaption VARCHAR(250)  )  
 AS  
 BEGIN   
	SET XACT_ABORT ON;
	INSERT INTO SliderQuestions VALUES(@Text, @QOrder, @StartValue, @EndValue, @StartValueCaption, @EndValueCaption);
	INSERT INTO AllQuestions VALUES(SCOPE_IDENTITY(), @Text, @QOrder, 'Slider');
END 
GO

 CREATE PROCEDURE Add_SmileyQuestions  
 (    @Text VARCHAR(255),    @QOrder  INT,    @NumOfSmiley INT  )  
 AS  
 BEGIN   
	SET XACT_ABORT ON;
	INSERT INTO SmileyQuestions VALUES(@Text, @QOrder, @NumOfSmiley);
	INSERT INTO AllQuestions VALUES(SCOPE_IDENTITY(), @Text, @QOrder, 'Smiley');
END  
GO

 CREATE PROCEDURE Update_StarQuestions  
(    @Text VARCHAR(255),    @QOrder  INT,    @NumOfStars INT,    @Id INT  )  
AS  
BEGIN   
	 SET XACT_ABORT ON;      
	 Update StarQuestions SET Text = @Text, QOrder = @QOrder, NumOfStars = @NumOfStars WHERE Id = @Id;   
	 Update AllQuestions SET Text = @Text, QOrder = @QOrder WHERE OriginalId = @Id AND Type = 'Star';  
END 
GO

CREATE PROCEDURE Update_SliderQuestions  
(    @Text VARCHAR(250),    @QOrder  INT,    @StartValue INT,    @EndValue INT,    @StartValueCaption VARCHAR(250),    @EndValueCaption VARCHAR(250),    @Id INT  )
AS  
BEGIN   
	SET XACT_ABORT ON;      
	Update SliderQuestions SET Text = @Text, QOrder = @QOrder, StartValue = @StartValue, EndValue = @EndValue, StartValueCaption = @StartValueCaption, EndValueCaption = @EndValueCaption WHERE Id = @Id;   
	Update AllQuestions SET Text = @Text, QOrder = @QOrder WHERE OriginalId = @Id AND Type = 'Slider';  
END
GO

CREATE PROCEDURE Update_SmileyQuestions  
(    @Text VARCHAR(255),    @QOrder  INT,    @NumOfSmiley INT,    @Id INT  )  
AS  
BEGIN   
	SET XACT_ABORT ON;      
	UPDATE SmileyQuestions SET Text = @Text, QOrder = @QOrder, NumOfSmiley = @NumOfSmiley WHERE Id = @Id;   
	Update AllQuestions SET Text = @Text, QOrder = @QOrder WHERE OriginalId = @Id AND Type = 'Smiley';  
END 
GO

CREATE PROCEDURE Delete_StarQuestions  
(    @Id INT  )
AS  
BEGIN   
	SET XACT_ABORT ON;      
	DELETE FROM StarQuestions where Id = @Id;   
	DELETE FROM AllQuestions where OriginalId = @Id AND Type = 'Star';  
END  
GO

CREATE PROCEDURE Delete_SliderQuestions
(    @Id INT  )  
AS  
BEGIN   
	SET XACT_ABORT ON;      
	DELETE FROM SliderQuestions where Id = @Id;   
	DELETE FROM AllQuestions where OriginalId = @Id AND Type = 'Slider';  
END
GO

CREATE PROCEDURE Delete_SmileyQuestions  
(    @Id INT  )
AS  
BEGIN   
	SET XACT_ABORT ON;      
	DELETE FROM SmileyQuestions where Id = @Id;   
	DELETE FROM AllQuestions where OriginalId = @Id AND Type = 'Smiley';  
END  
GO
