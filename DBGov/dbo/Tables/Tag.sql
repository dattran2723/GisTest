CREATE TABLE [dbo].[Tag] (
    [Id]             VARCHAR (50)   NOT NULL,
    [Ten]            NVARCHAR (MAX) NULL,
    [Code]           NVARCHAR (MAX) NULL,
    [Order]          INT            IDENTITY (1, 1) NOT NULL,
    [DaSuDung]       BIT            DEFAULT ((0)) NULL,
    [SuDungThoiGian] BIT            NULL,
    [ThoiGian]       DATETIME       NULL,
    [TenKhongDau]    VARCHAR (MAX)  NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED ([Id] ASC)
);

