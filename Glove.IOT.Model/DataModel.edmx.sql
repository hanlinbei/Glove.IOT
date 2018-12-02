
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/02/2018 21:20:43
-- Generated from EDMX file: E:\研究生\项目\Glove.IOT\Glove.IOT.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GloveIOTDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoRoleInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoRoleInfo] DROP CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoRoleInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionInfoRoleInfo] DROP CONSTRAINT [FK_ActionInfoRoleInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoRoleInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionInfoRoleInfo] DROP CONSTRAINT [FK_ActionInfoRoleInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoR_UserInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoR_UserInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[RoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[R_UserInfo_ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[UserInfoExt]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoExt];
GO
IF OBJECT_ID(N'[dbo].[UserInfoRoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoRoleInfo];
GO
IF OBJECT_ID(N'[dbo].[ActionInfoRoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfoRoleInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UName] nvarchar(32)  NULL,
    [Pwd] nvarchar(32)  NOT NULL,
    [ShowName] nvarchar(64)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [Remark] nvarchar(64)  NULL,
    [ModfiedOn] datetime  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'RoleInfo'
CREATE TABLE [dbo].[RoleInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(32)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ModfiedOn] datetime  NOT NULL,
    [Remark] nvarchar(64)  NULL,
    [DelFlag] smallint  NOT NULL
);
GO

-- Creating table 'ActionInfo'
CREATE TABLE [dbo].[ActionInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ModfiedOn] datetime  NOT NULL,
    [Remark] nvarchar(64)  NULL,
    [DelFlag] smallint  NOT NULL,
    [Url] nvarchar(512)  NOT NULL,
    [HttpMethd] nvarchar(32)  NULL,
    [ActionName] nvarchar(max)  NOT NULL,
    [IsMenu] bit  NOT NULL,
    [MenuIcon] nvarchar(512)  NULL,
    [Sort] int  NOT NULL
);
GO

-- Creating table 'R_UserInfo_ActionInfo'
CREATE TABLE [dbo].[R_UserInfo_ActionInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HasPermission] bit  NOT NULL,
    [UserInfoId] int  NOT NULL,
    [ActionInfoId] int  NOT NULL,
    [DelFlag] smallint  NOT NULL
);
GO

-- Creating table 'UserInfoExt'
CREATE TABLE [dbo].[UserInfoExt] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserInfoId] int  NOT NULL,
    [Age] int  NOT NULL,
    [Phone] nvarchar(16)  NULL,
    [Email] nvarchar(max)  NULL,
    [DelFlag] smallint  NOT NULL
);
GO

-- Creating table 'UserInfoRoleInfo'
CREATE TABLE [dbo].[UserInfoRoleInfo] (
    [UserInfo_Id] int  NOT NULL,
    [RoleInfo_Id] int  NOT NULL
);
GO

-- Creating table 'ActionInfoRoleInfo'
CREATE TABLE [dbo].[ActionInfoRoleInfo] (
    [ActionInfo_Id] int  NOT NULL,
    [RoleInfo_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RoleInfo'
ALTER TABLE [dbo].[RoleInfo]
ADD CONSTRAINT [PK_RoleInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ActionInfo'
ALTER TABLE [dbo].[ActionInfo]
ADD CONSTRAINT [PK_ActionInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [PK_R_UserInfo_ActionInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserInfoExt'
ALTER TABLE [dbo].[UserInfoExt]
ADD CONSTRAINT [PK_UserInfoExt]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserInfo_Id], [RoleInfo_Id] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [PK_UserInfoRoleInfo]
    PRIMARY KEY CLUSTERED ([UserInfo_Id], [RoleInfo_Id] ASC);
GO

-- Creating primary key on [ActionInfo_Id], [RoleInfo_Id] in table 'ActionInfoRoleInfo'
ALTER TABLE [dbo].[ActionInfoRoleInfo]
ADD CONSTRAINT [PK_ActionInfoRoleInfo]
    PRIMARY KEY CLUSTERED ([ActionInfo_Id], [RoleInfo_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserInfo_Id] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_UserInfo]
    FOREIGN KEY ([UserInfo_Id])
    REFERENCES [dbo].[UserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RoleInfo_Id] in table 'UserInfoRoleInfo'
ALTER TABLE [dbo].[UserInfoRoleInfo]
ADD CONSTRAINT [FK_UserInfoRoleInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_Id])
    REFERENCES [dbo].[RoleInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoRoleInfo_RoleInfo'
CREATE INDEX [IX_FK_UserInfoRoleInfo_RoleInfo]
ON [dbo].[UserInfoRoleInfo]
    ([RoleInfo_Id]);
GO

-- Creating foreign key on [ActionInfo_Id] in table 'ActionInfoRoleInfo'
ALTER TABLE [dbo].[ActionInfoRoleInfo]
ADD CONSTRAINT [FK_ActionInfoRoleInfo_ActionInfo]
    FOREIGN KEY ([ActionInfo_Id])
    REFERENCES [dbo].[ActionInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [RoleInfo_Id] in table 'ActionInfoRoleInfo'
ALTER TABLE [dbo].[ActionInfoRoleInfo]
ADD CONSTRAINT [FK_ActionInfoRoleInfo_RoleInfo]
    FOREIGN KEY ([RoleInfo_Id])
    REFERENCES [dbo].[RoleInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoRoleInfo_RoleInfo'
CREATE INDEX [IX_FK_ActionInfoRoleInfo_RoleInfo]
ON [dbo].[ActionInfoRoleInfo]
    ([RoleInfo_Id]);
GO

-- Creating foreign key on [UserInfoId] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo]
    FOREIGN KEY ([UserInfoId])
    REFERENCES [dbo].[UserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoR_UserInfo_ActionInfo'
CREATE INDEX [IX_FK_UserInfoR_UserInfo_ActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([UserInfoId]);
GO

-- Creating foreign key on [ActionInfoId] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo]
    FOREIGN KEY ([ActionInfoId])
    REFERENCES [dbo].[ActionInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoR_UserInfo_ActionInfo'
CREATE INDEX [IX_FK_ActionInfoR_UserInfo_ActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([ActionInfoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------