namespace ELECTIENDA.ViewModel
{
    public class ProviderViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public string? Img { get; set; }
        public string? licenceImg { get; set; }
        public string? role { get; set; }
        public List<string>? shops { get; set; }
        public List<string>? Phones { get; set; }

        public float Balance { get; set; }

        //public List<string> Products { get; set; } 
        // public List<string> services { get; set; } 
    }
}
