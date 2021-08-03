
declare @initdate datetime
declare @enddate datetime
set @initdate = '2021-01-01'
set @enddate = dateadd(MINUTE , -1, dateadd(MONTH, 1, @initdate))

select * from Appointment where StartedAt >= @initdate and StartedAt <= @enddate and userid = 1
Select * from Service


Select *, 100-round(Cost/Gain*100, 0) from (
                                               Select YEAR(StartedAt) as Year,
                                                   MONTH(StartedAt) as Month,
                                                   U.Name,
                                                   sum(Hours) as Hours,
                                                   sum(Hours) * 35 as Gain,
                                                   U.Cost,
                                                   sum(Hours) * 35 - U.Cost as Profit
                                               from Appointment
                                                   inner join [User] U on Appointment.UserId = U.Id
                                                   inner join Service S on Appointment.ServiceId = S.Id
                                               where S.Value != 0
                                                 and U.Cost != -1
                                               group by UserId, U.Name, U.Cost, YEAR(StartedAt), MONTH(StartedAt)
                                               union all
                                               Select YEAR(StartedAt),
                                                   MONTH(StartedAt),
                                                   U.Name,
                                                   sum(Hours),
                                                   sum(Hours) * 35,
                                                   sum(Hours) * 21,
                                                   sum(Hours) * 35 - sum(Hours) * 21 as Profit
                                               from Appointment
                                                   inner join [User] U on Appointment.UserId = U.Id
                                                   inner join Service S on Appointment.ServiceId = S.Id
                                               where S.Value != 0
                                                 and U.Cost = -1
                                               group by UserId, U.Name, U.Cost, YEAR(StartedAt), MONTH(StartedAt)
                                           ) as Result



