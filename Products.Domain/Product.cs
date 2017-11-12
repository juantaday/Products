namespace Products.Domain
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;
    using System.Runtime.Serialization;
   public class Product
    {
        [Key]
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The feld {0} is required.")]
        [MaxLength(50, ErrorMessage = "The feld {0} only can contain {1} characters lenght.")]
        [Index("Product_Description_Index", IsUnique = true)]
        public string  Description { get; set; }

        [Required(ErrorMessage = "The feld {0} is required.")]
        [DisplayFormat(DataFormatString = "{0:C2}",ApplyFormatInEditMode =false)]
        public decimal  Price { get; set; }

        [Display(Name ="Is Active")]
        public bool  IsActive { get; set; }

        [Display(Name = "Last Porcharse")]
        [DataType (DataType.Date)]
        public DateTime  LastPorcharse { get; set; }

        public string  Image { get; set; }

        [NotMapped]
        [Display (Name ="Image")]
        public HttpPostedFileBase ImageFile { get; set; }

        public double Stock { get; set; }

        [DataType(DataType.MultilineText)]
        public string  Reamarks { get; set; }

       // [JsonIgnore]
        public virtual  Category Category { get; set; }

    }
}
