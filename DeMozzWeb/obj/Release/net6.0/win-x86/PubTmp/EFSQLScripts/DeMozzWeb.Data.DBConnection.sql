IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220701192903_AddCategoryToDB')
BEGIN
    CREATE TABLE [Category] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [DisplayOrder] int NOT NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220701192903_AddCategoryToDB')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220701192903_AddCategoryToDB', N'7.0.0-preview.5.22302.2');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220718083924_ADDCVToDB')
BEGIN
    CREATE TABLE [CV] (
        [Id] int NOT NULL IDENTITY,
        [FN] nvarchar(30) NOT NULL,
        [LN] nvarchar(30) NOT NULL,
        [DateOfBirth] nvarchar(max) NOT NULL,
        [Nationality] nvarchar(max) NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [Skills] nvarchar(max) NULL,
        [Email] nvarchar(max) NOT NULL,
        [File] nvarchar(max) NULL,
        [Grade] int NOT NULL,
        CONSTRAINT [PK_CV] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220718083924_ADDCVToDB')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220718083924_ADDCVToDB', N'7.0.0-preview.5.22302.2');
END;
GO

COMMIT;
GO

