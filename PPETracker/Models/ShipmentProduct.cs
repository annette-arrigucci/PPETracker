namespace PPETracker.Models
{
    public class ShipmentProduct
    {
        public int ID { get; set; }
        public int ShipmentID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public Shipment Shipment { get; set; }
        public Product Product { get; set; }
    }
}
