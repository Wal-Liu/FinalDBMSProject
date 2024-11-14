use QLSanPham

create login admin with password = 'admin'
create user admin for login admin

create login nv01 with password = '123' 
create user nv01 for login nv01

create login nv02 with password = '123' 
create user nv02 for login nv02

create login nv03 with password = '123' 
create user nv03 for login nv03

CREATE LOGIN ql01 WITH PASSWORD = '123'
CREATE USER ql01 FOR LOGIN ql01
GO

CREATE LOGIN ql02 WITH PASSWORD = '123'
CREATE USER ql02 FOR LOGIN ql02
GO

CREATE LOGIN ql03 WITH PASSWORD = '123'
CREATE USER ql03 FOR LOGIN ql03
GO


CREATE ROLE NhanVien
GO
-- Cấp quyền thêm (INSERT), xóa (DELETE), và sửa (UPDATE) cho role NhanVien trên bảng SPThuocKho
GRANT INSERT, DELETE, UPDATE ON SPThuocKho TO NhanVien
GO

-- Cấp quyền thêm (INSERT), xóa (DELETE), và sửa (UPDATE) cho role NhanVien trên bảng SPThuocCH
GRANT INSERT, DELETE, UPDATE ON SPThuocCH TO NhanVien
GO

-- Tạo role mới tên là QuanLy
CREATE ROLE QuanLy
GO

-- Cấp quyền thêm (INSERT), xóa (DELETE), và sửa (UPDATE) cho role QuanLy trên bảng SPThuocKho
GRANT INSERT, DELETE, UPDATE ON SPThuocKho TO QuanLy
GO

-- Cấp quyền thêm (INSERT), xóa (DELETE), và sửa (UPDATE) cho role QuanLy trên bảng SPThuocCH
GRANT INSERT, DELETE, UPDATE ON SPThuocCH TO QuanLy
GO

-- Cấp quyền thêm (INSERT), xóa (DELETE), và sửa (UPDATE) cho role QuanLy trên bảng SanPham
GRANT INSERT, DELETE, UPDATE ON SanPham TO QuanLy
GO

-- Cấp quyền thêm (INSERT), xóa (DELETE), và sửa (UPDATE) cho role QuanLy trên bảng CuaHang
GRANT INSERT, DELETE, UPDATE ON CuaHang TO QuanLy
GO

-- Cấp quyền thêm (INSERT), xóa (DELETE), và sửa (UPDATE) cho role QuanLy trên bảng Kho
GRANT INSERT, DELETE, UPDATE ON Kho TO QuanLy
GO

-- Cấp quyền thêm (INSERT), xóa (DELETE), và sửa (UPDATE) cho role QuanLy trên bảng LoaiSP
GRANT INSERT, DELETE, UPDATE ON LoaiSP TO QuanLy
GO

EXEC sp_addsrvrolemember 'admin', 'sysadmin'


-- Thêm các user ql01, ql02, và ql03 vào role QuanLy
EXEC sp_addrolemember 'QuanLy', 'ql01'
EXEC sp_addrolemember 'QuanLy', 'ql02'
EXEC sp_addrolemember 'QuanLy', 'ql03'
GO

-- Thêm các user nv01, nv02, và nv03 vào role NhanVien
EXEC sp_addrolemember 'NhanVien', 'nv01'
EXEC sp_addrolemember 'NhanVien', 'nv02'
EXEC sp_addrolemember 'NhanVien', 'nv03'
GO

DROP USER ql01
DROP USER ql02
DROP USER ql03
DROP USER nv01
DROP USER nv02
DROP USER nv03
DROP USER admin

DROP LOGIN ql01
DROP LOGIN ql02
DROP LOGIN ql03
DROP LOGIN nv01
DROP LOGIN nv02
DROP LOGIN nv03
DROP LOGIN admin

DROP ROLE NhanVien
DROP ROLE QuanLy
