CREATE TABLE [dbo].[ThongTinDoiTuongChinh] (
    [Id]                   VARCHAR (50)   NOT NULL,
    [Ten]                  NVARCHAR (MAX) NULL,
    [SoThua]               INT            NULL,
    [SoTo]                 INT            NULL,
    [LopId]                VARCHAR (50)   NOT NULL,
    [DiaGioiHanhChinhCode] NVARCHAR (20)  NULL,
    [Lat]                  FLOAT (53)     NULL,
    [Lng]                  FLOAT (53)     NULL,
    [CauHinhDoiTuongId]    VARCHAR (50)   NOT NULL,
    [PhanKhuId]            VARCHAR (50)   NULL,
    [Tag]                  VARCHAR (MAX)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CauHinhDoiTuongId]) REFERENCES [dbo].[CauHinhDoiTuong] ([Id]),
    FOREIGN KEY ([LopId]) REFERENCES [dbo].[Lop] ([Id])
);

