CREATE TABLE [dbo].[ThongTinDoiTuongPhu] (
    [Id]                      VARCHAR (50)   NOT NULL,
    [Code]                    VARCHAR (100)  NULL,
    [Value]                   NVARCHAR (MAX) NULL,
    [ThongTinDoiTuongChinhId] VARCHAR (50)   NOT NULL,
    [KieuDuLieu]              VARCHAR (200)  NULL,
    [Ten]                     NVARCHAR (MAX) NULL,
    [Order]                   INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK__ThongTin__3214EC0757DCB64A] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK__ThongTinD__Thong__3F466844] FOREIGN KEY ([ThongTinDoiTuongChinhId]) REFERENCES [dbo].[ThongTinDoiTuongChinh] ([Id])
);

