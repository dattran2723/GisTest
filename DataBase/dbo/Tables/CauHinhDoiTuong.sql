CREATE TABLE [dbo].[CauHinhDoiTuong] (
    [Id]     VARCHAR (50)   NOT NULL,
    [Ten]    NVARCHAR (MAX) NULL,
    [Mota]   NVARCHAR (MAX) NULL,
    [Status] BIT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

