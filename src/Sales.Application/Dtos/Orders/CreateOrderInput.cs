using System;

namespace Sales.Application.Dtos.Orders
{
    public class CreateOrderInput
    {
        public Guid UserId { get; set; }
        public string Currency { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
    }
}
