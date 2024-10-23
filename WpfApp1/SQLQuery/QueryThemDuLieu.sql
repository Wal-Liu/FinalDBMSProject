--LoaiSP 
EXEC proc_ThemLoaiSP N'Ban Phim' 
EXEC proc_ThemLoaiSP N'Chuot'
EXEC proc_ThemLoaiSP N'Man Hinh' 
EXEC proc_ThemLoaiSP N'Lot Chuot' 
GO

EXEC proc_ThemSanPham N'Ban Phim Logitech F310', '100', 'sieu xin min', '1', 'cai hinh'
EXEC proc_ThemSanPham N'Ban Phim SteelSeries jj', '101', 'sieu xin', '1', 'cai hinh'
EXEC proc_ThemSanPham N'Man Hinh HyHy', '102', 'sieu xin', '3', 'cai hinh'
EXEC proc_ThemSanPham N'Lot Chuot hinh con chuot', '100', 'sieu xin min', '4', 'cai hinh'
EXEC proc_ThemSanPham N'Chuot hinh con meo', '220', 'sieu xin min', '2', 'cai hinh'
GO

SELECT * FROM SanPham

