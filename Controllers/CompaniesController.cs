using DapperWebApi.Contracts;
using DapperWebApi.Dto;
using DapperWebApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DapperWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        public CompaniesController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyRepository.GetCompanies();
            return Ok(companies);
        }

        [HttpGet("{id}", Name = "GetCompany")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _companyRepository.GetCompanyById(id);
            if (company is null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody]CompanyDto company)
        {
            var createdCompany = await _companyRepository.CreateCompany(company);
            return CreatedAtRoute("GetCompany", new { id = createdCompany.Id }, createdCompany);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody]CompanyDto company)
        {
            var dbCompany = await _companyRepository.GetCompanyById(id);
            if (dbCompany is null)
            {
                return NotFound();
            }

            await _companyRepository.UpdateCompany(id, company);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var dbCompany = await _companyRepository.GetCompanyById(id);
            if (dbCompany is null)
            {
                return NotFound();
            }

            await _companyRepository.DeleteCompany(id);

            return NoContent();
        }
    }
}
