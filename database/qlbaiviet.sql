use qlbaiviet2
go

--CREATE TABLE--
create table [Groups] (
	GroupId nvarchar(20) primary key,
	GroupName nvarchar(50) 
)

create table [Roles] (
	RoleId nvarchar(20) primary key,
	Decription nvarchar(255)
)	

create table [Credentials] (
	GroupId nvarchar(20) foreign key references Groups(GroupId),
	RoleId nvarchar(20) foreign key references Roles(RoleId),
	primary key (GroupId, RoleId)
)

create table [Users] (
	UserId int IDENTITY(1,1) primary key,
	Email nvarchar(255) , 
	Username nvarchar(50),
	[Password] nvarchar(50),
	GroupId nvarchar(20) foreign key references Groups(GroupId)
)

create table [Posts] (
	PostId int IDENTITY(1,1) primary key,
	Title nvarchar(255),
	Pcontent nvarchar(255),
	PostDate date,
	[Image] nvarchar(255),
	UserId int not null foreign key references Users(UserId)
)

create table [Comments] (
	CommentId int IDENTITY(1,1) primary key,
	Ccontent nvarchar(255),
	UserId int not null foreign key references Users(UserId),
	PostId int not null foreign key references Posts(PostId)
)

--ADD DATA--
insert into Groups(GroupId, GroupName)
values 
('ADMIN', 'admin'),
('MOD', 'moderator'),
('CLIENT','client')

insert into Roles(RoleId, Decription)
values
('CREATE_USER', 'thêm user'),
('DELETE_USER', 'xoá user'),
('EDIT_USER', 'sửa user'),
('VIEW_USER', 'xem user'),
('VIEW_POST', 'xem post'),
('CREATE_POST', 'thêm post'),
('EDIT_POST', 'sửa post'),
('DELETE_POST', 'xoá post'),
('CREATE_COMMENT', 'thêm comment'),
('DELETE_COMMENT', 'xoá comment'),
('EDIT_COMMENT', 'sửa comment'),
('VIEW_COMMENT', 'xem comment'),
('CREATE_ROLE', 'thêm role'),
('DELETE_ROLE', 'xoá role'),
('EDIT_ROLE', 'sửa role'),
('VIEW_ROLE', 'xem role'),
('CREATE_GROUP', 'thêm group'),
('DELETE_GROUP', 'xoá group'),
('EDIT_GROUP', 'sửa group'),
('VIEW_GROUP', 'xem group')

insert into Users(Email, Username, [Password], GroupId)
values
('admin@admin.com','admin','202cb962ac59075b964b07152d234b70','ADMIN'),
('mod@mod.com','mod','202cb962ac59075b964b07152d234b70','MOD'),
('client@client.com','client','202cb962ac59075b964b07152d234b70','CLIENT')

insert into [Credentials](GroupId, RoleId)
values
('MOD', 'VIEW_POST'),
('MOD', 'CREATE_POST'),
('MOD', 'EDIT_POST'),
('MOD', 'DELETE_POST'),
('MOD', 'VIEW_COMMENT'),
('MOD', 'CREATE_COMMENT'),
('MOD', 'EDIT_COMMENT'),
('MOD', 'DELETE_COMMENT')

--DROP TABLE--
drop table [Comments]
drop table [Posts]
drop table [Users]
drop table [Roles]
drop table [Groups]
drop table [Credentials]