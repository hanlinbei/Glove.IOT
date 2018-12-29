select * from UserInfo
select * from RoleInfo
select * from R_UserInfo_RoleInfo
select * from R_RoleInfo_ActionInfo
select * from ActionInfo
select * from DeviceInfo
select * from DeviceParameterInfo
select * from OperationLog

Use Test

select * from DeviceParameterInfo
order by(SubTime)
update DeviceParameterInfo
set StatusFlag='ÔËÐÐÖÐ'
where DeviceInfoId=1

select * from DeviceParameterInfo
where SubTime between '2018-12-18 19:21:00 'and'2018-12-18 23:21:00'

select
        StatusFlag,
		Max(StartTime)SubTime
		from DeviceParameterInfo
		group by StatusFlag

select * from DeviceParameterInfo
where StartTime=SubTime or StopTime=SubTime