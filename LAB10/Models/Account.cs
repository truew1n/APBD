using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LAB10.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(80)]
        public string Email { get; set; }

        [Required]
        [MaxLength(9)]
        public string Phone { get; set; }

        public Role Role { get; set; }

        public ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
