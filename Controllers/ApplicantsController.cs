using CodeTheCloud.Models;
using CodeTheCloud.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CodeTheCloud.Controllers
{
    [Authorize]
    public class ApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantsController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult ApplicantDetails()
        {
            var userId = User.Identity.GetUserId();

            var applicant = _context.Applicants
                            .FirstOrDefault(a => a.UserId == userId);

            return View("Details", applicant);
        }

        [HttpGet]
        public ActionResult Create(string id)
        {
            var applicant = new Applicant
            {
                UserId = id
            };

            var model = new ApplicantsViewModel
            {
                Applicant = applicant,
                Races = _context.Races.ToList(),
                Qualifications = _context.Qualifications.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Applicant applicant)
        {
            if (!ModelState.IsValid)
            {
                var model = new ApplicantsViewModel
                {
                    Applicant = applicant,
                    Races = _context.Races.ToList(),
                    Qualifications = _context.Qualifications.ToList()
                };
                return View(model);
            }
            
            _context.Applicants.Add(applicant);
            _context.SaveChanges();
            return RedirectToAction("DocumentsInfo", "Documents");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var profile = _context.Applicants.Find(id);

            if (profile == null)
                return HttpNotFound();

            var model = new ApplicantsViewModel
            {
                Applicant = profile,
                Races = _context.Races.ToList(),
                Qualifications = _context.Qualifications.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicantsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Races = _context.Races.ToList();
                model.Qualifications = _context.Qualifications.ToList();
                
                return View(model);
            }

            var dbProfile = _context.Applicants.Find(model.Applicant.Id);

            if (dbProfile == null)
                return HttpNotFound();

            dbProfile.FirstName = model.Applicant.FirstName;
            dbProfile.Surname = model.Applicant.Surname;
            dbProfile.Gender = model.Applicant.Gender;
            dbProfile.Race = model.Applicant.Race;
            dbProfile.IdNumber = model.Applicant.IdNumber;
            dbProfile.BirthDate = model.Applicant.BirthDate;
            dbProfile.ContactNumber = model.Applicant.ContactNumber;
            dbProfile.Qualification = model.Applicant.Qualification;
            dbProfile.ResidentialAddress = model.Applicant.ResidentialAddress;
            dbProfile.Acknowledgement = model.Applicant.Acknowledgement;

            _context.Entry(dbProfile).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("ApplicantDetails");
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
