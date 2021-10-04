using AutoMapper;
using d4.Sample.Domain.Employees;
using Microsoft.Extensions.DependencyInjection;

namespace d4.Sample.Infrastructure.JsonFiles
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSampleInfrastructureJsonFiles(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => 
                cfg.AddProfile<EmployeeStateProfile>());
            
            config.AssertConfigurationIsValid();

            services.AddSingleton(config.CreateMapper());
            services.AddSingleton(new Config() {BasePath = "/tmp/employees"});
            
            services.AddScoped<IEmployeeRepository,EmployeeRepository>();
            return services;
        }
    }
}