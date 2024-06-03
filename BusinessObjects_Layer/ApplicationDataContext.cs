using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects_Layer;

public class ApplicationDataContext : IdentityDbContext
{
    public ApplicationDataContext()
    {

    }

    public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options): base(options)
    {

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }
    // public DbSet<User> Users {get; set;}
    public DbSet<Cart> Carts { get; set; }
    // public DbSet<Order> Orders {get; set;}
    // public DbSet<OrderDetail> OrderDetails {get; set;}
}
