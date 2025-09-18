using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace ResumeManager.Models
{
    public class SoftwareExperience
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Applicant")]
        //very important
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; private set; } //very important

        [ForeignKey("Softwares")]
        //very important
        public int SoftwareId { get; set; }
        public virtual Software Softwares { get; private set; } //very important

        [Range(1, 10, ErrorMessage = "Select Your Rating out of 10, 10 is the best, 1 is Poor")]
        public int Rating { get; set; }

        [AllowNull]
        [StringLength(50)]
        public string Notes { get; set; }

        [NotMapped]
        public bool IsHidden { get; set; } = false;
    }
}