namespace Shop.Web.Data.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage ="El campo {0} no puede tener más de {1} caracteres.")]
        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Display(Name = "Precio")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Imagen")]
        public string ImageUrl { get; set; }

        [Display(Name = "Última Compra")]
        public DateTime? LastPurchase { get; set; }

        [Display(Name = "Última Venta")]
        public DateTime? LastSale { get; set; }

        [Display(Name = "¿Disponible?")]
        public bool IsAvailabe { get; set; }

        [Display(Name = "Existencias")]
        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }
    }
}
