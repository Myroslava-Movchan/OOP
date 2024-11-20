using System.ComponentModel.DataAnnotations;
namespace Online_Store_Management.Models
{

    public abstract class Customer
    {
        private Product? _product;

        [Key]
        public int Id { get; set; }
        [Required]
        public required string? LastName { get; set; }
        public int PostIndex { get; set; }

        public Product? GetProduct()
        { return _product; }

        public void SetProduct(Product value)
        { _product = value; }
        public abstract decimal GetDiscount();
        public virtual void Help(string issue)
        {
            Console.WriteLine($"The assistant will answer during one week to help you with your issue: {issue}.");
        }
        public void Recommendation()
        {
            Console.WriteLine("Turn on your notifications to receive information about new products!");
        }
    }
}
