using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobApplication.Data;
using JobApplication.Models;
using JobApplication.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JobApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRepository _repository;
        public ValuesController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.JobApplication>>> GetApplicants()
        {
            var applicants = await _repository.GetApplicants();
            return Ok(applicants);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.JobApplication>> GetApplicantById(int id)
        {
            var applicant = await _repository.GetApplicantById(id);
            if (applicant == null)
                return NotFound();
            return Ok(applicant);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            var applicant = await _repository.GetApplicantById(id);
            if (applicant == null)
            {
                return NotFound();
            }

            await _repository.DeleteApplicant(id);
            await _repository.SaveChangesAsync();
            return NoContent();
        }

    }
}
