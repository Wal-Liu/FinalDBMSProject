USE QLSanPham
GO

--SanPham--
CREATE PROCEDURE proc_ThemSanPham
    @tenSP NVARCHAR(100), 
	@donGia INT,
	@moTa NVARCHAR(1000),
	@maLoaiSP INT,
	@duongDanHinhAnh VARCHAR(1000)
AS 
BEGIN 
    INSERT INTO SanPham (tenSP, donGia, moTa, maLoaiSP, duongDanHinhAnh)
    VALUES (@tenSP, @donGia, @moTa, @maLoaiSP, @duongDanHinhAnh)
END 
GO

CREATE PROCEDURE proc_XoaSanPham
    @tenSP NVARCHAR(100)
AS 
BEGIN 
    DELETE FROM SanPham 
    WHERE tenSP = @tenSP 
END 
GO


CREATE PROCEDURE proc_SuaSanPham
    @maSP INT,
    @tenSP NVARCHAR(100), 
	@donGia INT,
	@moTa NVARCHAR(1000),
	@maLoaiSP INT,
	@duongDanHinhAnh VARCHAR(1000)
AS 
BEGIN 
    UPDATE SanPham
    SET tenSP = @tenSP,
        donGia = @donGia,
        moTa = @moTa,
        maLoaiSP = @maLoaiSP, 
        duongDanHinhAnh = @duongDanHinhAnh 
    WHERE maSP = @maSP
END 
GO

create proc proc_LayHetSanPham
as 
begin
	select * from SanPham
end
go

-----------------------------------------------------------------------
--Kho--
create proc proc_LayHetKho
as 
begin
	select * from Kho
end
go 
-----------------------------------------------------
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

create proc proc_LaySoLuongSanPhamTrongKho
	@maSP INT,
	@maKho INT
as 
begin
	select SPThuocKho.maSP, tenSP, soLuong
	from SPThuocKho, SanPham
	where SPThuocKho.maSP = SanPham.maSP
		and SPThuocKho.maSP = @maSP
		and SPThuocKho.maKho = @maKho
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
    UPDATE SPThuocCH
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

create proc proc_LaySoLuongSanPhamTrongCH
	@maCH int
as 
begin
	select SPThuocCH.maSP, tenSP, soLuong
	from SPThuocCH, SanPham
	where SPThuocCH.maSP = SanPham.maSP
	and SPThuocCH.maCH = @maCH
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
create proc proc_giaHoaDon
    @maSP int,
    @soLuong int
as
begin
    select donGia * @soluong from SanPham 
    where maSP =@maSP
end
go




--Loai SP--
CREATE PROCEDURE proc_ThemLoaiSP
    @tenLoaiSP NVARCHAR
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

--Kho--
DROP PROCEDURE proc_LayHetKho
DROP PROCEDURE proc_NhapSPVaoCH
DROP PROCEDURE proc_LayHetSanPhamTrongKho
DROP PROCEDURE proc_LaySoLuongSanPhamTrongKho
DROP PROCEDURE proc_ThemSPVaoKho

--Cửa Hàng--
DROP PROCEDURE proc_LayHetCuaHang
DROP PROCEDURE proc_BanSPTuCH
DROP PROCEDURE proc_LayHetSanPhamCH
DROP PROCEDURE proc_LaySoLuongSanPhamTrongCH


DROP PROCEDURE proc_giaHoaDon

--SanPham-- 
DROP PROCEDURE proc_ThemSanPham 
DROP PROCEDURE proc_XoaSanPham
DROP PROCEDURE proc_SuaSanPham
DROP PROCEDURE proc_LayHetSanPham 
DROP PROCEDURE proc_XoaLoaiSP
DROP PROCEDURE proc_ThemLoaiSP 

GO

