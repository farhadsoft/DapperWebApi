using DapperWebApi.Dto;
using DapperWebApi.Entities;

namespace DapperWebApi.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompanyById(int id);
        public Task<Company> CreateCompany(CompanyDto company);
        public Task UpdateCompany(int id, CompanyDto company);
        public Task DeleteCompany(int id);
    }
}
