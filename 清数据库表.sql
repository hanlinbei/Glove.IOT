select * from ActionInfo
select * from DeviceInfo
delete from DeviceInfo
select * from DeviceParameterInfo
delete from DeviceParameterInfo
select * from R_GroupInfo_DeviceInfo
delete from R_GroupInfo_DeviceInfo
select * from GroupInfo
delete from GroupInfo where GroupInfo.Id>1
select * from OperationLog
delete from OperationLog
select * from R_RoleInfo_ActionInfo
delete from R_RoleInfo_ActionInfo where R_RoleInfo_ActionInfo.RoleInfoId!=1
select * from R_UserInfo_RoleInfo
delete from R_UserInfo_RoleInfo where R_UserInfo_RoleInfo.UserInfoId!=1
select * from RoleInfo
select * from TeamInfo
delete from TeamInfo where TeamInfo.Id>1
select * from UserInfo
delete from UserInfo where UserInfo.Id>1
select * from WarningInfo
delete from WarningInfo

select * from DeviceParameterInfo where DeviceInfoId=1007