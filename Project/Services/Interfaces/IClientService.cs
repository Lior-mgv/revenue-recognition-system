using FinancesProject.DTO;
using FinancesProject.Models;

namespace FinancesProject.Services;

public interface IClientService
{
    Task<IndividualClient> AddIndividualClientAsync(IndividualClientForm clientForm);
    Task<Company> AddCompanyClientAsync(CompanyForm clientForm);
    Task<IndividualClient> DeleteIndividualClientAsync(int id);
    Task<IndividualClient> UpdateIndividualClientAsync(int id, IndividualClientPatch patch);
    Task<Company> UpdateCompanyClientAsync(int id, CompanyPatch patch);
}