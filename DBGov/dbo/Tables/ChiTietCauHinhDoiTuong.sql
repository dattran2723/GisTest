CREATE TABLE [dbo].[ChiTietCauHinhDoiTuong] (
    [Id]                 VARCHAR (50)   NOT NULL,
    [Ten]                NVARCHAR (MAX) NULL,
    [Code]               VARCHAR (100)  NULL,
    [KieuDuLieu]         VARCHAR (200)  NULL,
    [DuLieuMacDinh]      NVARCHAR (MAX) NULL,
    [BatBuoc]            BIT            CONSTRAINT [DF_CauHinhDoiTuong_BatBuoc] DEFAULT ((0)) NULL,
    [Mota]               NVARCHAR (MAX) NULL,
    [Status]             BIT            NULL,
    [TrangThaiThuocTinh] BIT            NULL,
    [CauHinhDoiTuongId]  VARCHAR (50)   NOT NULL,
    [Order]              INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CauHinhDoiTuongId]) REFERENCES [dbo].[CauHinhDoiTuong] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cho phép để trống hay không', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ChiTietCauHinhDoiTuong', @level2type = N'COLUMN', @level2name = N'BatBuoc';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Kiểu dữ liệu đầu vào note: số, chữ, tỉnh ...', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ChiTietCauHinhDoiTuong', @level2type = N'COLUMN', @level2name = N'KieuDuLieu';

