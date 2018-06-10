IF OBJECT_ID(N'__EFMigrationsHistory') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180610034211_Initial')
BEGIN
    CREATE TABLE [Battle] (
        [Id] int NOT NULL IDENTITY,
        [EndDate] datetime2 NOT NULL,
        [Name] nvarchar(max) NULL,
        [StartDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Battle] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180610034211_Initial')
BEGIN
    CREATE TABLE [Samurai] (
        [Id] int NOT NULL IDENTITY,
        [BattleId] int NOT NULL,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Samurai] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Samurai_Battle_BattleId] FOREIGN KEY ([BattleId]) REFERENCES [Battle] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180610034211_Initial')
BEGIN
    CREATE TABLE [Quote] (
        [Id] int NOT NULL IDENTITY,
        [SamuraiId] int NOT NULL,
        [Text] nvarchar(max) NULL,
        CONSTRAINT [PK_Quote] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Quote_Samurai_SamuraiId] FOREIGN KEY ([SamuraiId]) REFERENCES [Samurai] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180610034211_Initial')
BEGIN
    CREATE INDEX [IX_Quote_SamuraiId] ON [Quote] ([SamuraiId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180610034211_Initial')
BEGIN
    CREATE INDEX [IX_Samurai_BattleId] ON [Samurai] ([BattleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180610034211_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180610034211_Initial', N'2.0.3-rtm-10026');
END;

GO

