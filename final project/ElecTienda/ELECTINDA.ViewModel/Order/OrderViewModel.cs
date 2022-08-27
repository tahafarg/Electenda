namespace ELECTINDA.ViewModel
{

    public enum statues
    {
        Done, NotDone, Pending
    }
    public class OrderViewModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }

        //public int OrderNum { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public int Amount { get; set; }
        public statues Statue { get; set; }

        public List<string> OrderDetailes { get; set; }
    }
}
