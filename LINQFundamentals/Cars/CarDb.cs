using System.Data.Entity;

namespace Cars
{
    class CarDb : DbContext
    {
        public DbSet<Car> Cars { get; set; }
    }
}
