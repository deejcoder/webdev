using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    [MetadataType(typeof(CategoryMetaData))]
    public partial class Category
    {
        //I prefer there to be some encapsulated private class
        class CategoryMetaData
        {
            [Key]
            public int CID { get; set; }

            [Required(ErrorMessage = "The category name cannot be left blank!")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a category name between {2} and {1} characters in length.")]
            [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Please only use letters in your category name. It should also start with an upper-case letter.")]
            [Display(Name = "Category Name")]
            public string Name { get; set; }
        }
    }
}
