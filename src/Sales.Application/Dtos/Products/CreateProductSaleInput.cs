namespace Sales.Application.Dtos.Products
{
    public class CreateProductSaleInput
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
