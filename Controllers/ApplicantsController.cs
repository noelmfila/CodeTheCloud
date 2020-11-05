using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CodeTheCloud.Models;
using CodeTheCloud.ViewModels;

namespace CodeTheCloud.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApplicantsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Applicants
        public ActionResult Index()
        {
            return View();
        }

        // GET: Applicants/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
        public ActionResult Create()
        {
            var model = new ApplicantsViewModel
            {
                Races = _context.Races.ToList(),
                Genders = _context.Genders.ToList(),
                Qualifications = _context.Qualifications.ToList()
            };
            return View(model);
        }

        
        [HttpPost]
        public ActionResult Create(ApplicantsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return View();
        }

        // GET: Applicants/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Applicants/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Applicants/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Applicants/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
