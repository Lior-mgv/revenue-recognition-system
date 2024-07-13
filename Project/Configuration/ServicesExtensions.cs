using FinancesProject.Context;
using FinancesProject.Services;

namespace FinancesProject.Configuration;

public static class ServicesExtensions
{
    public static void RegisterServices(this IServiceCollection app)
    {
        app.AddDbContext<AppDbContext>();
        app.AddScoped<IClientService, ClientService>();
        app.AddScoped<IContractService, ContractService>();
        app.AddScoped<IAuthService, AuthService>();
        app.AddScoped<IQueryService, QueryService>();
        app.AddSingleton<SecurityHelpers>();
    }
}