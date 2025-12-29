using System.ComponentModel.DataAnnotations;

namespace GestionIntApi.Models.Admin
{
    public class Producto
    {

        public int Id { get; set; }

        // 🔥 TIPO DE PRODUCTO (para diferenciar)
        [Required]
        [MaxLength(30)]
        public string TipoProducto { get; set; } // Telefono, TV, Tablet, Laptop, etc.

        // 🔥 IDENTIFICADOR ÚNICO (nullable porque no todo tiene IMEI)
        [MaxLength(50)]
        public string? IMEI { get; set; } // Solo para teléfonos/tablets

        [MaxLength(50)]
        public string? Serie { get; set; } // Para TVs, laptops, etc.

        [Required]
        [MaxLength(50)]
        public string Marca { get; set; }

        [Required]
        [MaxLength(100)]
        public string Modelo { get; set; }

        [MaxLength(50)]
        public string? Color { get; set; }

        [MaxLength(100)]
        public string? Tamano { get; set; } // "55 pulgadas", "6.5 pulgadas", etc.

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } // Disponible, Vendido, Dañado, En Reparación

        // 🔥 Ubicación actual (se actualiza automáticamente con movimientos)
        public int? TiendaActualId { get; set; }
        public Tienda? TiendaActual { get; set; }

        public decimal PrecioCompra { get; set; }
        public decimal? PrecioVenta { get; set; }

        [MaxLength(500)]
        public string? Descripcion { get; set; } // Detalles adicionales

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        // Navegación a movimientos
        public ICollection<MovimientoInventario> Movimientos { get; set; } = new List<MovimientoInventario>();
    }
}
