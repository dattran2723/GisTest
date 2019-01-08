CREATE TABLE [dbo].[ThongTinVeDoiTuong] (
    [Id]                      VARCHAR (50)   NOT NULL,
    [ThongTinDoiTuongChinhId] VARCHAR (50)   NOT NULL,
    [DuLieuDoiTuong]          NVARCHAR (MAX) NULL,
    [Order]                   INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK__ThongTin__3214EC078FB5BD49] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__ThongTinV__Thong__4222D4EF] FOREIGN KEY ([ThongTinDoiTuongChinhId]) REFERENCES [dbo].[ThongTinDoiTuongChinh] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Lưu string json (geojson)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ThongTinVeDoiTuong', @level2type = N'COLUMN', @level2name = N'DuLieuDoiTuong';

