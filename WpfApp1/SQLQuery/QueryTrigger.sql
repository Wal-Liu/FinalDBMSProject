USE QLSanPham
GO

CREATE TRIGGER trg_SuaSPThuocKho
ON SPThuocKho
INSTEAD OF INSERT
AS
BEGIN
    -- Bắt đầu transaction
    BEGIN TRANSACTION

    -- Khai báo biến
    DECLARE @maSP INT, @maKho INT, @soLuong INT

    -- Lấy giá trị từ bảng INSERTED
    SELECT @maSP = maSP, @maKho = maKho, @soLuong = soLuong FROM INSERTED

    -- Kiểm tra sản phẩm đã tồn tại trong kho hay chưa
    IF EXISTS (SELECT 1 FROM SPThuocKho WHERE maSP = @maSP AND maKho = @maKho)
    BEGIN
        -- Cập nhật số lượng nếu sản phẩm đã tồn tại
        UPDATE SPThuocKho
        SET soLuong = soLuong + @soLuong
        WHERE maSP = @maSP AND maKho = @maKho

        PRINT 'Cập nhật số lượng sản phẩm thành công.'
    END
    ELSE
    BEGIN
        -- Thêm sản phẩm mới nếu chưa tồn tại
        INSERT INTO SPThuocKho (maSP, maKho, soLuong)
        VALUES (@maSP, @maKho, @soLuong)

        PRINT 'Thêm sản phẩm vào kho thành công.'
    END

    -- Kết thúc transaction
    COMMIT TRANSACTION
END
GO

CREATE TRIGGER trg_NhapSPVaoCH
ON SPThuocCH
INSTEAD OF INSERT
AS
BEGIN
    -- Bắt đầu transaction
    BEGIN TRANSACTION

    -- Khai báo biến
    DECLARE @maSP INT, @maCH INT, @soLuong INT

    -- Lấy giá trị từ bảng INSERTED
    SELECT @maSP = maSP, @maCH = maCH, @soLuong = soLuong FROM INSERTED

    -- Kiểm tra sản phẩm đã tồn tại trong cửa hàng hay chưa
    IF EXISTS (SELECT 1 FROM SPThuocCH WHERE maSP = @maSP AND maCH = @maCH)
    BEGIN
        -- Cập nhật số lượng nếu sản phẩm đã tồn tại
        UPDATE SPThuocCH
        SET soLuong = soLuong + @soLuong
        WHERE maSP = @maSP AND maCH = @maCH
        PRINT 'Cập nhật số lượng sản phẩm thành công.'
    END
    ELSE
    BEGIN
        -- Thêm sản phẩm mới nếu chưa tồn tại
        INSERT INTO SPThuocCH (maSP, maCH, soLuong)
        VALUES (@maSP, @maCH, @soLuong)

        PRINT 'Thêm sản phẩm vào kho thành công.'
    END
    IF (@soLuong = (SELECT soLuong from SPThuocKho WHERE maSP = @maSP))
    BEGIN 
        DELETE FROM SPThuocKho 
        WHERE maSP = @maSP
    END 
    ELSE 
    BEGIN 
		UPDATE SPThuocKho 
		SET soLuong = soLuong - @soLuong
		WHERE maSP = @maSP
    END


    -- Kết thúc transaction
    COMMIT TRANSACTION
END
GO

DROP TRIGGER trg_NhapSPVaoCH



