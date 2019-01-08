CREATE TABLE [dbo].[Lop] (
    [Id]                  VARCHAR (50)   DEFAULT (newid()) NOT NULL,
    [Ten]                 NVARCHAR (MAX) NULL,
    [ZoomMin]             FLOAT (53)     NULL,
    [ZoomMax]             FLOAT (53)     NULL,
    [CapDiaGioiHanhChinh] INT            NULL,
    [Status]              BIT            NULL,
    CONSTRAINT [PK_Lop] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lấy level bên bảng của country', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Lop', @level2type = N'COLUMN', @level2name = N'CapDiaGioiHanhChinh';

