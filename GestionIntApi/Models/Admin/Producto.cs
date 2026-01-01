using System.ComponentModel.DataAnnotations;

namespace GestionIntApi.Models.Admin
{
    public class Producto
    {

        public int Id { get; set; }

        [Required]
       
        public string Codigo { get; set; }
        // 🔥 TIPO DE PRODUCTO (para diferenciar)
        [Required]
        
        public string TipoProducto { get; set; } // Telefono, TV, Tablet, Laptop, etc.

        // 🔥 IDENTIFICADOR ÚNICO (nullable porque no todo tiene IMEI)
        
        public string? IMEI { get; set; } // Solo para teléfonos/tablets

       
        public string? Serie { get; set; } // Para TVs, laptops, etc.

        [Required]
       
        public string Marca { get; set; }

        [Required]

        public string Modelo { get; set; }

      
        public string? Color { get; set; }

      
        public string? Tamano { get; set; } // "55 pulgadas", "6.5 pulgadas", etc.

        [Required]
     
        public string Estado { get; set; } // Disponible, Vendido, Dañado, En Reparación

        // 🔥 Ubicación actual (se actualiza automáticamente con movimientos)
        public int? TiendaActualId { get; set; }
        public Tienda? TiendaActual { get; set; }

        public decimal PrecioCompra { get; set; }
        public decimal? PrecioVenta { get; set; }

       
        public string? Descripcion { get; set; } // Detalles adicionales

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Navegación a movimientos
        public ICollection<MovimientoInventario> Movimientos { get; set; } = new List<MovimientoInventario>();
    }
}
