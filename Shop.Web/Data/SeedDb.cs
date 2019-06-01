namespace Shop.Web.Data
{
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

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
        private readonly UserManager<User> userManager;
        private Random random;

        public SeedDb(DataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            // Antes de crear los productos vamos a crear un usuario
            var user = await this.userManager.FindByEmailAsync("oscar.einforgi@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Oscar",
                    LastName = "Rodriguez",
                    Email = "oscar.einforgi@gmail.com",
                    UserName = "oscar.einforgi@gmail.com",
                    PhoneNumber = "667520666"
                };

                var result = await this.userManager.CreateAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("iPhone X", user);
                this.AddProduct("Samsung S10", user);
                this.AddProduct("Xiaomi Pocophone F1", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(1000),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User = user
            });
        }
    }
}