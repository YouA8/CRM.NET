USE CRMDB;
GO
INSERT INTO [Role](RoleName,Remarke,IsValid,CreateTime,UpdateTime) 
VALUES
('superadmin','超级管理员',1,'2019-7-15','2019-7-15'),
('admin','管理员',1,'2019-7-15','2019-7-15'),
('user','用户',1,'2019-7-15','2019-7-15'),
('sale','销售',1,'2019-7-15','2019-7-15'),
('null','未分配',1,'2019-7-15','2019-7-15');
GO
INSERT INTO Module(ModuleName,Name,ModuleStyle,Url,ParentId,Grade,Orders,IsValid,CreateTime,UpdateTime)
VALUES
('Home','营销管理','菜单','/home',null,1,null,1,'2019-7-15','2019-7-15'),
('SaleChance','营销机会管理','菜单','/salechance',1,2,null,1,'2019-7-15','2019-7-15'),
('CusDevPlan','客户开发计划','菜单','/cusdevplan',1,2,null,1,'2019-7-15','2019-7-15'),
('Home','客户管理','菜单','/home',null,1,null,1,'2019-7-15','2019-7-15'),
('Customer','客户信息管理','菜单','/customer',4,2,null,1,'2019-7-15','2019-7-15'),
('CusLoss','客户流失管理','菜单','/cusloss',4,2,null,1,'2019-7-15','2019-7-15'),
('Home','服务管理','菜单','/home',null,1,null,1,'2019-7-15','2019-7-15'),
('CusServer','客户服务','菜单','/cusserver',7,2,null,1,'2019-7-15','2019-7-15'),
('Home','统计报表','菜单','/home',null,1,null,1,'2019-7-15','2019-7-15'),
('CusConReport','客户贡献分析','菜单','/cusconreport',9,2,null,1,'2019-7-15','2019-7-15'),
('CusMakeReport','客户构成分析','菜单','/cusmakereport',9,2,null,1,'2019-7-15','2019-7-15'),
('CusServerReport','客户服务分析','菜单','/cusserverreport',9,2,null,1,'2019-7-15','2019-7-15'),
('CusLossReport','客户流失分析','菜单','/cuslossreport',9,2,null,1,'2019-7-15','2019-7-15'),
('Home','系统设置','菜单','/home',null,1,null,1,'2019-7-15','2019-7-15'),
('Role','角色管理','菜单','/role',14,2,null,1,'2019-7-15','2019-7-15'),
('User','用户管理','菜单','/user',14,2,null,1,'2019-7-15','2019-7-15'),
('Menu','菜单管理','菜单','/menu',14,2,null,1,'2019-7-15','2019-7-15');
GO
INSERT INTO Permission(RoleId,ModuleId,CreateTime,UpdateTime)
VALUES
(1,1,'2019-7-15','2019-7-15'),
(1,2,'2019-7-15','2019-7-15'),
(1,3,'2019-7-15','2019-7-15'),
(1,4,'2019-7-15','2019-7-15'),
(1,5,'2019-7-15','2019-7-15'),
(1,6,'2019-7-15','2019-7-15'),
(1,7,'2019-7-15','2019-7-15'),
(1,8,'2019-7-15','2019-7-15'),
(1,9,'2019-7-15','2019-7-15'),
(1,10,'2019-7-15','2019-7-15'),
(1,11,'2019-7-15','2019-7-15'),
(1,12,'2019-7-15','2019-7-15'),
(1,13,'2019-7-15','2019-7-15'),
(1,14,'2019-7-15','2019-7-15'),
(1,15,'2019-7-15','2019-7-15'),
(1,16,'2019-7-15','2019-7-15'),
(1,17,'2019-7-15','2019-7-15'),
(3,1,'2019-7-15','2019-7-15'),
(3,2,'2019-7-15','2019-7-15'),
(3,3,'2019-7-15','2019-7-15'),
(3,4,'2019-7-15','2019-7-15'),
(3,5,'2019-7-15','2019-7-15'),
(3,6,'2019-7-15','2019-7-15'),
(3,7,'2019-7-15','2019-7-15'),
(3,8,'2019-7-15','2019-7-15'),
(3,9,'2019-7-15','2019-7-15'),
(3,10,'2019-7-15','2019-7-15'),
(3,11,'2019-7-15','2019-7-15'),
(3,12,'2019-7-15','2019-7-15'),
(3,13,'2019-7-15','2019-7-15'),
(4,1,'2019-7-15','2019-7-15'),
(4,2,'2019-7-15','2019-7-15'),
(4,3,'2019-7-15','2019-7-15'),
(4,4,'2019-7-15','2019-7-15'),
(4,5,'2019-7-15','2019-7-15'),
(4,6,'2019-7-15','2019-7-15'),
(4,7,'2019-7-15','2019-7-15'),
(4,8,'2019-7-15','2019-7-15');
GO
INSERT INTO [User](UserName,UserPwd,Name,Email,Phone,RoleId,IsValid,CreateTime,UpdateTime)
VALUES
('Jack','Jack123','Jack','Jack@123.com','12345678910',1,1,'2019-7-15','2019-7-15'),
('Luce','Luce123','Luce','Luce@123.com','12345678910',3,1,'2019-7-15','2019-7-15'),
('Joe','Joe123','Joe','Joe@123.com','12345678910',4,1,'2019-7-15','2019-7-15'),
('Smith','Smith123','Smith','Smith@123.com','12345678910',5,1,'2019-7-15','2019-7-15');
GO
INSERT INTO Customer(Name,Contact,Phone,Email,Address,WebSite,Level,Xyd,CusManager,Myd,State,Description,IsValid,CreateTime,UpdateTime) 
VALUES
('百度','xxx','123415678910','','','','VIP1','高',null,'非常满意',1,'',1,'2019-7-15','2019-7-15'),
('淘宝','yyy','123415678910','','','',null,'高',null,'满意',1,'',1,'2019-7-15','2019-7-15'),
('xx科技','zzz','123415678910','','','','VIP3','高',null,'非常满意',1,'',1,'2019-7-15','2019-7-15'),
('kk店','ccc','123415678910','','','','VIP3','高',null,'满意',1,'',1,'2019-7-15','2019-7-15'),
('ll店','ddd','123415678910','','','','VIP1','高',null,'非常满意',1,'',1,'2019-7-15','2019-7-15'),
('yy店','sss','123415678910','','','','VIP2','高',null,'满意',1,'',1,'2019-7-15','2019-7-15'),
('zz超市','qqq','123415678910','','','',null,'高',null,'不满意',1,'',1,'2019-7-15','2019-7-15');
GO
INSERT INTO CusContatct(CusId,ContactTime,Address,Overview,IsValid,CreateTime,UpdateTime)
VALUES
(1,'2019-8-10','xx路xx店','商量项目计划',1,'2019-8-10','2019-8-10'),
(1,'2019-10-5','xx路xx店','项目开始正常联系',1,'2019-10-5','2019-10-5'),
(1,'2020-5-7','xx路xx店','项目联系',1,'2020-5-7','2020-5-7'),
(2,'2019-8-10','zz路zz店','正常联系',1,'2019-8-10','2019-8-10'),
(3,'2019-8-10','kk店','正常联系',1,'2019-8-10','2019-8-10'),
(3,'2020-1-10','kk店','正常联系',1,'2020-1-10','2020-1-10'),
(4,'2019-12-4','yy店','正常联系',1,'2019-12-4','2019-12-4');
GO
INSERT INTO CusReprieve(LossId,Measure,PlanStartTime,PlanEndTime,ExeAffect,IsValid,CreateTime,UpdateTime)
VALUES
(6,'重新商谈','2020-6-12','2020-6-30','完成计划，准备重新项目计划。',1,'2020-6-30','2020-6-30');
GO
INSERT INTO CusLoss(CusId,LastOrderTime,ConfirmLossTime,LossReason,State,IsValid,CreateTime,UpdateTime)
VALUES
(7,'2020-5-15','2020-5-16','合作不愉快',1,1,'2020-5-16','2020-5-16');
GO
INSERT INTO SaleChance(ChanceSource,CustomerName,Contact,Phone,Probability,Overview,Description,CreatorName,AssignorName,AssignTime,State,DevResult,IsValid,CreateTime,UpdateTime)
VALUES
('百度','百度','xxx','12345678910',100,'','','Jack',null,null,1,1,1,'2019-7-15','2019-7-15'),
('淘宝','淘宝','yyy','12345678910',100,'','','Jack',null,null,1,1,1,'2019-7-15','2019-7-15'),
('xx科技','百度','zzz','12345678910',100,'','','Jack',null,null,1,1,1,'2019-7-15','2019-7-15');
GO
INSERT INTO CusDevPlan(SaleChanceId,PlanCantext,PlanStartTime,PlanEndTime,ExeAffect,IsValid,CreateTime,UpdateTime)
VALUES
(1,'项目开始准备','2020-1-12','2020-1-30','成功',1,'2020-1-12','2020-1-30'),
(1,'项目运行正常商谈','2020-2-10','2020-2-20','成功',1,'2020-2-10','2020-2-20'),
(1,'项目结束','2020-3-15','2020-3-20','成功',1,'2020-3-15','2020-3-20'),
(2,'项目开始准备','2020-5-10','2020-5-15','成功',1,'2020-5-10','2020-5-15'),
(2,'项目运行正常商谈','2020-6-10','2020-6-12','成功',1,'2020-6-10','2020-6-12'),
(3,'正常商谈','2020-4-5','2020-4-8','成功',1,'2020-4-5','2020-4-8');
GO
INSERT INTO CusOrder(CusId,OrderNo,OrderTime,Address,State,IsValid,CreateTime,UpdateTime)
VALUES
(1,'yntm','2020-1-12','',1,1,'2020-1-12','2020-1-12'),
(1,'ytnm','2020-1-12','',1,1,'2020-1-12','2020-1-12'),
(1,'mete','2020-1-12','',1,1,'2020-1-12','2020-1-12'),
(2,'ntoe','2020-6-12','',1,1,'2020-6-12','2020-6-12'),
(2,'sdfd','2020-6-12','',1,1,'2020-6-12','2020-6-12'),
(3,'mekc','2020-3-10','',1,1,'2020-3-10','2020-3-10');
GO
INSERT INTO OrderDetail(OrderId,GoodsName,GoodsNum,Unit,Price,Sum,IsValid,CreateTime,UpdateTime)
VALUES
(1,'广告',1,'则',100000,100000,1,'2020-1-12','2020-1-12'),
(2,'广告',1,'则',100000,100000,1,'2020-1-12','2020-1-12'),
(3,'广告',1,'则',100000,100000,1,'2020-1-12','2020-1-12'),
(4,'充电器',500,'副',12,6000,1,'2020-6-12','2020-6-12'),
(5,'耳机',500,'副',150,75000,1,'2020-6-12','2020-6-12'),
(6,'耳机',500,'副',150,75000,1,'2020-3-10','2020-3-10');
GO
INSERT INTO CusServer(ServerType,Overview,Customer,Creator,Request,Assigner,AssignTime,ServiceHandle,ServiceHandler,ServiceHandleResult,ServiceHandleTime,Myd,State,IsValid,CreateTime,UpdateTime)
VALUES
('咨询','','百度','Jack',null,null,null,null,null,null,null,'非常满意',1,1,'2020-3-10','2020-3-10'),
('反馈','','百度','Jack',null,null,null,null,null,null,null,'非常满意',1,1,'2020-6-10','2020-6-10'),
('咨询','','淘宝','Jack',null,null,null,null,null,null,null,'满意',1,1,'2020-4-10','2020-4-10'),
('反馈','','淘宝','Jack',null,null,null,null,null,null,null,'满意',1,1,'2020-3-10','2020-3-10'),
('投诉','','xx科技','Jack',null,null,null,null,null,null,null,'非常满意',1,1,'2020-8-10','2020-8-10'),
('投诉','','百度','Jack',null,null,null,null,null,null,null,'满意',1,1,'2020-5-10','2020-5-10'),
('反馈','','xx科技','Jack',null,null,null,null,null,null,null,'满意',1,1,'2020-2-10','2020-2-10');
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