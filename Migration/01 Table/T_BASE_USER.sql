create table  T_BASE_USER
(
       Id                bigint identity(1, 1) not null,
       BUSurname         nvarchar(20) default '' not null,
       BUGivenname       nvarchar(20) default '' not null,
       BUJobNumber       nvarchar(20) default '' not null,
       BUSex             tinyint default 0 not null,
       BUAvatars         nvarchar(200) default '' not null,
       BUPhoneNum        nvarchar(50) default '' not null,
       BUEmail           nvarchar(50) default '' not null,
       BUDepartId        bigint default 0 not null,
       BUTitle           nvarchar(100) default '' not null,
       BUCreateUserId    bigint default 0 not null,
       BUCreateUserName  nvarchar(50) default '' not null,
       BUCreateTime      datetime default getdate() not null,
       BUOperateUserId   bigint default 0 not null,
       BUOperateUserName nvarchar(50) default '' not null,
       BUOperateTime     datetime default getdate() not null,
       BUIsValid         tinyint default 1 not null
);
alter  table T_BASE_USER
       add constraint PK_T_BASE_USER_Id primary key (Id);
EXEC sp_addextendedproperty 'MS_Description', '��Ա��Ϣ�� User Info', 'user', dbo, 'table', T_BASE_USER, NULL, NULL;
EXEC sp_addextendedproperty 'MS_Description', '��Ա��Id', 'user', dbo, 'table', T_BASE_USER, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', 'Surname ��', 'user', dbo, 'table', T_BASE_USER, 'column', BUSurname;
EXEC sp_addextendedproperty 'MS_Description', 'Givenname ��', 'user', dbo, 'table', T_BASE_USER, 'column', BUGivenname;
EXEC sp_addextendedproperty 'MS_Description', 'JobNumber ����', 'user', dbo, 'table', T_BASE_USER, 'column', BUJobNumber;
EXEC sp_addextendedproperty 'MS_Description', 'Sex �Ա�(1-�У�2-Ů��0-����)', 'user', dbo, 'table', T_BASE_USER, 'column', BUSex;
EXEC sp_addextendedproperty 'MS_Description', 'Avatars ͷ��', 'user', dbo, 'table', T_BASE_USER, 'column', BUAvatars;
EXEC sp_addextendedproperty 'MS_Description', 'PhoneNum �绰', 'user', dbo, 'table', T_BASE_USER, 'column', BUPhoneNum;
EXEC sp_addextendedproperty 'MS_Description', 'Email ����', 'user', dbo, 'table', T_BASE_USER, 'column', BUEmail;
EXEC sp_addextendedproperty 'MS_Description', 'DepartId ��������Id', 'user', dbo, 'table', T_BASE_USER, 'column', BUDepartId;
EXEC sp_addextendedproperty 'MS_Description', 'UserTitle ��λͷ��', 'user', dbo, 'table', T_BASE_USER, 'column', BUTitle;
EXEC sp_addextendedproperty 'MS_Description', 'CreateUserId ������Id', 'user', dbo, 'table', T_BASE_USER, 'column', BUCreateUserId;
EXEC sp_addextendedproperty 'MS_Description', 'CreateUserName ����������', 'user', dbo, 'table', T_BASE_USER, 'column', BUCreateUserName;
EXEC sp_addextendedproperty 'MS_Description', 'CreateTime ��������', 'user', dbo, 'table', T_BASE_USER, 'column', BUCreateTime;
EXEC sp_addextendedproperty 'MS_Description', 'OperateUserId ������Id', 'user', dbo, 'table', T_BASE_USER, 'column', BUOperateUserId;
EXEC sp_addextendedproperty 'MS_Description', 'OperateUserName ����������', 'user', dbo, 'table', T_BASE_USER, 'column', BUOperateUserName;
EXEC sp_addextendedproperty 'MS_Description', 'OperateTime ����ʱ��', 'user', dbo, 'table', T_BASE_USER, 'column', BUOperateTime;
EXEC sp_addextendedproperty 'MS_Description', 'IsValid ��Ч�ԣ�0.��Ч 1.��Ч��', 'user', dbo, 'table', T_BASE_USER, 'column', BUIsValid;
