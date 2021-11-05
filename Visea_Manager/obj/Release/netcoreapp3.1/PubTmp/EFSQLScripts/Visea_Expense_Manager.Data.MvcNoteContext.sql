IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120111046_InitialCreate')
BEGIN
    CREATE TABLE [Note] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Date] datetime2 NOT NULL,
        [Type] nvarchar(max) NULL,
        [Price] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Note] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120111046_InitialCreate')
BEGIN
    CREATE TABLE [Zfile] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [note] nvarchar(max) NULL,
        [path] nvarchar(max) NULL,
        CONSTRAINT [PK_Zfile] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120111046_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200120111046_InitialCreate', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120123615_adduserparam')
BEGIN
    ALTER TABLE [Zfile] ADD [User] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120123615_adduserparam')
BEGIN
    ALTER TABLE [Note] ADD [User] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120123615_adduserparam')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200120123615_adduserparam', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120145131_addfilenoteid')
BEGIN
    ALTER TABLE [Zfile] ADD [NoteId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120145131_addfilenoteid')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200120145131_addfilenoteid', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120145901_addfilenoteid1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200120145901_addfilenoteid1', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120150131_addfilenoteid2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200120150131_addfilenoteid2', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120154407_addnotestate')
BEGIN
    ALTER TABLE [Note] ADD [State] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200120154407_addnotestate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200120154407_addnotestate', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121094935_floatpricenote')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Note]') AND [c].[name] = N'Price');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Note] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Note] ALTER COLUMN [Price] real NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121094935_floatpricenote')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200121094935_floatpricenote', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121100640_prix')
BEGIN
    ALTER TABLE [Note] ADD [Prix] real NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121100640_prix')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200121100640_prix', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121102525_New_DataAnnotations')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Note]') AND [c].[name] = N'Prix');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Note] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Note] ALTER COLUMN [Prix] decimal(18, 2) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121102525_New_DataAnnotations')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200121102525_New_DataAnnotations', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121110410_pricetodeci')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Note]') AND [c].[name] = N'Price');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Note] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Note] ALTER COLUMN [Price] decimal(18,2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121110410_pricetodeci')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200121110410_pricetodeci', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121110608_pricetodeciandcolumd')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Note]') AND [c].[name] = N'Price');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Note] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Note] ALTER COLUMN [Price] decimal(18, 2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121110608_pricetodeciandcolumd')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200121110608_pricetodeciandcolumd', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121111711_pricerange')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Note]') AND [c].[name] = N'Price');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Note] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Note] ALTER COLUMN [Price] decimal(18,2) NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121111711_pricerange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200121111711_pricerange', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121134736_pricerangeregex')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200121134736_pricerangeregex', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200121135739_pricerangemesoule')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200121135739_pricerangemesoule', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200123163001_commentarie')
BEGIN
    ALTER TABLE [Note] ADD [commente] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200123163001_commentarie')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200123163001_commentarie', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200123163023_commentarie2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200123163023_commentarie2', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200203104924_Congertable')
BEGIN
    CREATE TABLE [Conger] (
        [Id] int NOT NULL IDENTITY,
        [Title] nvarchar(max) NULL,
        [Date_Debut] datetime2 NOT NULL,
        [Date_Fin] datetime2 NOT NULL,
        [Type] nvarchar(max) NULL,
        [User] nvarchar(max) NULL,
        [State] nvarchar(max) NULL,
        CONSTRAINT [PK_Conger] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200203104924_Congertable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200203104924_Congertable', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200203131321_Congerdirectore')
BEGIN
    ALTER TABLE [Conger] ADD [Director] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200203131321_Congerdirectore')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200203131321_Congerdirectore', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200204094504_Congercomment')
BEGIN
    ALTER TABLE [Conger] ADD [commente] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200204094504_Congercomment')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200204094504_Congercomment', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200204101705_Userinit')
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Role] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Director] nvarchar(max) NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200204101705_Userinit')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200204101705_Userinit', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200205104323_Congerdemijournee')
BEGIN
    ALTER TABLE [Conger] ADD [Demijourne] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200205104323_Congerdemijournee')
BEGIN
    ALTER TABLE [Conger] ADD [time] real NOT NULL DEFAULT CAST(0 AS real);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200205104323_Congerdemijournee')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200205104323_Congerdemijournee', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200205105624_Congerdatetotime')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200205105624_Congerdatetotime', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306094059_grosse creation de model event,client ect ...')
BEGIN
    ALTER TABLE [Users] ADD [Date_of_creation] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306094059_grosse creation de model event,client ect ...')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200306094059_grosse creation de model event,client ect ...', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306095523_grosse creation de model event,client ect ...(le vrais)')
BEGIN
    CREATE TABLE [Client] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [Type_Id] int NOT NULL,
        CONSTRAINT [PK_Client] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306095523_grosse creation de model event,client ect ...(le vrais)')
BEGIN
    CREATE TABLE [Etape_Mission] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Etape_Mission] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306095523_grosse creation de model event,client ect ...(le vrais)')
