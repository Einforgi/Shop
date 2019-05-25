namespace Shop.Web.Data
{

    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;

    // Esta clase "SeedDb" es un alimentador de base de datos, es decir,
    // nos sirve para no tener que estar entrando datos de ejemplo
    // cada vez que los necesitemos, porque se haya borrado la base
    // de datos o por cualquier otro motivo. En este caso creamos 3
    // productos... Para que se ejecute esta clase, habrá que hacer
    // unos cambios tanto en "Program.cs" como en "Startup.cs", que son 
    // las clases que se ejecutan inicialmente al correr el proyecto.
    
    public class SeedDb
    {
        private readonly DataContext context;
        private Random random;

        public SeedDb(DataContext context)
        {
            this.context = context;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            if (!this.context.Products.Any())
            {
                this.AddProduct("iPhone X");
                this.AddProduct("Samsung S10");
                this.AddProduct("Xiaomi Pocophone F1");
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(1000),
                IsAvailabe = true,
                Stock = this.random.Next(100)
            });
        }
    }
}