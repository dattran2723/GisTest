CREATE PROCEDURE [dbo].[UpdateThongTinDoiTuong]
	@Id varchar(50),
	@Ten nvarchar(max),
	@SoThua int,
	@SoTo int,
	@LopId varchar(50),
	@DiaGioHanhChinh nvarchar(20),
	@CauHinhDoiTuong varchar(50),
	@PhanKhuId varchar(50),
	@Tag varchar(max),
	@ListThongTin nvarchar(max)
AS
BEGIN
	DECLARE @count int,@countTemp int,@i int
	DECLARE @CodeP varchar(100), @ValueP nvarchar(max),@KieuDuLieuP varchar(200),@TenP nvarchar(max)
	set @count =(select COUNT(*) from ThongTinDoiTuongChinh where Id = @Id)
	If @count > 0 --update
	Begin
		UPDATE [dbo].[ThongTinDoiTuongChinh]
		   SET [Ten] = @Ten
			  ,[SoThua] = @SoThua
			  ,[SoTo] = @SoTo
			  --,[LopId] = @LopId
			  --,[DiaGioiHanhChinhCode] = @DiaGioHanhChinh
			  ,[Lat] = -1
			  ,[Lng] = -1
			  ,[CauHinhDoiTuongId] = @CauHinhDoiTuong
			  ,Tag=@Tag
		 WHERE [Id] = @Id
		 --delete thong tin phu
		 DELETE FROM [dbo].[ThongTinDoiTuongPhu]  WHERE [ThongTinDoiTuongPhu].ThongTinDoiTuongChinhId = @Id
		 --insert thong tin phu
		 select * Into #temp from dbo.parseJSONInforShap(@ListThongTin)
		 set @countTemp = (select COUNT(*) from #temp)
		 set @i =1
		 while @countTemp >= @i
		 Begin
			Select @TenP=Ten,@CodeP=Code, @KieuDuLieuP=KieuDuLieu,@ValueP=DuLieuMacDinh from #temp where RowNum = @i
			INSERT INTO [dbo].[ThongTinDoiTuongPhu]([Id],[Code],[Value],[ThongTinDoiTuongChinhId],[KieuDuLieu],[Ten])
			VALUES (NEWID(),@CodeP,@ValueP,@Id,@KieuDuLieuP,@TenP)
			Set @i=@i+1
		 end
		 select COUNT(*) from [ThongTinDoiTuongChinh] where id =@Id
	 End
	 Else --insert
	 Begin
		INSERT INTO [dbo].[ThongTinDoiTuongChinh]([Id],[Ten],[SoThua],[SoTo],[LopId],[DiaGioiHanhChinhCode],[Lat],[Lng],[CauHinhDoiTuongId],[PhanKhuId],Tag)
			 VALUES(@Id,@Ten,@SoThua,@SoTo,@LopId,@DiaGioHanhChinh,-1,-1,@CauHinhDoiTuong,@PhanKhuId,@Tag)
	    --insert thong tin phu
		 select * Into #tempI from dbo.parseJSONInforShap(@ListThongTin)
		 set @countTemp = (select COUNT(*) from #tempI)
		 set @i =1
		 while @countTemp >= @i
		 Begin
			--DECLARE @CodeP varchar(100), @ValueP nvarchar(max),@KieuDuLieuP varchar(200),@TenP nvarchar(max)
			Select @TenP=Ten,@CodeP=Code, @KieuDuLieuP=KieuDuLieu,@ValueP=DuLieuMacDinh from #tempI where RowNum = @i
			INSERT INTO [dbo].[ThongTinDoiTuongPhu]([Id],[Code],[Value],[ThongTinDoiTuongChinhId],[KieuDuLieu],[Ten])
			VALUES (NEWID(),@CodeP,@ValueP,@Id,@KieuDuLieuP,@TenP)
			Set @i=@i+1
		 end
		 select COUNT(*) from [ThongTinDoiTuongChinh] where id =@Id
	 End
	END


	--exec UpdateThongTinDoiTuong NEWID(),N'hoa',10,20,'11','3','11',N'[{"Ten":"Tỉnh","Code":"tinh","KieuDuLieu":"city","DuLieuMacDinh":""},{"Ten":"Điện tích","Code":"dien_tich","KieuDuLieu":"float","DuLieuMacDinh":"0.0"},{"Ten":"Dân số","Code":"dan_so","KieuDuLieu":"float","DuLieuMacDinh":"0.0"}]'