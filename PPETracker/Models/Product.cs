using System;
using System.ComponentModel.DataAnnotations;

namespace PPETracker.Models
{
    public abstract class Product
    {
        public int ID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Name { get; set; }

        public int CategoryID { get; set; }

        [StringLength(50)]
        public string PhotoLink { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Brand { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [StringLength(100)]
        public string Comments { get; set; }

        public int Quantity { get; set; }

        public Category Category { get; set; }
    }
}
