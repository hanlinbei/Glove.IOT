
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/08/2019 19:05:15
-- Generated from EDMX file: E:\研究生\项目\Glove.IOT\Glove.IOT.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Test];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserInfoR_UserInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_RoleInfo] DROP CONSTRAINT [FK_UserInfoR_UserInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoR_UserInfo_RoleInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_RoleInfo] DROP CONSTRAINT [FK_RoleInfoR_UserInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleInfoR_RoleInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_RoleInfo_ActionInfo] DROP CONSTRAINT [FK_RoleInfoR_RoleInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_R_RoleInfo_ActionInfoActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_RoleInfo_ActionInfo] DROP CONSTRAINT [FK_R_RoleInfo_ActionInfoActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_DeviceInfoDeviceParameterInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DeviceParameterInfo] DROP CONSTRAINT [FK_DeviceInfoDeviceParameterInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_TeamInfoUserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [FK_TeamInfoUserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_GroupInfoUserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfo] DROP CONSTRAINT [FK_GroupInfoUserInfo];
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
IF OBJECT_ID(N'[dbo].[R_UserInfo_RoleInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_UserInfo_RoleInfo];
GO
IF OBJECT_ID(N'[dbo].[ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[R_RoleInfo_ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_RoleInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[DeviceInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeviceInfo];
GO
IF OBJECT_ID(N'[dbo].[DeviceParameterInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DeviceParameterInfo];
GO
IF OBJECT_ID(N'[dbo].[OperationLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OperationLog];
GO
IF OBJECT_ID(N'[dbo].[WarningInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WarningInfo];
GO
IF OBJECT_ID(N'[dbo].[TeamInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TeamInfo];
GO
IF OBJECT_ID(N'[dbo].[GroupInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GroupInfo];
GO
IF OBJECT_ID(N'[dbo].[R_GroupInfo_DeviceInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_GroupInfo_DeviceInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UCode] nvarchar(32)  NOT NULL,
    [UName] nvarchar(32)  NULL,
    [Pwd] nvarchar(64)  NOT NULL,
    [Remark] nvarchar(256)  NULL,
    [Gender] nvarchar(64)  NULL,
    [Picture] nvarchar(512)  NULL,
    [Phone] nvarchar(256)  NULL,
    [Email] nvarchar(max)  NULL,
    [StatusFlag] bit  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [TeamInfoId] int  NOT NULL,
    [GroupInfoId] int  NOT NULL
);
GO

-- Creating table 'RoleInfo'
CREATE TABLE [dbo].[RoleInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(32)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [Remark] nvarchar(64)  NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'R_UserInfo_RoleInfo'
CREATE TABLE [dbo].[R_UserInfo_RoleInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserInfoId] int  NOT NULL,
    [RoleInfoId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'ActionInfo'
CREATE TABLE [dbo].[ActionInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ActionType] nvarchar(256)  NOT NULL,
    [ActionName] nvarchar(max)  NOT NULL,
    [Url] nvarchar(512)  NOT NULL,
    [HttpMethod] nvarchar(512)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [IsDeleted] bit  NULL
);
GO

-- Creating table 'R_RoleInfo_ActionInfo'
CREATE TABLE [dbo].[R_RoleInfo_ActionInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RoleInfoId] int  NOT NULL,
    [ActionInfoId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL
);
GO

-- Creating table 'DeviceInfo'
CREATE TABLE [dbo].[DeviceInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DeviceId] nvarchar(256)  NOT NULL,
    [IsDeleted] bit  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'DeviceParameterInfo'
CREATE TABLE [dbo].[DeviceParameterInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NowOutput] int  NULL,
    [TargetOutput] int  NULL,
    [SingleProgress] smallint  NULL,
    [StatusFlag] nvarchar(256)  NOT NULL,
    [StartTime] datetime  NULL,
    [StopTime] datetime  NULL,
    [SubTime] datetime  NOT NULL,
    [DeviceInfoId] int  NOT NULL
);
GO

-- Creating table 'OperationLog'
CREATE TABLE [dbo].[OperationLog] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UName] nvarchar(256)  NOT NULL,
    [ActionType] nvarchar(256)  NOT NULL,
    [ActionName] nvarchar(256)  NOT NULL,
    [OperationObj] nvarchar(max)  NOT NULL,
    [Ip] nvarchar(max)  NOT NULL,
    [Mac] nvarchar(max)  NULL,
    [IsDeleted] bit  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'WarningInfo'
CREATE TABLE [dbo].[WarningInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DeviceId] nvarchar(256)  NOT NULL,
    [WarningMessage] nvarchar(256)  NOT NULL,
    [StartTime] datetime  NULL,
    [StopTime] datetime  NULL,
    [IsDeleted] bit  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'TeamInfo'
CREATE TABLE [dbo].[TeamInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TName] nvarchar(256)  NULL,
    [StartTime] datetime  NULL,
    [StopTime] datetime  NULL,
    [IsDeleted] bit  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'GroupInfo'
CREATE TABLE [dbo].[GroupInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GName] nvarchar(256)  NULL,
    [IsDeleted] bit  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'R_GroupInfo_DeviceInfo'
CREATE TABLE [dbo].[R_GroupInfo_DeviceInfo] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GroupInfoId] int  NOT NULL,
    [DeviceInfoId] int  NOT NULL,
    [IsDeleted] bit  NOT NULL
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

-- Creating primary key on [Id] in table 'R_UserInfo_RoleInfo'
ALTER TABLE [dbo].[R_UserInfo_RoleInfo]
ADD CONSTRAINT [PK_R_UserInfo_RoleInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ActionInfo'
ALTER TABLE [dbo].[ActionInfo]
ADD CONSTRAINT [PK_ActionInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'R_RoleInfo_ActionInfo'
ALTER TABLE [dbo].[R_RoleInfo_ActionInfo]
ADD CONSTRAINT [PK_R_RoleInfo_ActionInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DeviceInfo'
ALTER TABLE [dbo].[DeviceInfo]
ADD CONSTRAINT [PK_DeviceInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DeviceParameterInfo'
ALTER TABLE [dbo].[DeviceParameterInfo]
ADD CONSTRAINT [PK_DeviceParameterInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OperationLog'
ALTER TABLE [dbo].[OperationLog]
ADD CONSTRAINT [PK_OperationLog]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WarningInfo'
ALTER TABLE [dbo].[WarningInfo]
ADD CONSTRAINT [PK_WarningInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TeamInfo'
ALTER TABLE [dbo].[TeamInfo]
ADD CONSTRAINT [PK_TeamInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'GroupInfo'
ALTER TABLE [dbo].[GroupInfo]
ADD CONSTRAINT [PK_GroupInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'R_GroupInfo_DeviceInfo'
ALTER TABLE [dbo].[R_GroupInfo_DeviceInfo]
ADD CONSTRAINT [PK_R_GroupInfo_DeviceInfo]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserInfoId] in table 'R_UserInfo_RoleInfo'
ALTER TABLE [dbo].[R_UserInfo_RoleInfo]
ADD CONSTRAINT [FK_UserInfoR_UserInfo_RoleInfo]
    FOREIGN KEY ([UserInfoId])
    REFERENCES [dbo].[UserInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoR_UserInfo_RoleInfo'
CREATE INDEX [IX_FK_UserInfoR_UserInfo_RoleInfo]
ON [dbo].[R_UserInfo_RoleInfo]
    ([UserInfoId]);
GO

-- Creating foreign key on [RoleInfoId] in table 'R_UserInfo_RoleInfo'
ALTER TABLE [dbo].[R_UserInfo_RoleInfo]
ADD CONSTRAINT [FK_RoleInfoR_UserInfo_RoleInfo]
    FOREIGN KEY ([RoleInfoId])
    REFERENCES [dbo].[RoleInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleInfoR_UserInfo_RoleInfo'
CREATE INDEX [IX_FK_RoleInfoR_UserInfo_RoleInfo]
ON [dbo].[R_UserInfo_RoleInfo]
    ([RoleInfoId]);
GO

-- Creating foreign key on [RoleInfoId] in table 'R_RoleInfo_ActionInfo'
ALTER TABLE [dbo].[R_RoleInfo_ActionInfo]
ADD CONSTRAINT [FK_RoleInfoR_RoleInfo_ActionInfo]
    FOREIGN KEY ([RoleInfoId])
    REFERENCES [dbo].[RoleInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleInfoR_RoleInfo_ActionInfo'
CREATE INDEX [IX_FK_RoleInfoR_RoleInfo_ActionInfo]
ON [dbo].[R_RoleInfo_ActionInfo]
    ([RoleInfoId]);
GO

-- Creating foreign key on [ActionInfoId] in table 'R_RoleInfo_ActionInfo'
ALTER TABLE [dbo].[R_RoleInfo_ActionInfo]
ADD CONSTRAINT [FK_R_RoleInfo_ActionInfoActionInfo]
    FOREIGN KEY ([ActionInfoId])
    REFERENCES [dbo].[ActionInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_R_RoleInfo_ActionInfoActionInfo'
CREATE INDEX [IX_FK_R_RoleInfo_ActionInfoActionInfo]
ON [dbo].[R_RoleInfo_ActionInfo]
    ([ActionInfoId]);
GO

-- Creating foreign key on [DeviceInfoId] in table 'DeviceParameterInfo'
ALTER TABLE [dbo].[DeviceParameterInfo]
ADD CONSTRAINT [FK_DeviceInfoDeviceParameterInfo]
    FOREIGN KEY ([DeviceInfoId])
    REFERENCES [dbo].[DeviceInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeviceInfoDeviceParameterInfo'
CREATE INDEX [IX_FK_DeviceInfoDeviceParameterInfo]
ON [dbo].[DeviceParameterInfo]
    ([DeviceInfoId]);
GO

-- Creating foreign key on [TeamInfoId] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [FK_TeamInfoUserInfo]
    FOREIGN KEY ([TeamInfoId])
    REFERENCES [dbo].[TeamInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TeamInfoUserInfo'
CREATE INDEX [IX_FK_TeamInfoUserInfo]
ON [dbo].[UserInfo]
    ([TeamInfoId]);
GO

-- Creating foreign key on [GroupInfoId] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [FK_GroupInfoUserInfo]
    FOREIGN KEY ([GroupInfoId])
    REFERENCES [dbo].[GroupInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupInfoUserInfo'
CREATE INDEX [IX_FK_GroupInfoUserInfo]
ON [dbo].[UserInfo]
    ([GroupInfoId]);
GO

-- Creating foreign key on [GroupInfoId] in table 'R_GroupInfo_DeviceInfo'
ALTER TABLE [dbo].[R_GroupInfo_DeviceInfo]
ADD CONSTRAINT [FK_GroupInfoR_GroupInfo_DeviceInfo]
    FOREIGN KEY ([GroupInfoId])
    REFERENCES [dbo].[GroupInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GroupInfoR_GroupInfo_DeviceInfo'
CREATE INDEX [IX_FK_GroupInfoR_GroupInfo_DeviceInfo]
ON [dbo].[R_GroupInfo_DeviceInfo]
    ([GroupInfoId]);
GO

-- Creating foreign key on [DeviceInfoId] in table 'R_GroupInfo_DeviceInfo'
ALTER TABLE [dbo].[R_GroupInfo_DeviceInfo]
ADD CONSTRAINT [FK_R_GroupInfo_DeviceInfoDeviceInfo]
    FOREIGN KEY ([DeviceInfoId])
    REFERENCES [dbo].[DeviceInfo]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_R_GroupInfo_DeviceInfoDeviceInfo'
CREATE INDEX [IX_FK_R_GroupInfo_DeviceInfoDeviceInfo]
ON [dbo].[R_GroupInfo_DeviceInfo]
    ([DeviceInfoId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------