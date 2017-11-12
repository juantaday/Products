namespace Products.Models
{
    using System;
    public class Product
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastPorcharse { get; set; }
        public string Image { get; set; }
        public double Stock { get; set; }
        public string Reamarks { get; set; }

        public string ImageFullPath
        { get
            {
                if (this.Image != null)
                {
                    return string.Format("http://soccerapi.somee.com/{0}",
                    this.Image.Substring(1));
                }
                else
                {
                    return null;
                }
                   
            }
       }
    }
}
