using Application.Services;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Infrastructure.Scheduling
{
    public class StatusUpdater : IJob
    {
        private readonly ISolutionRep solutionRep;

        public StatusUpdater(ISolutionRep solutionRep)
        {
            this.solutionRep = solutionRep;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            const int Week = 7;

            var today = DateTime.Now.Date;
            var deadlineCutoff = today.AddDays(-Week);

            var sols = solutionRep.GetAll().Where(it =>
                it.Status == SolutionStatuses.UnderReview.ToString() &&
                it.TaskItem.Deadline <= deadlineCutoff);

            await sols.ForEachAsync((sol) =>
            {
                sol.Status = SolutionStatuses.Ignored.ToString();
            });

            await solutionRep.SaveChangesAsync();
        }
    }
}
