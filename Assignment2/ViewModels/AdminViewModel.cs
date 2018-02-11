using Assignment2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Assignment2.ViewModels
{
    public class AdminViewModel
    {
        public class RoleViewModel
        {
            public string ID { get; set; }
            [Required(AllowEmptyStrings = false)]
            [Display(Name = "Role Name")]
            public string Name { get; set; }

        }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }
        public Address Address { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }

}
