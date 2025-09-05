
using Microsoft.EntityFrameworkCore;

namespace SubWallet.Models;

public class AppDbContext : DbContext
{
   public DbSet<Subscription> Subscriptions { get; set; }

   public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
   {
       
   }
}