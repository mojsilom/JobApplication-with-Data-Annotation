using Microsoft.EntityFrameworkCore;
using JobApplication.Data;
using JobApplication.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace JobApplication.Services
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateApplicant(Models.JobApplication applicant)
        {
            _context.Applicants.Add(applicant);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteApplicant(int id)
        {
            var applicantToDelete = await _context.Applicants.FindAsync(id);
            if (applicantToDelete != null)
            {
                var cvToDelete = await _context.FileModel.Where(r => r.FileModelId == applicantToDelete.FileUploadFileModelId).FirstOrDefaultAsync();
                _context.FileModel.Remove(cvToDelete);
                _context.Applicants.Remove(applicantToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Models.JobApplication>> GetApplicants()
        {
            var applicants = await _context.Applicants.ToListAsync();
            foreach (var applicant in applicants)
            {
                var cvToShow = await _context.FileModel.Where(r => r.FileModelId == applicant.FileUploadFileModelId).FirstOrDefaultAsync();
            }
            return applicants;
        }

        public async Task<Models.JobApplication?> GetApplicantById(int applicantID)
        {
            var applicant = await _context.Applicants.Where(u=>u.ApplicationID==applicantID).FirstOrDefaultAsync();
            if (applicant != null)
            {
                var cvToShow = await _context.FileModel.Where(r => r.FileModelId == applicant.FileUploadFileModelId).FirstOrDefaultAsync();
                return await _context.Applicants.Where(u => u.ApplicationID == applicantID).FirstOrDefaultAsync();
            }
            throw new Exception("Applicant not found!");
        }
        public async Task<bool> SaveChangesAsync()
        {
            return ((await _context.SaveChangesAsync() >= 0));
        }
    }
}
