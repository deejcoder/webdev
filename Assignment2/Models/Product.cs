using System.ComponentModel.DataAnnotations;
using Assignment2.Models;

namespace Assignment2.Models
{
    public partial class Product
    {
        public int PID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public int Price { get; set; }
        public int? CID { get; set; } //can be NULL
        public virtual Category Category { get; set; }
    }
}