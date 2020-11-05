using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using CodeTheCloud.Models;

namespace CodeTheCloud.ViewModels
{
    public class DocumentViewModel
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Applicant))]
        public int ApplicantId { get; set; }

        public Applicant Applicant { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string FileName { get; set; }

        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }

        public string FilePath { get; set; }

        public string AzureFileName { get; set; }

        public string FileExtension { get; set; }

        [Display(Name = "File")]
        public HttpPostedFileBase FormFile { get; set; }

        public string UserId { get; set; }
    }
}