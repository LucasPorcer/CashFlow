using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cashflow.Domain.Entities.Cashflow;

namespace Cashflow.Infra.Persistence.Context.SqlServer.Configuration
{
    public class CashflowMap : IEntityTypeConfiguration<CashflowModel>
    {
        public void Configure(EntityTypeBuilder<CashflowModel> builder)
        {
            builder.ToTable("Cashflow");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Value)
                .IsRequired()
                .HasColumnType("decimal(15, 2)");

            builder
                .Property(x => x.IsConsolidated);

            builder
                .Property(x => x.ConsilidationDate)
                .IsRequired(false);
        }
    }
}
