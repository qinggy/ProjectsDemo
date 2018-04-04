/****** 对象:  Table [dbo].[Test]    脚本日期: 05/10/2013 11:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
    [ID] [int] IDENTITY(1,1) NOT NULL,
    [UserID] [int] NOT NULL,
    [UserName] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED 
(
    [ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

go

----- 使用该触发器即可，但现实业务可能会比这复杂很多
--1、--处理的触发器示例
create trigger tr_insert on 表
instead of insert  --注意触发器的类型
as
--更新已经存在的主键
update [Test] set UserName=inserted.UserName from [Test]  join inserted  on [Test].UserID=inserted.UserID

--插入存在的主键数据
insert [Test] (UserID,UserName) select inserted.UserID,inserted.UserName from inserted left join [Test] on inserted.UserID=[Test].UserID where [Test].id is null
--或者使用这句更新语句
--insert [Test] (UserID,UserName) select inserted.UserID,inserted.UserName from inserted WHERE (SELECT COUNT(0) FROM test WHERE UserId = INSERTED.UserId) = 0
go
--——————————————————————————————————————————

--像这种每条插入语句都回去判断，执行会很慢
--2、--触发器
CREATE TRIGGER tri_edit  ON tab
INSTEAD OF INSERT
AS
if exists(select col1,col2 from tab join inserted on tab.学号=INSERTED.学号)
 begin
--这里面你可以加如些其他修改操作,取决于具体的功能
    update tab set col1='num1' from tab join inserted on tab.学号=inserted.学号
 end
else
	insert tab  select * from inserted
GO