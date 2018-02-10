using System.ComponentModel.DataAnnotations;

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
}
