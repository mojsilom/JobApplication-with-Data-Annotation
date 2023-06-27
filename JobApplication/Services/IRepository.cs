using Microsoft.AspNetCore.Mvc;
using JobApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Services
{
    public interface IRepository
    {
        Task<IEnumerable<Models.JobApplication>> GetApplicants();

        Task<Models.JobApplication?> GetApplicantById(int applicantID);
        Task CreateApplicant(Models.JobApplication applicant);

        Task DeleteApplicant(int id);
        Task<bool> SaveChangesAsync();
       
    }
}
