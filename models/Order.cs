namespace Orders.Api.models
{
    public sealed class Order
    {
        public int OrderId { get; set; }
        public required string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public required string CustomerCode { get; set; }
        public required string CompanyName { get; set; }
        public int? VehicleId { get; set; }
        public string? PlateNumber { get; set; }
        public string? VehicleType { get; set; }
        public required string OriginCity { get; set; }
        public required string DestinationCity { get; set; }
        public required string CargoDescription { get; set; }
        public decimal WeightKg { get; set; }
        public decimal FreightAmount { get; set; }
        public required string CurrencyCode { get; set; }
        public required string Status { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}