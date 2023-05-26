using Cashflow.Domain.Common.Entities;

namespace Cashflow.Domain.Entities.Cashflow
{
    public class CashflowModel : Entity
    { 
        public decimal Value { get; set; }
        public bool IsConsolidated { get; set; }
        public DateTime? ConsilidationDate { get; set; }
    }
}
