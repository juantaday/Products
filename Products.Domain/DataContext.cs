namespace Products.Domain
{
    using System.Data.Entity;
    public class DataContext:DbContext
    {

        public DataContext():base ("DefaultConnection")
        {
           //this.Configuration.LazyLoadingEnabled = false;
           //this.Configuration.ProxyCreationEnabled = false;
           //TODO 29 20.25
        }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

    }
}
