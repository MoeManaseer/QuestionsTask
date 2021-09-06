IF DB_ID('QuestionsDB') IS NULL
	BEGIN
		CREATE DATABASE [QuestionsDB]
	END
GO

USE [QuestionsDB]
GO

IF OBJECT_ID(N'[dbo].[AllQuestions]', N'U') IS NULL  
   CREATE TABLE [dbo].[AllQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OriginalId] [int] NOT NULL,
	[Text] [varchar](250) NOT NULL,
	[Order] [int] NOT NULL,
	[Type] [varchar](50) NOT NULL,
	 CONSTRAINT [PK_AllQuestions] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
	) ON [PRIMARY]
GO

IF OBJECT_ID(N'[dbo].[SliderQuestions]', N'U') IS NULL  
   CREATE TABLE [dbo].[SliderQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](250) NOT NULL,
	[Order] [int] NOT NULL,
	[StartValue] [int] NOT NULL,
	[EndValue] [int] NOT NULL,
	[StartValueCaption] [varchar](250) NOT NULL,
	[EndValueCaption] [varchar](250) NOT NULL,
	 CONSTRAINT [PK_SliderQuestions] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)
	) ON [PRIMARY]
GO

IF OBJECT_ID(N'[dbo].[SmileyQuestions]', N'U') IS NULL  
   CREATE TABLE [dbo].[SmileyQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](255) NOT NULL,
	[Order] [int] NOT NULL,
	[NumOfSmiley] [int] NOT NULL,
	CONSTRAINT [PK_SmileyQuestions] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) 
	) ON [PRIMARY]
GO

IF OBJECT_ID(N'[dbo].[StarQuestions]', N'U') IS NULL  
   
CREATE TABLE [dbo].[StarQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Text] [varchar](250) NOT NULL,
	[Order] [int] NOT NULL,
	[NumOfStars] [int] NOT NULL,
	CONSTRAINT [PK_StarsQuestions] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) 
	) ON [PRIMARY]
GO

CREATE OR ALTER PROCEDURE [dbo].Add_StarQuestions
 (@Text VARCHAR(255),    @Order  INT,    @NumOfStars INT, @Id INT = NULL OUTPUT, @AllQuestionsId INT = NULL OUTPUT  )
 AS  
 BEGIN   
	SET XACT_ABORT ON;      
	INSERT INTO StarQuestions VALUES(@Text, @Order, @NumOfStars);
	SET @Id = SCOPE_IDENTITY();
	INSERT INTO AllQuestions VALUES(@Id, @Text, @Order, 'Star');
	SET @AllQuestionsId = SCOPE_IDENTITY();
END  
GO

CREATE OR ALTER PROCEDURE [dbo].Add_SliderQuestions  
 (    @Text VARCHAR(250),    @Order  INT,    @StartValue INT,    @EndValue INT,    @StartValueCaption VARCHAR(250),    @EndValueCaption VARCHAR(250), @Id INT = NULL OUTPUT, @AllQuestionsId INT = NULL OUTPUT  )  
 AS  
 BEGIN   
	SET XACT_ABORT ON;
	INSERT INTO SliderQuestions VALUES(@Text, @Order, @StartValue, @EndValue, @StartValueCaption, @EndValueCaption);
	SET @Id = SCOPE_IDENTITY();
	INSERT INTO AllQuestions VALUES(@Id, @Text, @Order, 'Slider');
	SET @AllQuestionsId = SCOPE_IDENTITY();
END 
GO

CREATE OR ALTER PROCEDURE [dbo].Add_SmileyQuestions  
 (    @Text VARCHAR(255),    @Order  INT,    @NumOfSmiley INT, @Id INT = NULL OUTPUT, @AllQuestionsId INT = NULL OUTPUT  )  
 AS  
 BEGIN   
	SET XACT_ABORT ON;
	INSERT INTO SmileyQuestions VALUES(@Text, @Order, @NumOfSmiley);
	SET @Id = SCOPE_IDENTITY();
	INSERT INTO AllQuestions VALUES(@Id, @Text, @Order, 'Smiley');
	SET @AllQuestionsId = SCOPE_IDENTITY();
END  
GO

CREATE OR ALTER PROCEDURE [dbo].Update_StarQuestions  
(    @Text VARCHAR(255),    @Order  INT,    @NumOfStars INT,    @Id INT  )  
AS  
BEGIN   
	 SET XACT_ABORT ON;      
	 UPDATE StarQuestions SET Text = @Text, [Order] = @Order, NumOfStars = @NumOfStars WHERE Id = @Id;   
	 UPDATE AllQuestions SET Text = @Text, [Order] = @Order WHERE OriginalId = @Id AND Type = 'Star';  
END 
GO

CREATE OR ALTER PROCEDURE [dbo].Update_SliderQuestions  
(    @Text VARCHAR(250),    @Order  INT,    @StartValue INT,    @EndValue INT,    @StartValueCaption VARCHAR(250),    @EndValueCaption VARCHAR(250),    @Id INT  )
AS  
BEGIN   
	SET XACT_ABORT ON;      
	UPDATE SliderQuestions SET Text = @Text, [Order] = @Order, StartValue = @StartValue, EndValue = @EndValue, StartValueCaption = @StartValueCaption, EndValueCaption = @EndValueCaption WHERE Id = @Id;   
	UPDATE AllQuestions SET Text = @Text, [Order] = @Order WHERE OriginalId = @Id AND Type = 'Slider';  
END
GO

CREATE OR ALTER PROCEDURE [dbo].Update_SmileyQuestions  
(    @Text VARCHAR(255),    @Order  INT,    @NumOfSmiley INT,    @Id INT  )  
AS  
BEGIN   
	SET XACT_ABORT ON;      
	UPDATE SmileyQuestions SET Text = @Text, [Order] = @Order, NumOfSmiley = @NumOfSmiley WHERE Id = @Id;   
	UPDATE AllQuestions SET Text = @Text, [Order] = @Order WHERE OriginalId = @Id AND Type = 'Smiley';  
END 
GO

CREATE OR ALTER PROCEDURE [dbo].Delete_StarQuestions  
(    @Id INT  )
AS  
BEGIN   
	SET XACT_ABORT ON;      
	DELETE FROM StarQuestions WHERE Id = @Id;   
	DELETE FROM AllQuestions WHERE OriginalId = @Id AND Type = 'Star';  
END  
GO

CREATE OR ALTER PROCEDURE [dbo].Delete_SliderQuestions
(    @Id INT  )  
AS  
BEGIN   
	SET XACT_ABORT ON;      
	DELETE FROM SliderQuestions WHERE Id = @Id;   
	DELETE FROM AllQuestions WHERE OriginalId = @Id AND Type = 'Slider';  
END
GO

CREATE OR ALTER PROCEDURE [dbo].Delete_SmileyQuestions  
(    @Id INT  )
AS  
BEGIN   
	SET XACT_ABORT ON;      
	DELETE FROM SmileyQuestions WHERE Id = @Id;   
	DELETE FROM AllQuestions WHERE OriginalId = @Id AND Type = 'Smiley';  
END  
GO