USE QLSanPham
GO

--SanPham--
CREATE PROCEDURE proc_ThemSanPham
    @tenSP NVARCHAR(100), 
	@donGia INT,
	@moTa NVARCHAR(1000),
	@maLoaiSP INT
AS 
BEGIN 
    INSERT INTO SanPham (tenSP, donGia, moTa, maLoaiSP)
    VALUES (@tenSP, @donGia, @moTa, @maLoaiSP)
END 
GO

CREATE PROCEDURE proc_XoaSanPham
    @maSP NVARCHAR(100)
AS 
BEGIN 
    DELETE FROM SanPham 
    WHERE maSP = @maSP 
END 
GO


CREATE PROCEDURE proc_SuaSanPham
    @maSP INT,
    @tenSP NVARCHAR(100), 
	@donGia INT,
	@moTa NVARCHAR(1000),
	@maLoaiSP INT
AS 
BEGIN 
    UPDATE SanPham
    SET tenSP = @tenSP,
        donGia = @donGia,
        moTa = @moTa,
        maLoaiSP = @maLoaiSP
    WHERE maSP = @maSP
END 
GO

create proc proc_LayHetSanPham
as 
begin
	select * from SanPham
end
go


CREATE FUNCTION func_LayTenLoaiSP 
(
    @maLoaiSP INT
)
RETURNS NVARCHAR(100)
AS
BEGIN
    DECLARE @tenLoaiSP NVARCHAR(100);

    SELECT @tenLoaiSP = tenLoaiSP
    FROM LoaiSP
    WHERE maLoaiSP = @maLoaiSP;
    RETURN @tenLoaiSP;
END
GO


-----------------------------------------------------------------------
--Kho--
create proc proc_LayHetKho
as 
begin
	select * from Kho
end
go 

create proc proc_LayHetSanPhamTrongKho
	@maKho int
as 
begin
	select SPThuocKho.maSP, tenSP, soLuong
	from SPThuocKho, SanPham
	where SPThuocKho.maSP = SanPham.maSP
	and SPThuocKho.maKho = @maKho
end
go 

create function func_LaySoLuongSanPhamTrongKho
	(@maSP INT, @maKho INT)
returns INT
as
begin
	declare @soLuong INT;

	select @soLuong = soLuong
	from SPThuocKho
	where maSP = @maSP
		and maKho = @maKho;

	return @soLuong;
end
go


CREATE PROCEDURE proc_ThemSPVaoKho
    @maSP INT,
    @maKho INT,
    @soLuong INT
AS
BEGIN
    INSERT INTO SPThuocKho(maSP, maKho, soLuong) 
    VALUES(@maSP, @maKho, @soLuong)
END
GO


-----------------------------------------------------------
--Cửa Hàng--

create proc proc_LayHetCuaHang
as 
begin
	select * from CuaHang
end
go 

CREATE PROCEDURE proc_BanSPTuCH 
    @maSP INT, 
    @maCH INT, 
    @soLuong INT 
AS 
BEGIN 
    UPDATE dbo.SPThuocCH
    SET soLuong = soluong - @soluong 
    WHERE maSP = @maSP and maCH = @maCH
END 
GO

create proc proc_LayHetSanPhamCH
	@maCuaHang INT
as 
begin
	select SPThuocCH.maSP, tenSP, soLuong
	from SPThuocCH, SanPham
	where SPThuocCH.maSP = SanPham.maSP
	and SPThuocCH.maCH = @maCuaHang
end
Go


create function func_LaySoLuongSanPhamTrongCH
	(@maSP INT, @maCH INT)
returns INT
as
begin
	declare @soLuong INT;

	select @soLuong = soLuong
	from SPThuocCH
	where maSP = @maSP
		and maCH = @maCH;

	return @soLuong;
end
go

-----------------------------------------------------
CREATE PROCEDURE proc_NhapSPVaoCH 
    @maSP INT, 
    @maCH INT,
    @maKho INT,
    @soLuong INT 
AS 
BEGIN 
    INSERT INTO SPThuocCH(maSP, maCH, soLuong)
    VALUES(@maSP, @maCH, @soLuong)
END 
GO




--millacenous--
CREATE FUNCTION func_giaHoaDon
    (@maSP INT, @soLuong INT)
RETURNS INT
AS
BEGIN
    DECLARE @total INT

    -- Calculate the total price
    SELECT @total = SanPham.donGia * @soLuong
    FROM SanPham
    WHERE maSP = @maSP;

    RETURN @total;
END
GO


--Loai SP--
CREATE PROCEDURE proc_ThemLoaiSP
    @tenLoaiSP NVARCHAR(100)
AS
BEGIN 
    INSERT INTO LoaiSP(tenLoaiSP) 
    VALUES(@tenLoaiSP)
END
GO

CREATE PROCEDURE proc_XoaLoaiSP
    @maLoaiSP NVARCHAR
AS
BEGIN 
    DELETE FROM LoaiSP 
    WHERE maLoaiSP = @maLoaiSP
END
GO

CREATE PROCEDURE proc_SuaLoaiSanPham
    @maloaiSP INT,
    @tenloaiSP NVARCHAR(100)
AS 
BEGIN 
    UPDATE LoaiSanPham
    SET tenLoaiSP = @tenLoaiSP
    WHERE maLoaiSP = @maLoaiSP
END 
GO

create proc proc_LayHetLoaiSP
as 
begin
	select * from LoaiSP
end
go


--Kho--
DROP PROCEDURE proc_LayHetKho
DROP PROCEDURE proc_NhapSPVaoCH
DROP PROCEDURE proc_LayHetSanPhamTrongKho
DROP PROCEDURE proc_ThemSPVaoKho

--Cửa Hàng--
DROP PROCEDURE proc_LayHetCuaHang
DROP PROCEDURE proc_BanSPTuCH 
DROP PROCEDURE proc_LayHetSanPhamCH

DROP FUNCTION func_giaHoaDon
DROP FUNCTION func_LaySoLuongSanPhamTrongKho
DROP FUNCTION func_LaySoLuongSanPhamTrongCH

--SanPham-- 
DROP PROCEDURE proc_ThemSanPham 
DROP PROCEDURE proc_XoaSanPham
DROP PROCEDURE proc_SuaSanPham
DROP PROCEDURE proc_LayHetSanPham 
DROP PROCEDURE proc_LayHetLoaiSP 
DROP PROCEDURE proc_XoaLoaiSP
DROP PROCEDURE proc_ThemLoaiSP 
DROP PROCEDURE proc_SualLoaiSanPham
drop function func_LayTenLoaiSP
GO

