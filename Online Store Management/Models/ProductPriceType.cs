namespace Online_Store_Management.Models
{
    public record struct ProductTypeEnum
    {
        public enum ProductPriceType
        {
            Low = 0,
            Medium = 1,
            High = 2,
            InvalidPrice,
            Uncategorized
        }
    }
    
}
