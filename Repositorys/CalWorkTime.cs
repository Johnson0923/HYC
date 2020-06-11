using HYCWEB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HYCWEB.Repositorys
{
    ////public class CalWorkTime
    //{
    //    public IQueryable<T> getEmployeeListSummary<T>(string EmployeeName, string StartMonth,string EndMonth) where T : new()
    //    {
    //        string sql = @"select 
    //                        (select as_name from as_member m where m.AS_PSN = ws.as_psn) as as_psn
    //                        ,ws.as_workmonth as [month]
    //                        --,s.as_psn
    //                        ,cast(ws.as_totaltime as varchar)+'分' as totalhours
    //                        ,cast(ws.as_basetime as varchar)+'分' as basichours
    //                        ,cast(ws.as_timegap as varchar)+'分' as overhours
    //                        ,RIGHT('0' + cast(ws.as_timegap/1440 as varchar),2) + '天' 
    //                         + RIGHT('0'+cast(cast((ws.as_timegap /60)as int) % 24 as varchar),2) +'時'
    //                         + RIGHT('0'+cast(cast(ws.as_timegap as int)% 60 as varchar),2)+'分鐘' 
    //                         as [換算結果] 
    //                        from (select w.as_psn
    //                        ,substring(cast(as_date as varchar),1,6) as as_workmonth
    //                        ,sum(w.as_hours) as as_totaltime ,22*9*60 as as_basetime,sum(w.as_hours)-(22*9*60) as as_timegap
    //                        from as_worktime w 
    //                        where w.as_psn=@EmployeeName
    //                        group by w.AS_PSN,substring(cast(as_date as varchar),1,6)) ws
    //                        order by  ws.as_psn,ws.as_workmonth";
    //        using (DbContext db = new HYCEntities())
    //        {
    //            var result = db.Database.SqlQuery<T>(sql, new SqlParameter[] { new SqlParameter("@EmployeeName", EmployeeName) });
    //            return result.AsQueryable<T>();
    //        }
    public class CalWorkTime
    {
        public List<T> GetEmployeeListSummary<T>(string EmployeeName)
        {
            using (var context = new HYCEntities())
            {
                string sql = @"select 
                                (select as_name from as_member m where m.AS_PSN = ws.as_psn) as as_psn
                                ,ws.as_workmonth as [as_workmonth]
                                --,s.as_psn
                                ,cast(ws.as_totaltime as varchar)+'分' as as_totaltime
                                ,cast(ws.as_basetime as varchar)+'分' as as_basetime
                                ,cast(ws.as_timegap as varchar)+'分' as as_timegap
                                ,(case when ws.as_timegap<0 then
                                ('本月少'+RIGHT('0'+cast(cast((-ws.as_timegap /60)as decimal)  as varchar),2) +'時'
                                + RIGHT('0'+cast(cast(-ws.as_timegap as decimal)% 60 as varchar),2)+'分鐘' )
                                else ('本月多'+RIGHT('0'+cast(cast((ws.as_timegap /60)as decimal)  as varchar),2) +'時'
                                + RIGHT('0'+cast(cast(ws.as_timegap as decimal)% 60 as varchar),2)+'分鐘' ) end) 
                                 as [conversion] 
                                from (select w.as_psn
                                ,substring(cast(as_date as varchar),1,6) as as_workmonth
                                ,sum(w.as_hours) as as_totaltime ,22*9*60 as as_basetime,cast(sum(w.as_hours)-(22*9*60) as int) as as_timegap
                                from as_worktime w 
                                where w.AS_USERID=@EmployeeName
                                group by w.AS_PSN,substring(cast(as_date as varchar),1,6)) ws
                                order by  ws.as_psn,ws.as_workmonth";


                string sql2 = @"select 
                            (select as_name from as_member m where m.AS_PSN = ws.as_psn) as as_psn
                            ,ws.as_workmonth as [month]
                            --,s.as_psn
                            ,cast(ws.as_totaltime as varchar)+'分' as totalhours
                            ,cast(ws.as_basetime as varchar)+'分' as basichours
                            ,cast(ws.as_timegap as varchar)+'分' as overhours
                            ,RIGHT('0' + cast(ws.as_timegap/1440 as varchar),2) + '天' 
                             + RIGHT('0'+cast(cast((ws.as_timegap /60) as decimal) % 24 as varchar),2) +'時'
                             + RIGHT('0'+cast(cast(ws.as_timegap as decimal)% 60 as varchar),2)+'分鐘' 
                             as [conversion] 
                            from (select w.as_psn
                            ,substring(cast(as_date as varchar),1,6) as as_workmonth
                            ,sum(w.as_hours) as as_totaltime ,22*9*60 as as_basetime,sum(w.as_hours)-(22*9*60) as as_timegap
                            from as_worktime w 
                            where w.AS_USERID=@EmployeeName
                            group by w.AS_PSN,substring(cast(as_date as varchar),1,6)) ws
                            order by  ws.as_psn,ws.as_workmonth";
                return context.Database.SqlQuery<T>(sql, (new SqlParameter[] { new SqlParameter("@EmployeeName", EmployeeName != null ? (object)EmployeeName : DBNull.Value) }).ToArray()).ToList();
            }
        }
    }
}