using CodeTheCloud.Models;
using CodeTheCloud.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

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


        public ActionResult ApplicantDetails(int id)
        {
            var applicant = _context.Applicants.Find(id);

            return View("Details", applicant);
        }

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
        public ActionResult Create(ApplicantsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var applicant = new Applicant
            {
                FirstName = model.Applicant.FirstName,
                Surname = model.Applicant.Surname,
                UserId = model.Applicant.UserId,
                Gender = model.Applicant.Gender,
                Race = model.Applicant.Race,
                IdNumber = model.Applicant.IdNumber,
                BirthDate = model.Applicant.BirthDate,
                ContactNumber = model.Applicant.ContactNumber,
                Qualification = model.Applicant.Qualification,
                ResidentialAddress = model.Applicant.ResidentialAddress,
                Acknowledgement = model.Applicant.Acknowledgement,
                RegistrationDate = DateTime.Now.ToShortDateString()
            };

            _context.Applicants.Add(applicant);
            _context.SaveChanges();
           // return View();
            return RedirectToAction("DocumentsInfo", "Documents", new {id = model.Applicant.UserId});
        }

        public ActionResult Edit(int id)
        {
            var profile = _context.Applicants.Find(id);
            
            var model = new ApplicantsViewModel
            {
                Applicant = profile,
                Races = _context.Races.ToList(),
                Qualifications = _context.Qualifications.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ApplicantsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var dbProfile = _context.Applicants.Find(model.Applicant.Id);

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

            return RedirectToAction("ApplicantDetails", new {id = model.Applicant.Id});
        }

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    return View();
        //}

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
