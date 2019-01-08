CREATE TABLE [dbo].[ThongTinLatLngDoiTuong] (
    [Id]     VARCHAR (50)  NOT NULL,
    [MaxLat] VARCHAR (200) NULL,
    [MinLat] VARCHAR (200) NULL,
    [MaxLng] VARCHAR (200) NULL,
    [MinLng] VARCHAR (200) NULL,
    [Width]  FLOAT (53)    NULL,
    [Height] FLOAT (53)    NULL,
    CONSTRAINT [PK_ThongTinLatLngDoiTuong] PRIMARY KEY CLUSTERED ([Id] ASC)
);

