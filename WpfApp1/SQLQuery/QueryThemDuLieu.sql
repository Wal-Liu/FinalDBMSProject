--LoaiSP 
EXEC proc_ThemLoaiSP N'Ban Phim' 
EXEC proc_ThemLoaiSP N'Chuot'
EXEC proc_ThemLoaiSP N'Man Hinh' 
EXEC proc_ThemLoaiSP N'Lot Chuot' 
GO

--SanPham--
EXEC proc_ThemSanPham N'Ban Phim Logitech F310', '100', 'sieu xin min', '1'
EXEC proc_ThemSanPham N'Ban Phim SteelSeries jj', '101', 'sieu xin', '1'
EXEC proc_ThemSanPham N'Man Hinh HyHy', '102', 'sieu xin', '3'
EXEC proc_ThemSanPham N'Lot Chuot hinh con chuot', '100', 'sieu xin min', '4'
EXEC proc_ThemSanPham N'Chuot hinh con meo', '220', 'sieu xin min', '2'
GO


--Kho-- 
INSERT INTO Kho (tenKho, diaChi, SDT) 
VALUES ('Kho Kho', 'Phan Van Tri', '08086871')

--Cua Hang--
INSERT INTO CuaHang (tenCH, diaChi, SDT) 
VALUES ('Cua Hang Cua', 'Pham Van Dong', '123') 

INSERT INTO CuaHang (tenCH, diaChi, SDT) 
VALUES ('Cua Hang Hang', 'No Trang Long', '155') 

INSERT INTO CuaHang (tenCH, diaChi, SDT) 
VALUES ('Cua Hang Cua Hang', 'Nguyen Xi', '12334') 

SELECT * FROM CuaHang
SELECT * FROM KHO
SELECT * FROM SanPham

