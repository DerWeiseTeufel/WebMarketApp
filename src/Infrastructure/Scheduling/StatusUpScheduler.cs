using Quartz;
using Quartz.Impl;

namespace Infrastructure.Scheduling
{
    public class StatusUpScheduler
    {
        public static async void Start()
        {
            const int IntervalHours = 72;

            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail job = JobBuilder.Create<StatusUpdater>().Build();

            ITrigger trigger = TriggerBuilder.Create() 
                .WithIdentity("trigger1", "group1")    
                .StartNow()                            
                .WithSimpleSchedule(it => it           
                    .WithIntervalInHours(IntervalHours)
                    .RepeatForever())                  
                .Build();                              

            await scheduler.ScheduleJob(job, trigger); 
        }
    }
}
