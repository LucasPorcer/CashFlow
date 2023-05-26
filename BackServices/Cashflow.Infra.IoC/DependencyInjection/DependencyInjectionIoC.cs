using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Cashflow.Application.Entry.Command;
using Cashflow.Application.Entry.Mapper;
using Cashflow.Domain.Jobs;
using Cashflow.Infra.Jobs;
using Cashflow.Infra.Persistence.Context.SqlServer.Context;
using Cashflow.Infra.Repositories.Transaction;
using System.Reflection;
using Cashflow.Domain.Interfaces.Repository;

namespace Cashflow.Infra.IoC.DependencyInjection
{
    public static class DependencyInjectionIoC
    {
        public static void AddDependencyInjectionIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CashflowSqlServerContext>(options =>
            {
                options.UseInMemoryDatabase("Bank");
            });

            AddValidations(services);

            AddCommands(services);

            AddMappings(services);

            AddJobs(services, configuration);

            AddRepositories(services);

        }

        private static void AddValidations(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateEntryCommand).GetTypeInfo().Assembly);
        }

        private static void AddCommands(IServiceCollection services)
        {
            services.AddMediatR(options => {
                options.RegisterServicesFromAssembly(typeof(CreateEntryCommand).GetTypeInfo().Assembly);
            });
        }

        private static void AddMappings(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EntryMappingProfile).GetTypeInfo().Assembly);
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICashflowRepository, CashflowRepository>();
            services.AddScoped<IConsolidationSchaduleJob, ConsolidationSchaduleJob>();
        }

        private static void AddJobs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(quartz =>
            {
                quartz.UseMicrosoftDependencyInjectionScopedJobFactory();

                var jobKey = new JobKey("ConsolidationJob");

                quartz.AddJob<IConsolidationSchaduleJob>(opts => opts.WithIdentity(jobKey));

                quartz.AddTrigger(opts => opts
                   .ForJob(jobKey).WithIdentity("TriggerKey").WithCronSchedule("0 0/2 * * * ?"));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
