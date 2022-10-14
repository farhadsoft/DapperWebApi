using Dapper;
using DapperWebApi.Context;
using DapperWebApi.Contracts;
using DapperWebApi.Dto;
using DapperWebApi.Entities;
using System.Data;

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
            string query = "SELECT * FROM Companies WHERE Id = @Id";

            using var connection = _context.CreateConnection();
            var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { id });

            return company;
        }

        public async Task<Company> CreateCompany(CompanyDto company)
        {
            string query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)" +
                "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new DynamicParameters();
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using var connection = _context.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(query, parameters);

            var createdCompany = new Company
            {
                Id = id,
                Name = company.Name,
                Address = company.Address,
                Country = company.Country
            };

            return createdCompany;
        }

        public async Task UpdateCompany(int id, CompanyDto company)
        {
            string query = "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Id", id, DbType.Int32);
            parameters.Add("Name", company.Name, DbType.String);
            parameters.Add("Address", company.Address, DbType.String);
            parameters.Add("Country", company.Country, DbType.String);

            using var connections = _context.CreateConnection();
            await connections.ExecuteAsync(query, parameters);
        }

        public async Task DeleteCompany(int id)
        {
            string query = "DELETE FROM Companies WHERE Id = @Id";

            using var connections = _context.CreateConnection();
            await connections.ExecuteAsync(query, new { id });
        }
    }
}
