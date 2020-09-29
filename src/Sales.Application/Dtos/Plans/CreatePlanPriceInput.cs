using System;

namespace Sales.Application.Dtos.Plans
{
    public class CreatePlanPriceInput
    {
        public Guid PlanId { get; set; }
        public string Currency { get; set; }
        public decimal Price { get; set; }
    }
}
