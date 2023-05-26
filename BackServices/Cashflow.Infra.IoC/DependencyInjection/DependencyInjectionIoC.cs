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
        /// <summary>
        /// Método principal responsável por gerir as injecoes de dependencias
        /// </summary>
        /// <returns></returns>
        public static void AddDependencyInjectionIoC(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CashflowSqlServerContext>(options =>
            {
                options.UseInMemoryDatabase("Bank");
            });

            AddValidations(services);

            AddCommands(services);

            AddMappings(services);

            AddJobServices(services);

            AddJobs(services, configuration);

            AddRepositories(services);

        }

        /// <summary>
        /// Método responsável por adicionar o Validator para determinada classe
        /// </summary>
        private static void AddValidations(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(CreateEntryCommand).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// Método responsável por adicionar os comandos que o MediatR ira gestionar, assim fazendo bom uso do CQRS
        /// </summary>
        private static void AddCommands(IServiceCollection services)
        {
            services.AddMediatR(options => {
                options.RegisterServicesFromAssembly(typeof(CreateEntryCommand).GetTypeInfo().Assembly);
            });
        }

        /// <summary>
        /// Método responsável por adicionar os mapeamentos e profiles utilizados na API
        /// </summary>
        private static void AddMappings(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EntryMappingProfile).GetTypeInfo().Assembly);
        }


        /// <summary>
        /// Método responsável por adicionar os repositorios utilizados na API
        /// </summary>
        private static void AddRepositories(this IServiceCollection services)
        {
            /// <summary>
            /// Foi optado por Scoped pois 
            /// O método AddScoped garante que uma instância do serviço seja criada uma vez por solicitação. 
            /// Isso é especialmente útil quando se trata de acesso ao banco de dados, pois você geralmente deseja que a mesma instância do contexto de banco de dados seja usada ao longo de uma única solicitação HTTP.
            /// </summary>
            services.AddScoped<ICashflowRepository, CashflowRepository>();
        }

        /// <summary>
        /// Método responsavel por adicionar os serviços que serão utilizados por Jobs internos
        /// </summary>
        private static void AddJobServices(this IServiceCollection services)
        {   
            services.AddSingleton<IConsolidationSchaduleJob, ConsolidationSchaduleJob>();
        }

        /// <summary>
        /// Método responsável por adicionar e configurar o JOB de consolidacao
        /// </summary>
        private static void AddJobs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddQuartz(quartz =>
            {
                quartz.UseMicrosoftDependencyInjectionScopedJobFactory();

                var jobKey = new JobKey("ConsolidationJob");

                // Adiciona oque o Job deve fazer
                quartz.AddJob<IConsolidationSchaduleJob>(opts => opts.WithIdentity(jobKey));

                quartz.AddTrigger(opts => opts
                   .ForJob(jobKey).WithIdentity("TriggerKey").WithCronSchedule("0 0/2 * * * ?"));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
