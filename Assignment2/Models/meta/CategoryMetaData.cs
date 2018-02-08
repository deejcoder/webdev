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
            public int CID;
            [Display(Name = "Category Name")]
            public string Name;
        }
    }
}
