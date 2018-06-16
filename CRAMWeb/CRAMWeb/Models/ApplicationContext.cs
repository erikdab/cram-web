namespace CRAMWeb.Models
{
    using System.Data.Entity;

    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("name=ApplicationContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
    }
}