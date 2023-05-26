using Quartz;
using Cashflow.Domain.Jobs;
using Cashflow.Domain.Interfaces.Repository;

namespace Cashflow.Infra.Jobs
{
    public class ConsolidationSchaduleJob : IConsolidationSchaduleJob
    {
        private readonly ICashflowRepository _cashflowRepository;

        public ConsolidationSchaduleJob(ICashflowRepository cashflowRepository)
        {
            _cashflowRepository = cashflowRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var entrysWithouConsolidation = await _cashflowRepository.FindAsync(x => !x.IsConsolidated);

            foreach (var entry in entrysWithouConsolidation)
            {
                entry.IsConsolidated = true;
                entry.ConsilidationDate = DateTime.Now;

                await _cashflowRepository.UpdateAsync(entry);
            }
        }
    }
}