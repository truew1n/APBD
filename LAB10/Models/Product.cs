using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LAB10.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Weight { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Width { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Height { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Depth { get; set; }

        public ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