BEGIN
    CREATE TABLE [Event] (
        [Id] int NOT NULL IDENTITY,
        [Nature] nvarchar(max) NULL,
        [Date] datetime2 NOT NULL,
        [Date_of_creation] datetime2 NOT NULL,
        [Type] nvarchar(max) NULL,
        [Type_Id] int NOT NULL,
        [Classe_Id] int NOT NULL,
        [User] nvarchar(max) NULL,
        [Heures] int NOT NULL,
        [commente] nvarchar(max) NULL,
        CONSTRAINT [PK_Event] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306095523_grosse creation de model event,client ect ...(le vrais)')
BEGIN
    CREATE TABLE [Formation] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [Type_Id] int NOT NULL,
        CONSTRAINT [PK_Formation] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306095523_grosse creation de model event,client ect ...(le vrais)')
BEGIN
    CREATE TABLE [Mission] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [Type_Id] int NOT NULL,
        CONSTRAINT [PK_Mission] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306095523_grosse creation de model event,client ect ...(le vrais)')
BEGIN
    CREATE TABLE [Type_formation] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_Type_formation] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306095523_grosse creation de model event,client ect ...(le vrais)')
BEGIN
    CREATE TABLE [Type_Mission] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Type] nvarchar(max) NULL,
        [Type_Id] int NOT NULL,
        CONSTRAINT [PK_Type_Mission] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306095523_grosse creation de model event,client ect ...(le vrais)')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200306095523_grosse creation de model event,client ect ...(le vrais)', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306164607_refonte de la table apres plein de mauvaise manip')
BEGIN
    CREATE TABLE [Event] (
        [Id] int NOT NULL IDENTITY,
        [Nature] nvarchar(max) NULL,
        [Date] datetime2 NOT NULL,
        [Date_of_creation] datetime2 NOT NULL,
        [Type] nvarchar(max) NULL,
        [Type_Id] int NOT NULL,
        [Classe_Id] int NOT NULL,
        [User] nvarchar(max) NULL,
        [Heures] datetime2 NOT NULL,
        [commente] nvarchar(max) NULL,
        CONSTRAINT [PK_Event] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200306164607_refonte de la table apres plein de mauvaise manip')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200306164607_refonte de la table apres plein de mauvaise manip', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200309110922_classe type event')
BEGIN
    ALTER TABLE [Event] ADD [Classe2_Id] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200309110922_classe type event')
BEGIN
    ALTER TABLE [Event] ADD [Classe3_Id] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200309110922_classe type event')
BEGIN
    ALTER TABLE [Event] ADD [Classe4_Id] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200309110922_classe type event')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200309110922_classe type event', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200309150727_link id clientid formationid ect')
BEGIN
    ALTER TABLE [Type_Mission] ADD [Mission_Id] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200309150727_link id clientid formationid ect')
BEGIN
    ALTER TABLE [Type_formation] ADD [formation_Id] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200309150727_link id clientid formationid ect')
BEGIN
    ALTER TABLE [Mission] ADD [Client_Id] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200309150727_link id clientid formationid ect')
BEGIN
    ALTER TABLE [Etape_Mission] ADD [type_Mission_Id] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200309150727_link id clientid formationid ect')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200309150727_link id clientid formationid ect', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200312150010_Classe str')
BEGIN
    ALTER TABLE [Event] ADD [Classe2_str] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200312150010_Classe str')
BEGIN
    ALTER TABLE [Event] ADD [Classe3_str] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200312150010_Classe str')
BEGIN
    ALTER TABLE [Event] ADD [Classe4_str] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200312150010_Classe str')
BEGIN
    ALTER TABLE [Event] ADD [Classe_str] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200312150010_Classe str')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200312150010_Classe str', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200601163138_distance_work')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200601163138_distance_work', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200603150306_CreationofMonthValidation')
BEGIN
    CREATE TABLE [MonthValidations] (
        [Id] int NOT NULL IDENTITY,
        [UserId] int NOT NULL,
        [Date] datetime2 NOT NULL,
        CONSTRAINT [PK_MonthValidations] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200603150306_CreationofMonthValidation')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200603150306_CreationofMonthValidation', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200715082000_addingphone')
BEGIN
    ALTER TABLE [Users] ADD [Phone] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200715082000_addingphone')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200715082000_addingphone', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200715090857_addingTypeEventDatatable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200715090857_addingTypeEventDatatable', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200715091524_addingTypeEventDatatables')
BEGIN
    CREATE TABLE [TypeEvent] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        CONSTRAINT [PK_TypeEvent] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200715091524_addingTypeEventDatatables')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200715091524_addingTypeEventDatatables', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200716092911_addingCopiercollerindatabase')
BEGIN
    ALTER TABLE [Users] ADD [CopierId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200716092911_addingCopiercollerindatabase')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200716092911_addingCopiercollerindatabase', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200723132324_useridinevet')
BEGIN
    ALTER TABLE [Event] ADD [UserId] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200723132324_useridinevet')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200723132324_useridinevet', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200730104001_initexpresse')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200730104001_initexpresse', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200730104510_initexpresse2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200730104510_initexpresse2', N'3.1.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200730104927_initexpresse3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200730104927_initexpresse3', N'3.1.1');
END;

GO

