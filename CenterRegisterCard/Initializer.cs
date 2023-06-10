using CenterRegisterCard.DAL.Interfaces;
using CenterRegisterCard.DAL.Repositorias;
using CenterRegisterCard.Domain.Models;
using CenterRegisterCard.Service.Implementations;
using CenterRegisterCard.Service.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace CenterRegisterCard
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Employee>, EmployeeRepository>();
            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<SocialCard>, SocialCardRepository>();
            services.AddScoped<IBaseRepository<UserStatus>, UserStatusRepository>();
            services.AddScoped<IBaseRepository<CategoryBeneficiary>,CategoryBeneficiaryRepository>();
            services.AddScoped<IBaseRepository<Event>,EventRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}
