CREATE PROCEDURE SP_LOGIN_ACCOUNT
    @UserName VARCHAR(50),
    @Password VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Không tồn tại tài khoản
    IF NOT EXISTS (
        SELECT 1 FROM Account WHERE UserName = @UserName
    )
    BEGIN
        SELECT 
            0 AS Status,
            N'Tài khoản không tồn tại' AS Message;
        RETURN;
    END

    -- 2. Sai mật khẩu
    IF NOT EXISTS (
        SELECT 1 
        FROM Account 
        WHERE UserName = @UserName 
          AND PassWord = @Password
    )
    BEGIN
        SELECT 
            0 AS Status,
            N'Sai mật khẩu' AS Message;
        RETURN;
    END

    -- 3. Đăng nhập thành công
    SELECT 
        1 AS Status,
        N'Đăng nhập thành công' AS Message,
        ID,
        Code
    FROM Account
    WHERE UserName = @UserName
      AND PassWord = @Password;
END

-- EXEC dbo.SP_LOGIN_ACCOUNT @UserName = 'admin', @Password = '123456';