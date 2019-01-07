CREATE TABLE [dbo].[ThongTinBuildingDoiTuong] (
    [Id]                      VARCHAR (50)   NOT NULL,
    [ThongTinDoiTuongChinhId] VARCHAR (50)   NOT NULL,
    [DuLieuBuilding]          NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

