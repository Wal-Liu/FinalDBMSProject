USE QuanLySanPham
GO

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

CREATE PROCEDURE proc_XoaSanPham
    @tenSP NVARCHAR(100)
AS 
BEGIN 
    DELETE FROM SanPham 
    WHERE tenSP = @tenSP 
END 
GO


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

CREATE PROCEDURE proc_NhapSPVaoCH 
    @maSP INT, 
    @maCH INT, 
    @soLuong INT 
AS 
BEGIN 
    INSERT INTO SPThuocCH(maSP, maCH, soLuong)
    VALUES(@maSP, @maCH, @soLuong)
END 
GO

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

CREATE PROCEDURE proc_ThemLoaiSP
    @tenLoaiSP NVARCHAR
AS
BEGIN 
    INSERT INTO LoaiSP(tenLoaiSP) 
    VALUES(@tenLoaiSP)
END
GO
create proc proc_LayHetSanPhamCH
as 
begin
	select * from SPThuocCH
end
Go

CREATE PROCEDURE proc_XoaLoaiSP
    @maLoaiSP NVARCHAR
AS
BEGIN 
    DELETE FROM LoaiSP 
    WHERE maLoaiSP = @maLoaiSP
END
GO

create proc proc_LayHetSanPham
as 
begin
	select * from SanPham
end

create proc proc_giaHoaDon
    @maSP int,
    @soLuong int
as
begin
    select donGia * @soluong from SanPham 
    where maSP =@maSP
end
go
create proc proc_LaySanPhamTrongCH
    @maSP INT
as 
begin
    select * from SPThuocCuaHang
end
go
create proc proc_LayHetSanPhamTrongKho
as 
begin
	select SPThuocKho.maSP, tenSP, soLuong
	from SPThuocKho, SanPham
	where SPThuocKho.maSP = SanPham.maSP
end
go 

create proc proc_LaySoLuongSanPhamTrongKho
	@maSP INT
as 
begin
	select SPThuocKho.maSP, tenSP, soLuong
	from SPThuocKho, SanPham
	where SPThuocKho.maSP = SanPham.maSP
		and SPThuocKho.maSP = @maSP
end
go 

create proc proc_LayHetCuaHang
as 
begin
	select * from CuaHang
end
go 





DROP PROCEDURE proc_ThemSPVaoKho

DROP PROCEDURE proc_BanSPTuCH 
DROP PROCEDURE proc_NhapSPVaoCH
DROP PROCEDURE proc_LayHetCuaHang


DROP PROCEDURE proc_ThemSanPham
DROP PROCEDURE proc_XoaSanPham
DROP PROCEDURE proc_SuaSanPham
DROP PROCEDURE proc_LayHetSanPham
DROP PROCEDURE proc_LayHetSanPhamTrongKho
DROP PROCEDURE proc_LaySoLuongSanPhamTrongKho


DROP PROCEDURE proc_ThemLoaiSP
DROP PROCEDURE proc_XoaLoaiSP



