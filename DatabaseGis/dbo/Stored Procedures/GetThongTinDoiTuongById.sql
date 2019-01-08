CREATE PROCEDURE [dbo].[GetThongTinDoiTuongById]
	@Id varchar(50)
AS
BEGIN
	Declare @temp table(Ten nvarchar(max),[Value] nvarchar(max))
	Declare @codeParent nvarchar(20)
	Declare @check nvarchar(20)
	Declare @Ten nvarchar(max)
	Declare @SoThua int
	Declare @SoTo int
	Declare @CheckExit int
	Declare @CheckUrl int
	Set @CheckExit = (select COUNT(*) from ThongTinDoiTuongChinh where Id = @Id)
	If @CheckExit > 0
	Begin

		Set @codeParent =(Select DiaGioiHanhChinhCode from ThongTinDoiTuongChinh where Id = @Id)
		--country
		Set @check = SUBSTRING(@codeParent,1,3)
		If LEN(@check) > 2
		Begin
			Insert into @temp(Ten,[Value])
			select N'Quốc gia' as Ten, [Name] as [Value] from Countries where Code = @check
		End
		--city
		Set @check = SUBSTRING(@codeParent,1,6)
		If LEN(@check) > 5
		Begin
			Insert into @temp(Ten,[Value])
			select N'Thành phố / Tỉnh' as Ten, [Name] as [Value] from Countries where Code = @check
		End
		--district
		Set @check = SUBSTRING(@codeParent,1,9)
		If LEN(@check) > 8
		Begin
			Insert into @temp(Ten,[Value])
			select N'Quận / Huyện' as Ten, [Name] as [Value] from Countries where Code = @check
		End
		--ward
		Set @check = SUBSTRING(@codeParent,1,12)
		If LEN(@check) > 11
		Begin
			Insert into @temp(Ten,[Value])
			select N'Phường / Xã' as Ten, [Name] as [Value] from Countries where Code = @check
		End

		--country on doi tuong
		Insert into @temp(Ten,[Value])
		select ThongTinDoiTuongPhu.Ten,Countries.[Name] as [Value] from ThongTinDoiTuongPhu,Countries where ThongTinDoiTuongPhu.[Value] = Countries.Code and ThongTinDoiTuongChinhId = @Id

		--Ten, so thua, so to
		select @Ten = Ten,@SoThua=SoThua,@SoTo = SoTo from ThongTinDoiTuongChinh where Id = @Id
		Insert into @temp(Ten,[Value]) values (N'Tên',@Ten)
		Insert into @temp(Ten,[Value]) values (N'Số hiệu thửa',@SoThua)
		Insert into @temp(Ten,[Value]) values (N'Số hiệu tờ',@SoTo)

		Insert into @temp(Ten,[Value])
		Select Ten,[Value] from ThongTinDoiTuongPhu where ThongTinDoiTuongChinhId = @Id  and Id not in (select ThongTinDoiTuongPhu.Id from ThongTinDoiTuongPhu,Countries where ThongTinDoiTuongPhu.[Value] = Countries.Code and ThongTinDoiTuongChinhId = @Id) and ThongTinDoiTuongPhu.KieuDuLieu <> 'url'

		Set @CheckUrl = (select COUNT(*) from ThongTinDoiTuongPhu where ThongTinDoiTuongChinhId = @Id and KieuDuLieu = 'url')
		If @CheckUrl > 0
		Begin
			Insert into @temp(Ten,[Value])
			select Ten,'<a href="'+[Value]+'" target="_blank">'+[Value]+'</a>' as [Value] from ThongTinDoiTuongPhu where ThongTinDoiTuongChinhId = @Id and KieuDuLieu = 'url'
		End
		select * from @temp
	End
	Else
	Begin
	   select * from @temp where Ten <> ''
	End
END