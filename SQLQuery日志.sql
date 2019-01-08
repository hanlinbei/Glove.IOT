select * from UserInfo
select * from RoleInfo
select * from R_UserInfo_RoleInfo
select * from R_RoleInfo_ActionInfo
select * from ActionInfo
select * from DeviceInfo
select * from DeviceParameterInfo
select * from OperationLog
select * from WarningInfo

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

		select * from DeviceParameterInfo


		select * from WarningInfo as t1
		left join DeviceInfo as t2 on t1.DeviceId=t2.DeviceId 

		--select * from DeviceInfo as t2
		left join DeviceParameterInfo as t3
		on (t3.DeviceInfoId=t2.Id)
		where t3.StatusFlag='故障中' and t2.IsDeleted=0

select 
t1.DeviceId,
t1.WarningMessage,
t2.报警开始时间,
t1.SubTime,
t2.最新提交时间,
报警时长=t2.最新提交时间-t2.报警开始时间
 from WarningInfo as t1 
inner join	(select
		DeviceId ,
		最新提交时间=MAX(SubTime),
		报警开始时间=Min(SubTime)
		from WarningInfo
		group by DeviceId)as t2 on t1.DeviceId=t2.DeviceId and t1.SubTime=t2.最新提交时间

		select * from DeviceInfo
		
		select 
		--年=year(SubTime),
		--月=month(Subtime),
		--日=day(SubTime),
		Convert(char(10),SubTime,120),
		COUNT (*)
		from DeviceInfo
		--group by year(SubTime),month(Subtime),day(SubTime)
		group by Convert(char(10),SubTime,120)

		select * from DeviceParameterInfo

		select 
		日期=Convert(char(10),SubTime,120),
		今日车间产量=Sum (NowOutput)
		from DeviceParameterInfo
		group by Convert(char(10),SubTime,120)
		order by Convert(char(10),SubTime,120)

		select * from DeviceParameterInfo
		select * from GroupInfo
		select * from DeviceInfo
		select* from TeamInfo

		Create Table DeviceInfo
		where StatusFlag='4'
		delete  from DeviceInfo 
		where IsDeleted=1

		select 
		DeviceInfoId,
		开机时间=min(SubTime)
		from DeviceParameterInfo 
		where StatusFlag='运行中'
		group by DeviceInfoId
		
		select* from DeviceInfo as t1
		inner join DeviceParameterInfo  as t2 on t1.DeviceId=t2.DeviceId



		select* 	
		from DeviceParameterInfo as t1
		inner join  DeviceInfo as t2 on t1.DeviceId=t2.DeviceId
	
	    select  * 
		from DeviceParameterInfo as t1
		use Test
		select * from DeviceParameterInfo as t1
		where t1.DeviceInfoId=1 and t1.StartTime=t1.SubTime
	     