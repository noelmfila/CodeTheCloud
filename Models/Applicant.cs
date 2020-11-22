using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeTheCloud.Models
{
    public class Applicant
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [RegularExpression(@"^[0-9a-zA-ZÀ-ÿ''-'\s]{1,40}$", ErrorMessage = "special characters are not  allowed.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[0-9a-zA-ZÀ-ÿ''-'\s]{1,40}$", ErrorMessage = "special characters are not  allowed.")]
        public string Surname { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Race { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public string BirthDate { get; set; }

        [Required]
        [Display(Name = "SA ID Number")]
        [MinLength(13, ErrorMessage = "Please enter a valid ID number")]
        [MaxLength(13, ErrorMessage = "Please enter a valid ID number")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "ID number must be numeric")]
        public string IdNumber { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [MinLength(10, ErrorMessage = "Please enter valid contact number")]
        [MaxLength(10, ErrorMessage = "Please enter valid contact number")]
        [RegularExpression("^0[0-9]*$", ErrorMessage = "Contact number must be numeric")]
        public string ContactNumber { get; set; }

        [Required]
        [Display(Name = "Qualification")]
        public string Qualification { get; set; }


        [Display(Name = "Residential Address")]
        [StringLength(70)]
        public string ResidentialAddress { get; set; }

        [Display(Name = "I declare that all the information that I have provided is true to the best of my knowledge")]
        [Required(ErrorMessage = "Please accept the Acknowledgement before continuing")]
        public bool Acknowledgement { get; set; }

        public string RegistrationDate { get; set; }

        public string Availability { get; set; }

        [Display(Name = "Admin Comments")]
        public string AdminComments { get; set; }

        [Display(Name = "Excluded from Opportunities")]
        public bool Excluded { get; set; }

        [StringLength(30)]
        [Display(Name = "Reason")]
        public string ExclusionReason { get; set; }

        [Display(Name = "Until")]
        public string ResetDate { get; set; }

    }
}