using GestionIntApi.DTO.Admin;
using GestionIntApi.Models;
using GestionIntApi.Models.Admin;
using GestionIntApi.Repositorios.Contrato;
using GestionIntApi.Repositorios.Implementacion.Admin;
using Microsoft.EntityFrameworkCore;

namespace GestionIntApi.Repositorios.Interfaces.Admin
{
    public class TiendaInventarioService: ITiendaInventarioService
    {

        private readonly IGenericRepository<Tienda> _tiendaRepository;
        private readonly IGenericRepository<Producto> _productoRepository;

        public TiendaInventarioService(
            IGenericRepository<Tienda> tiendaRepository,
            IGenericRepository<Producto> productoRepository)
        {
            _tiendaRepository = tiendaRepository;
            _productoRepository = productoRepository;
        }

        public async Task<List<TiendaDestinoDTO>> ObtenerTiendasDisponibles()
        {
            var query = await _tiendaRepository.Consultar();
            var tiendas = await query.ToListAsync();

            return tiendas.Select(t => new TiendaDestinoDTO
            {
                Id = t.Id,
                NombreTienda = t.NombreTienda,
                Direccion = t.Direccion
            }).ToList();
        }

        public async Task<StockBodegaDTO> ObtenerInventarioTienda(int tiendaId)
        {
            var query = await _productoRepository.Consultar(p => p.TiendaId == tiendaId);
            var productos = await query.ToListAsync();

            var stock = new StockBodegaDTO
            {
                TotalProductos = productos.Count,
                ProductosDisponibles = productos.Count(p => p.Estado == "Disponible"),
                ProductosEnTransito = productos.Count(p => p.Estado == "En Tránsito"),
                ProductosDanados = productos.Count(p => p.Estado == "Dañado"),
                ValorTotal = productos.Sum(p => p.PrecioVenta ?? p.PrecioCompra),
                DesglosePorTipo = productos
                    .GroupBy(p => p.TipoProducto)
                    .Select(g => new StockPorTipoDTO
                    {
                        TipoProducto = g.Key,
                        Cantidad = g.Count(),
                        ValorTotal = g.Sum(p => p.PrecioVenta ?? p.PrecioCompra)
                    })
                    .ToList()
            };

            return stock;
        }

        public async Task<List<ProductoBodegaDTO>> ObtenerProductosTienda(int tiendaId)
        {
            var query = await _productoRepository.Consultar(p => p.TiendaId == tiendaId);
            var productos = await query.ToListAsync();

            return productos.Select(p => new ProductoBodegaDTO
            {
                Id = p.Id,
                TipoProducto = p.TipoProducto,
                Codigo = p.Codigo,
                Marca = p.Marca,
                Modelo = p.Modelo,
                IMEI = p.IMEI,
                Serie = p.Serie,
                Color = p.Color,
                Tamano = p.Tamano,
                Estado = p.Estado,
                PrecioCompra = p.PrecioCompra,
                PrecioVenta = p.PrecioVenta,
                FechaIngreso = p.FechaRegistro,
                DiasEnBodega = (DateTime.UtcNow - p.FechaRegistro).Days
            }).ToList();
        }
    }
}

