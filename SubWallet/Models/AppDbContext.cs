using SubWallet.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

public class AppDbContext : DbContext
{
   public DbSet<Subscription> Subscriptions { get; set; }

   public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
   {
       
   }
}