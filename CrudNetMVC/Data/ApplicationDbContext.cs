using CrudNetMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudNetMVC.Data
{
    public class ApplicationDbContext: DbContext
    {
        //ctor atajo constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
            
        }
        //agregar los modelos son las tablas de bd
        public DbSet<Contacto> Contacto{ get; set; }
    }
}
