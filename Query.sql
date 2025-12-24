-- Bảng api
CREATE TABLE ApiUrl (
	ID INT IDENTITY(1,1) PRIMARY KEY,
    Code AS ('AU' + RIGHT('000000' + CAST(ID AS VARCHAR(6)), 6)) PERSISTED,
	ApiName NVARCHAR(200) NOT NULL,
	Value VARCHAR(200) NOT NULL UNIQUE,
	CreateDate DATETIME DEFAULT GETDATE(),
    EditDate DATETIME DEFAULT GETDATE()
);
-- Bảng Tài khoản
CREATE TABLE Account (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Code AS ('AC' + RIGHT('000000' + CAST(ID AS VARCHAR(6)), 6)) PERSISTED,
    UserName VARCHAR(50) NOT NULL UNIQUE,
    PassWord VARCHAR(100) NOT NULL,
    Role VARCHAR(20) DEFAULT 'Admin',
    IsActive BIT DEFAULT 1
);


-- Bảng quyền hạn người dùng
CREATE TABLE Permission (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Code AS ('PM' + RIGHT('000000' + CAST(ID AS VARCHAR(6)), 6)) PERSISTED,
    PermissionName VARCHAR(100) NOT NULL,
	Value VARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE AccountPermission (
    AccountID INT NOT NULL,
    PermissionID INT NOT NULL,
    Allow BIT DEFAULT 1,

    CONSTRAINT PK_AccountPermission PRIMARY KEY (AccountID, PermissionID),
    CONSTRAINT FK_AP_Account FOREIGN KEY (AccountID) REFERENCES Account(ID),
    CONSTRAINT FK_AP_Permission FOREIGN KEY (PermissionID) REFERENCES Permission(ID)
);


-- Bảng thông tin người dùng
CREATE TABLE UserInfor (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Code AS ('UI' + RIGHT('000000' + CAST(ID AS VARCHAR(6)), 6)) PERSISTED,
    AccountID INT NOT NULL,
    FullName NVARCHAR(100),
    Email VARCHAR(100),
    Phone VARCHAR(20),
    CreateDate DATETIME DEFAULT GETDATE(),
    EditDate DATETIME DEFAULT GETDATE(),
	EndDate DATETIME DEFAULT GETDATE()

    CONSTRAINT FK_UserInfor_Account
        FOREIGN KEY (AccountID) REFERENCES Account(ID)
);

CREATE PROCEDURE sp_Login
    @UserName VARCHAR(50),
    @Password VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ID, Code, UserName, Role
    FROM Account
    WHERE UserName = @UserName
      AND PassWord = @Password
END