using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeTheCloud.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Applicant))]
        public int ApplicantId { get; set; }

        public Applicant Applicant { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string FileName { get; set; }

        [DataType(DataType.Date)]
        public DateTime UploadDate { get; set; }

        public string FilePath { get; set; }

        public string AzureFileName { get; set; }

        public string FileExtension { get; set; }

        public string UserId { get; set; }
    }
}