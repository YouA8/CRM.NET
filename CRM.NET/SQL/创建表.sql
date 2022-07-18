USE master;
GO
IF EXISTS(
    SELECT * FROM SYSDATABASES WHERE NAME = 'CRMDB'
 )
BEGIN
    ALTER DATABASE [CRMDB] SET SINGLE_USER WITH ROLLBACK IMMEDIATE; --设置数据库为单用户模式
    DROP DATABASE CRMDB;
END
GO
CREATE DATABASE CRMDB;
GO
USE CRMDB;
GO
/*创建角色表*/
CREATE TABLE [Role](
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    RoleName VARCHAR(50) NOT NULL,
    Remarke VARCHAR(300) NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL
);
/*创建模块表*/
CREATE TABLE Module(
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ModuleName VARCHAR(50) NOT NULL,
    Name VARCHAR(50) NOT NULL,
    ModuleStyle VARCHAR(20) NOT NULL,
    Url VARCHAR(100) NOT NULL,
    ParentId INT NULL,
    Grade INT NOT NULL,
    Orders INT NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL,
    CONSTRAINT fk_Module_Self FOREIGN KEY(ParentId) REFERENCES [Module](Id)
);
/*创建权限表*/
CREATE TABLE Permission(
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    RoleId INT NOT NULL,
    ModuleId INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL,
    CONSTRAINT fk_Permission_Role FOREIGN KEY(RoleId) REFERENCES [Role](Id),
    CONSTRAINT fk_Permission_Module FOREIGN KEY(ModuleId) REFERENCES [Module](Id)
);
/*创建用户表*/
CREATE TABLE [User](
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    UserName VARCHAR(20) NOT NULL,
    UserPwd VARCHAR(20) NOT NULL,
    Name VARCHAR(50) NULL,
    Email VARCHAR(50) NULL,
    Phone VARCHAR(11) NULL,
    RoleId INT NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL,
    CONSTRAINT fk_User_Role FOREIGN KEY(RoleId) REFERENCES [Role](Id)
);
/*创建客户表*/
CREATE TABLE Customer(
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Name VARCHAR(50) NOT NULL,
    Contact VARCHAR(50) NOT NULL,
    Phone VARCHAR(50) NOT NULL,
    Email VARCHAR(100) NULL,
    Address VARCHAR(100) NULL,
    WebSite VARCHAR(100) NULL,
    Level VARCHAR(50) NULL,
    Xyd VARCHAR(50) NULL,
    CusManager VARCHAR(50) NULL,
    Myd VARCHAR(50) NULL,
    State INT NOT NULL,
    Description VARCHAR(300) NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL
);
/*创建客户联系表*/
CREATE TABLE CusContatct(
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    CusId INT NOT NULL,
    ContactTime DATETIME2 NULL,
    Address VARCHAR(100) NULL,
    Overview VARCHAR(100) NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL
);
/*创建客户流失暂缓表*/
CREATE TABLE CusReprieve(
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    LossId INT NOT NULL,
    Measure VARCHAR(300),
    PlanStartTime DATETIME2 NULL,
    PlanEndTime DATETIME2 NULL,
    ExeAffect VARCHAR(100) NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL
);
/*创建客户流失表*/
CREATE TABLE CusLoss(
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    CusId INT NOT NULL,
    LastOrderTime DATETIME2 NULL,
    ConfirmLossTime DATETIME2 NULL,
    LossReason VARCHAR(300) NULL,
    State INT NOT NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL
);
/*创建营销机会表*/
CREATE TABLE SaleChance(
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ChanceSource VARCHAR(200) NULL,
    CustomerName VARCHAR(50) NOT NULL,
    Contact VARCHAR(50) NOT NULL,
    Phone VARCHAR(11) NOT NULL,
    Probability INT NULL,
    Overview VARCHAR(200) NULL,
    Description VARCHAR(300) NULL,
    CreatorName VARCHAR(50) NOT NULL,
    AssignorName VARCHAR(50) NULL,
    AssignTime DATETIME2 NULL,
    State INT NOT NULL,
    DevResult INT NOT NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL
);
/*创建客户开发计划*/
CREATE TABLE CusDevPlan(
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    SaleChanceId INT NOT NULL,
    PlanCantext VARCHAR(300) NOT NULL,
    PlanStartTime DATETIME2 NULL,
    PlanEndTime DATETIME2 NULL,
    ExeAffect VARCHAR(100) NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL
);
/*创建客户订单表*/
CREATE TABLE CusOrder(
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    CusId INT NOT NULL,
    OrderNo VARCHAR(50) NOT NULL,
    OrderTime DATETIME2 NOT NULL,
    Address VARCHAR(100) NULL,
    State INT NOT NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL
);
/*创建订单详情表*/
CREATE TABLE OrderDetail(
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    OrderId INT NOT NULL,
    GoodsName VARCHAR(50) NOT NULL,
    GoodsNum INT NULL,
    Unit VARCHAR(20) NULL,
    Price FLOAT NULL,
    Sum FLOAT NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL
);
/*创建客户服务表*/
CREATE TABLE CusServer(
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    ServerType VARCHAR(20) NULL,
    Overview VARCHAR(100) NULL,
    Customer VARCHAR(50) NULL,
    Creator VARCHAR(50) NOT NULL,
    Request VARCHAR(100) NULL,
    Assigner VARCHAR(50) NULL,
    AssignTime DATETIME2 NULL,
    ServiceHandle VARCHAR(200) NULL,
    ServiceHandler VARCHAR(50) NULL,
    ServiceHandleResult  VARCHAR(100)  NULL,
    ServiceHandleTime DATETIME2 NULL,
    Myd VARCHAR(20) NULL,
    State INT NOT NULL,
    IsValid INT NOT NULL,
    CreateTime DATETIME2 NOT NULL,
    UpdateTime DATETIME2 NOT NULL
);
GO

/*
SELECT * FROM [User];
SELECT * FROM [Role];
SELECT * FROM Module;
SELECT * FROM Permission;
SELECT * FROM Customer;
SELECT * FROM CusContatct;
SELECT * FROM CusServer;
SELECT * FROM CusOrder;
SELECT * FROM OrderDetail;
SELECT * FROM CusDevPlan;
SELECT * FROM SaleChance;
SELECT * FROM CusReprieve;
SELECT * FROM CusLoss;
GO
*/