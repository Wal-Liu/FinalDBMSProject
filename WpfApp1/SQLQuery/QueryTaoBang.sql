CREATE DATABASE QLSanPham
GO

USE QLSanPham 
GO

CREATE TABLE LoaiSP
(
	maLoaiSP INT NOT NULL IDENTITY(1, 1),
	tenLoaiSP NVARCHAR(100) NOT NULL, 
	PRIMARY KEY(maLoaiSP)
)
GO

CREATE TABLE SanPham
(
	maSP INT NOT NULL IDENTITY(1, 1), 
	tenSP NVARCHAR(100) UNIQUE NOT NULL, 
	donGia INT NOT NULL check (donGia > 0),
	moTa NVARCHAR(1000),
	maLoaiSP INT NOT NULL,
	duongDanHinhAnh VARCHAR(1000), 
	PRIMARY KEY(maSP),
	FOREIGN KEY (maLoaiSP) REFERENCES LoaiSP(maLoaiSP)
)
GO

CREATE TABLE Kho
(
	maKho INT NOT NULL IDENTITY(1, 1),
	tenKho NVARCHAR(100), 
	diaChi NVARCHAR(1000), 
	SDT NVARCHAR(10), 
	PRIMARY KEY(maKho), 
)
GO

CREATE TABLE SPThuocKho 
(
	maSP INT NOT NULL, 
	maKho INT NOT NULL, 
	soLuong INT NOT NULL CHECK (soLuong >= 0), 
	FOREIGN KEY (maSP) REFERENCES SanPham(maSP), 
	FOREIGN KEY (maKho) REFERENCES Kho(maKho), 
	PRIMARY KEY (maKho, maSP)
)

CREATE TABLE CuaHang
(	 	
	maCH INT NOT NULL IDENTITY(1, 1),
	tenCH NVARCHAR(100), 
	diaChi NVARCHAR(1000), 
	SDT NVARCHAR(10), 
	PRIMARY KEY(maCH), 
)
GO 

CREATE TABLE SPThuocCH
(
	maSP INT NOT NULL, 
	maCH INT NOT NULL, 
	soLuong INT NOT NULL CHECK (soLuong >= 0), 
	FOREIGN KEY (maSP) REFERENCES SanPham(maSP), 
	FOREIGN KEY (maCH) REFERENCES CuaHang(maCH), 
	PRIMARY KEY (maCH, maSP)
)
GO


CREATE TABLE TaiKhoan 
( 
	tenTaiKhoan VARCHAR(100) NOT NULL UNIQUE, 
	matKhau VARCHAR(1000) NOT NULL,
	viTri NVARCHAR(1000) NOT NULL CHECK (viTri in ('quanly', 'nhanvien')),
	PRIMARY KEY(tenTaiKhoan),
)
GO

DROP TABLE TaiKhoan
DROP TABLE SPThuocKho 
DROP TABLE SPThuocCH 
DROP TABLE SanPham 
DROP TABLE Kho 
DROP TABLE CuaHang
DROP TABLE LoaiSP

