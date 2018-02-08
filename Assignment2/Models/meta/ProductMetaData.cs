using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
        //I prefer there to be some encapsulated private class
        class ProductMetaData
        {
            [Key]
            public int PID;

            [Display(Name = "Product Name")]
            [Required]
            [StringLength(40, ErrorMessage = "The {0} should have at least {2} characters and no longer than {1} characters.", MinimumLength = 4)]
            public string Name;

            [Display(Name = "Product Description")]
            [StringLength(200, ErrorMessage = "The {0} should have at least {2} characters and no longer than {1} characters.", MinimumLength = 0)]
            [DataType(DataType.MultilineText)] //Make it a text area instead or multiline text box
            public string Desc;
        }
    }
}