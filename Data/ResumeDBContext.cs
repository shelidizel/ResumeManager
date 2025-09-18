using Microsoft.EntityFrameworkCore;
using ResumeManager.Models;
using System.Collections.Generic;

namespace ResumeManager.Data
{
    public class ResumeDbContext : DbContext
    {
        public ResumeDbContext(DbContextOptions<ResumeDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<Experience> Experiences { get; set; }
        public virtual DbSet<Software> Softwares { get; set; }
        public virtual DbSet<SoftwareExperience> SoftwareExperiences { get; set; }
    }
}