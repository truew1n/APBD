using System.ComponentModel.DataAnnotations;

namespace LAB10.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Amount { get; set; }
    }
}
