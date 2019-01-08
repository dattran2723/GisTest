CREATE TABLE [dbo].[Countries] (
    [Id]                   INT            IDENTITY (1, 1) NOT NULL,
    [Name]                 NVARCHAR (100) NULL,
    [Description]          NVARCHAR (100) NULL,
    [Level]                INT            NULL,
    [Type]                 NVARCHAR (100) NULL,
    [ParentId]             INT            DEFAULT ((0)) NULL,
    [ModuleId]             INT            NULL,
    [IsVisible]            INT            DEFAULT ((0)) NULL,
    [CreatedOnDate]        DATETIME       NULL,
    [CreatedByUserId]      INT            NULL,
    [LastModifiedOnDate]   DATETIME       NULL,
    [LastModifiedByUserId] INT            NULL,
    [IsState]              BIT            DEFAULT ((0)) NULL,
    [Code]                 NVARCHAR (20)  NULL,
    [Lat]                  FLOAT (53)     NULL,
    [Lng]                  FLOAT (53)     NULL,
    [NameKhongDau]         VARCHAR (100)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

