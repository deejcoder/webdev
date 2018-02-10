using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Assignment2.Models
{
    public partial class Category
    {
        public int CID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}