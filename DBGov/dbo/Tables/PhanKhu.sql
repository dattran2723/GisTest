CREATE TABLE [dbo].[PhanKhu] (
    [Id]                   VARCHAR (50)   NOT NULL,
    [TenPhanKhu]           NVARCHAR (MAX) NULL,
    [DiaGioiHanhChinhCode] NVARCHAR (20)  NULL,
    [Lat]                  FLOAT (53)     NULL,
    [Lng]                  FLOAT (53)     NULL,
    [Zoom]                 FLOAT (53)     NULL,
    [ParentId]             VARCHAR (50)   NULL,
    [TenTimKiem]           VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_PhanKhu] PRIMARY KEY CLUSTERED ([Id] ASC)
);

