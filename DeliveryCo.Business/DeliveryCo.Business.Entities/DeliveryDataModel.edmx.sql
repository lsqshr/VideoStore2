
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/16/2011 23:58:04
-- Generated from EDMX file: C:\Users\Matt\Desktop\PracTaskB\DeliveryCo.Business\DeliveryCo.Business.Entities\DeliveryDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Deliveries];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'DeliveryInfoes'
CREATE TABLE [dbo].[DeliveryInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SourceAddress] nvarchar(max)  NOT NULL,
    [DestinationAddress] nvarchar(max)  NOT NULL,
    [OrderNumber] nvarchar(max)  NOT NULL,
    [DeliveryIdentifier] uniqueidentifier  NOT NULL,
    [DeliveryNotificationAddress] nvarchar(max)  NOT NULL,
    [Status] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'DeliveryInfoes'
ALTER TABLE [dbo].[DeliveryInfoes]
ADD CONSTRAINT [PK_DeliveryInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------