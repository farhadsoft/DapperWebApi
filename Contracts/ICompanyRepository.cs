using DapperWebApi.Entities;

namespace DapperWebApi.Contracts
{
    public interface ICompanyRepository
    {
        public Task<IEnumerable<Company>> GetCompanies();
        public Task<Company> GetCompanyById(int id);
    }
}
