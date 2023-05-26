using Cashflow.Domain.ViewModel.Cashflow;
using Bogus;

namespace Cashflow.Tests.Builders.CashFlow
{
    public static class CashFlowBuilder
    {
        public static List<DailyBalanceDto> CreateDailyBalances(int days = 2)
        {
            var faker = new Faker();

            var response = new List<DailyBalanceDto>();

            for (int i = 0; i <= days; i++)
            {
                var dailyBalance = new DailyBalanceDto
                {
                    Date = DateTime.Now.AddDays(i + 1).Date,
                    Balance = faker.Random.Decimal(500, 2000)
                };

                response.Add(dailyBalance);
            }

            return response;
        }
    }
}
