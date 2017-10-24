﻿namespace Products.Domain
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "The feld {0} is required.")]
        [MaxLength(50, ErrorMessage = "The feld {0} only can contain {1} characters lenght.")]
        [Index("Category_Description_Index",IsUnique =true)]
        public string  Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
