
USE master
GO
IF NOT EXISTS (
    SELECT [name]
        FROM sys.databases
        WHERE [name] = N'LearnAspNetCore'
)
CREATE DATABASE LearnAspNetCore
GO

IF OBJECT_ID('[dbo].[users]', 'U') IS NOT NULL
DROP TABLE [dbo].[users]
GO

CREATE TABLE [dbo].[users]
(
    [id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    [name] NVARCHAR(255) NOT NULL,
    [gender] BIT DEFAULT 1 NOT NULL,
    [birthday] DATE,
    [email] VARCHAR(255),
    [phone] VARCHAR(20),
    [address] NVARCHAR(500)
);
GO

IF OBJECT_ID('[dbo].[user_tokens]', 'U') IS NOT NULL
DROP TABLE [dbo].[user_tokens]
GO
CREATE TABLE [dbo].[user_tokens]
(
    [Id] INT NOT NULL PRIMARY KEY,
    [user_id] INT NOT NULL,
    [token] VARCHAR(255) NOT NULL
    CONSTRAINT FK_USER_TOKEN_USER_ID FOREIGN KEY ([user_id]) REFERENCES users([id])
);
GO