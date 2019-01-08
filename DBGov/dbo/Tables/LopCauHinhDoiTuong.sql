CREATE TABLE [dbo].[LopCauHinhDoiTuong] (
    [Id]                VARCHAR (50) NOT NULL,
    [LopId]             VARCHAR (50) NOT NULL,
    [CauHinhDoiTuongId] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CauHinhDoiTuongId]) REFERENCES [dbo].[CauHinhDoiTuong] ([Id]),
    FOREIGN KEY ([LopId]) REFERENCES [dbo].[Lop] ([Id])
);

