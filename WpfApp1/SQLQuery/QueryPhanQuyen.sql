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
GRANT EXECUTE TO NhanVien
DENY EXECUTE ON proc_LayHetSanPham to NhanVien
DENY EXECUTE ON proc_ThemSanPham to NhanVien
DENY EXECUTE ON proc_XoaSanPham to NhanVien
DENY EXECUTE ON proc_SuaSanPham to NhanVien
DENY EXECUTE ON proc_LaySanPham to NhanVien
DENY EXECUTE ON func_LayTenLoaiSP to NhanVien
DENY EXECUTE ON proc_ThemLoaiSP to NhanVien
DENY EXECUTE ON proc_SuaLoaiSanPham to NhanVien
DENY EXECUTE ON proc_LayHetLoaiSP to NhanVien

DENY EXECUTE ON proc_LayHetTaiKhoan to NhanVien
DENY EXECUTE ON proc_TaoTaiKhoan to NhanVien


-- Tạo role mới tên là QuanLy
CREATE ROLE QuanLy
GO
GRANT INSERT, DELETE, UPDATE, SELECT, EXECUTE TO QuanLy
DENY EXECUTE ON proc_LayHetTaiKhoan to QuanLy
DENY EXECUTE ON proc_TaoTaiKhoan to QuanLy
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

select 'drop login [' + name + '];'
from sys.server_principals
GO

select 'drop user [' + name + '];'
from sys.sysusers
GO

select * from sys.sysusers 


DROP ROLE NhanVien
DROP ROLE QuanLy
