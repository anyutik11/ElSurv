using Microsoft.EntityFrameworkCore;


namespace web.Db;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options) { }


    public DbSet<Company> Company { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Review> Review { get; set; }
    public DbSet<Guest> Guest { get; set; }
    public DbSet<Answer> Answer { get; set; }
    //public DbSet<Restaurants> Restaurants { get; set; }

    public DbSet<Bonuce> Bonuce{ get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.ApplyConfiguration(new CompanyConfig());

        modelBuilder.Entity<Company>()
            .HasQueryFilter(p => p.deleted == false);

        modelBuilder.Entity<User>()
            .HasQueryFilter(p => p.deleted == false);

        modelBuilder.Entity<Review>()
            .HasQueryFilter(p => p.deleted == false);

        modelBuilder.Entity<Guest>()
            .HasQueryFilter(p => p.deleted == false);

        modelBuilder.Entity<Answer>()
            .HasQueryFilter(p => p.deleted == false);

        //modelBuilder.Entity<Restaurants>().HasQueryFilter(p => p.deleted == false);

        modelBuilder.Entity<Bonuce>()
            .HasQueryFilter(p => p.deleted == false);

    }
    


}
