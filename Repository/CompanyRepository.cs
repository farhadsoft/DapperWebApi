using Dapper;
using DapperWebApi.Context;
using DapperWebApi.Contracts;
using DapperWebApi.Entities;

namespace DapperWebApi.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly DapperContext _context;
        public CompanyRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompanies()
        {
            string query = "SELECT * FROM Companies";

            using var connection = _context.CreateConnection();
            var companies = await connection.QueryAsync<Company>(query);

            return companies.ToList();
        }

        public async Task<Company> GetCompanyById(int id)
        {
            string query = "SELECT * FROM Companies WHERE Id=@Id";

            using var connection = _context.CreateConnection();
            var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });

            return company;
        }
    }
}
