USE QLSanPham
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

CREATE PROCEDURE proc_XoaLoaiSP
    @maLoaiSP NVARCHAR
AS
BEGIN 
    DELETE FROM LoaiSP 
    WHERE maLoaiSP = @maLoaiSP
END
GO

DROP PROCEDURE proc_ThemSPVaoKho

DROP PROCEDURE proc_BanSPTuCH 
DROP PROCEDURE proc_NhapSPVaoCH

DROP PROCEDURE proc_ThemSanPham
DROP PROCEDURE proc_XoaSanPham
DROP PROCEDURE proc_SuaSanPham

DROP PROCEDURE proc_ThemLoaiSP
DROP PROCEDURE proc_XoaLoaiSP


