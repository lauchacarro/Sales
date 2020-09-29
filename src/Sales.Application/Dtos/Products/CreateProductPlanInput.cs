namespace Sales.Application.Dtos.Products
{
    public class CreateProductPlanInput
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PlanType { get; set; }
        public string Duration { get; set; }

        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}
