CREATE DATABASE PostsAndComments
GO

USE PostsAndComments
GO

CREATE TABLE Images(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY
	, Link NVARCHAR(MAX) NOT NULL
)
GO

CREATE TABLE Users(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY
	, FirstName NVARCHAR(MAX) NOT NULL
	, LastName NVARCHAR(MAX) NOT NULL
	, Email NVARCHAR(256) UNIQUE NOT NULL
	, HashedPassword NVARCHAR(MAX) NOT NULL
	, ImageId INT NULL FOREIGN KEY REFERENCES Images(Id)
	, Role NVARCHAR(MAX) NOT NULL
)
GO

CREATE TABLE Posts(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY
	, Title NVARCHAR(MAX) NOT NULL
	, Description NVARCHAR(MAX) NOT NULL
	, ImageId INT NULL FOREIGN KEY REFERENCES Images(Id)
)
GO

CREATE TABLE Likes(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY
	, UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
	, PostId INT NOT NULL FOREIGN KEY REFERENCES Posts(Id)
)
GO

CREATE TABLE Comments(
	Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY
	, UserId INT NOT NULL FOREIGN KEY REFERENCES Users(Id)
	, PostId INT NOT NULL FOREIGN KEY REFERENCES Posts(Id)
	, Text NVARCHAR(MAX) NOT NULL
)
GO