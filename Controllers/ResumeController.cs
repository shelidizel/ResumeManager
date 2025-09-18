using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResumeManager.Data;
using ResumeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeManager.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ResumeDbContext _context;
        //private readonly IWebHostEnvironment _webHost;

        public ResumeController(ResumeDbContext context)
        {
            _context = context;
            //_webHost = webHost;
        }

        public IActionResult Index()
        {
            List<Applicant> applicants = _context.Applicants.ToList();
            return View(applicants);
        }

        [HttpGet]
        public IActionResult Create()
        {
            Applicant applicant = new Applicant();
            applicant.Experiences.Add(new Experience() { ExperienceId = 1 });
            ViewBag.Gender = GetGender();
            applicant.SoftwareExperiences.Add(new SoftwareExperience() { Id = 1 });
            ViewBag.Softwares = GetSoftwares();
            ViewBag.Rating = GetRating();
            return View(applicant);
        }

        [HttpPost]
        public IActionResult Create(Applicant applicant)
        {
            applicant.Experiences.RemoveAll(n => n.YearsWorked == 0);
            applicant.Experiences.RemoveAll(n => n.IsDeleted == true);
            string uniqueFileName = "/photourl";
            applicant.PhotoUrl = uniqueFileName;

            _context.Add(applicant);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            Applicant applicant = _context.Applicants
                .Include(e => e.Experiences)
                .Include(f => f.SoftwareExperiences)
                .Where(a => a.Id == Id).FirstOrDefault();
            ViewBag.Gender = GetGender();
            ViewBag.Softwares = GetSoftwares();
            ViewBag.Rating = GetRating();
            return View(applicant);
        }

        [HttpPost]
        public IActionResult Edit(Applicant applicant)
        {
            List<Experience> expDetails = _context.Experiences.Where(d => d.ApplicantId == applicant.Id).ToList();
            _context.Experiences.RemoveRange(expDetails);
            _context.SaveChanges();

            List<SoftwareExperience> softDetails = _context.SoftwareExperiences.Where(d => d.ApplicantId == applicant.Id).ToList();
            _context.SoftwareExperiences.RemoveRange(softDetails);
            _context.SaveChanges();



            applicant.Experiences.RemoveAll(n => n.YearsWorked == 0);
            applicant.Experiences.RemoveAll(n => n.IsDeleted == true);
            //if (applicant.ProfilePhoto != null)
            //{
            //    string uniqueFileName = GetUploadedFileName(applicant);
            //    applicant.PhotoUrl = uniqueFileName;
            //}
            _context.Attach(applicant);
            _context.Entry(applicant).State = EntityState.Modified;
            _context.Experiences.AddRange(applicant.Experiences);
            _context.SoftwareExperiences.AddRange(applicant.SoftwareExperiences);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int Id)
        {
            Applicant applicant = _context.Applicants
                .Include(e => e.Experiences)
                .Include(f => f.SoftwareExperiences)
                .Where(a => a.Id == Id).FirstOrDefault();
            ViewBag.Softwares = GetSoftwares();
            ViewBag.Rating = GetRating();
            return View(applicant);
        }

        public IActionResult Delete(int Id)
        {
            Applicant applicant = _context.Applicants
                .Include(e => e.Experiences)
                .Include(f => f.SoftwareExperiences)
                .Where(a => a.Id == Id).FirstOrDefault();
            ViewBag.Softwares = GetSoftwares();
            ViewBag.Rating = GetRating();
            return View(applicant);
        }

        [HttpPost]
        public IActionResult Delete(Applicant applicant)
        {
            _context.Attach(applicant);
            _context.Entry(applicant).State = EntityState.Deleted;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        //private string GetUploadedFileName(Applicant applicant)
        //{
        //    string uniqueFileName = null;
        //    if (applicant.ProfilePhoto != null)
        //    {
        //        string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            applicant.ProfilePhoto.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}

        private List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> GetGender()
        {
            List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> selGender = new List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>();
            var selItem = new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = "",
                Text = "Select Gender"
            };
            selGender.Insert(0, selItem);
            selItem = new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = "Male",
                Text = "Male"
            };
            selGender.Add(selItem);
            selItem = new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem()
            {
                Value = "Female",
                Text = "Female"
            };
            selGender.Add(selItem);
            return selGender;
        }

        private List<SelectListItem> GetSoftwares()
        {
            List<SelectListItem> selSoftwares = _context.Softwares
                .OrderBy(n => n.Name)
                .Select(n =>
                    new SelectListItem
                    {
                        Value = n.Id.ToString(),
                        Text = n.Name
                    }).ToList();

            var selItem = new SelectListItem()
            {
                Value = null,
                Text = "Select Software"
            };

            selSoftwares.Insert(0, selItem);

            return selSoftwares;
        }

        private List<SelectListItem> GetRating()
        {
            List<SelectListItem> selRating = new List<SelectListItem>();

            var selItem = new SelectListItem() { Value = "0", Text = "Select Rating" };

            selRating.Insert(0, selItem);

            for (int i = 1; i < 11; i++)
            {
                selItem = new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                };
                selRating.Add(selItem);
            }
            return selRating;
        }
    }
}