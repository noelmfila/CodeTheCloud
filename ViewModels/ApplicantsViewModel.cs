using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodeTheCloud.Models;

namespace CodeTheCloud.ViewModels
{
    public class ApplicantsViewModel
    {
        public Applicant Applicant { get; set; }

        public IEnumerable<Race> Races { get; set; }
        public Race Race { get; set; }

        public IEnumerable<Qualification> Qualifications { get; set; }
        public Qualification Qualification { get; set; }

        public IEnumerable<Gender> Genders { get; set; }
        public Gender Gender { get; set; }
    }
}