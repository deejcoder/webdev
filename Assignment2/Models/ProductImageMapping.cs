
using System.ComponentModel.DataAnnotations;

namespace Assignment2.Models
{
    public class ProductImageMapping
    {
        [Key]
        public int ID { get; set; }
        public int ImageNumber { get; set; }

        //ProductID
        public int PID { get; set; }

        //ProductImageID
        public int PImageID { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductImage ProductImage { get; set; }
    }
}
