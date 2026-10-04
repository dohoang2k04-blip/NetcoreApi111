using Microsoft.EntityFrameworkCore;
using csmvc.Models;

namespace csmvc.Data;
public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Product { get; set; }
}
//DbSet<Product>: refer to the class "Product", Product -> refer to table named "product"

//khai báo thêm các table ở đây