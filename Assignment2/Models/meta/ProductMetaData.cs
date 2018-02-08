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
            public int PID { get; set; }

            [Display(Name = "Product Name")]
            [Required(ErrorMessage = "The product Name cannot be left blank!")]
            [RegularExpression(@"^[a-zA-Z0-9'-'\s]*$", ErrorMessage = "Please enter a product name which only consists of letters and numbers.")]
            [StringLength(50, ErrorMessage = "The {0} should have at least {2} characters and no longer than {1} characters.", MinimumLength = 3)]
            public string Name { get; set; }

            [Display(Name = "Product Description")]
            [StringLength(200, ErrorMessage = "The {0} should have at least {2} characters and no longer than {1} characters.", MinimumLength = 10)]
            [Required(ErrorMessage = "The product Description cannot be left blank!")]
            [RegularExpression(@"^[A-Za-z0-9'-'\s]*$", ErrorMessage = "Please enter a product description which consists of only letters and numbers")]
            [DataType(DataType.MultilineText)] //Make it a text area instead or multiline text box
            public string Desc { get; set; }

            [Required(ErrorMessage = "The product Price cannot be left blank!")]
            [Range(0.10, 10000, ErrorMessage = "Please enter a price between {1} and {2}")]
            [DataType(DataType.Currency)]
            [DisplayFormat(DataFormatString = "{0:c}")]
            public int Price { get; set; }
        }
    }
}