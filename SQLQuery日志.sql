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
set StatusFlag='运行中'
where DeviceInfoId=1

select * from DeviceParameterInfo
where SubTime between '2018-12-18 19:21:00 'and'2018-12-18 23:21:00'

select
        StatusFlag,
		Max(StartTime)SubTime
		from DeviceParameterInfo
		group by StatusFlag

select top 1 * from DeviceParameterInfo
where StartTime=SubTime 
Order by id Desc

select top 1 * from DeviceParameterInfo
where DeviceInfoId=1
order by SubTime Desc

select DeviceInfoId,
最新开机时间=MAX(SubTime)
from DeviceParameterInfo
where StartTime=SubTime 
group by DeviceInfoId

select *
from DeviceParameterInfo
where StartTime=SubTime 

select  
t2.DeviceInfoId,
t2.最新开机时间,
运行时长=t4.最新正常运行时间-t2.最新开机时间,
t4.最新正常运行时间,
t3.DeviceId,
t1.StatusFlag,
t1.StartTime,
t1.StopTime,
t1.SubTime
 from DeviceParameterInfo as t1 
 inner join DeviceInfo as t3 on t1.DeviceInfoId=t3.Id,
 (select
        DeviceInfoId,
		Max(SubTime)SubTime,
		最新开机时间=MAX(StartTime)
		from DeviceParameterInfo
		group by DeviceInfoId) as t2,
(select
		DeviceInfoId,
		最新正常运行时间=Max(SubTime)
		from DeviceParameterInfo
		where StatusFlag='运行中'
		group by DeviceInfoId) as t4
		where
		t1.SubTime=t2.SubTime and t1.DeviceInfoId=t2.DeviceInfoId and t1.DeviceInfoId=t4.DeviceInfoId