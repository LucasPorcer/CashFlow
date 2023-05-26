using Cashflow.Domain.ViewModel.Cashflow;
using Bogus;
using Cashflow.Domain.Entities.Cashflow;

namespace Cashflow.Tests.Builders.CashFlow
{
    /// <summary>
    /// Método responsável gerar objetos que serão utilizados em testes
    /// </summary>
    public static class CashFlowBuilder
    {
        /// <summary>
        /// Exemplo de utilização errada da biblioteca Faker
        /// </summary>
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

        /// <summary>
        /// Exemplo de utilização correta da biblioteca Faker
        /// </summary>
        public static List<CashflowModel> GetCashflowModelList(int count = 10)
        {
            var faker = new Faker<CashflowModel>()
                .RuleFor(o => o.Value, f =>f.Random.Decimal(500, 2000))
                .RuleFor(o => o.ConsilidationDate, DateTime.Now)
                .RuleFor(o => o.IsConsolidated, f => f.Random.Bool());

            return faker.Generate(count);
        }

        public static CashflowModel CreateGenerticCashFlowModelWithValue()
        {
            var faker = new Faker<CashflowModel>()
                .RuleFor(o => o.Value, f => f.Random.Decimal(500, 2000));

            return faker.Generate();
        }
    }
}
