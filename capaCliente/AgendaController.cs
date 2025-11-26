using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Http.Cors;

namespace capaCliente
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/agenda")]
    public class AgendaController : ApiController
    {
        // ============================================================
        // 🔹 CADENA DE CONEXIÓN A AZURE SQL
        // ============================================================
        private readonly string connectionString =
            "Server=tcp:sqlserver-oscar-01.database.windows.net,1433;" +
            "Initial Catalog=tiendas;" +
            "Persist Security Info=False;" +
            "User ID=adminsql;" +
            "Password=P@ssw0rd2025!;" +
            "MultipleActiveResultSets=False;" +
            "Encrypt=True;" +
            "TrustServerCertificate=False;" +
            "Connection Timeout=30;";

        // ============================================================
        // CLIENTES
        // ============================================================
        [HttpGet]
        [Route("clientes")]
        public IHttpActionResult GetClientes()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Cliente", conexion))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return Ok(dt);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("clientes/agregar")]
        public IHttpActionResult AddCliente([FromBody] ClienteModel data)
        {
            if (data == null) return BadRequest("Datos inválidos");

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Cliente (Nombre, Documento) VALUES (@n, @d)", conexion))
                {
                    cmd.Parameters.AddWithValue("@n", data.Nombre);
                    cmd.Parameters.AddWithValue("@d", data.Documento ?? (object)DBNull.Value);

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    return Ok(new { mensaje = "Cliente registrado correctamente" });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("clientes/eliminar/{id}")]
        public IHttpActionResult DeleteCliente(int id)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Cliente WHERE IdCliente=@id", conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conexion.Open();
                    int affected = cmd.ExecuteNonQuery();

                    if (affected == 0) return NotFound();
                    return Ok(new { mensaje = "Cliente eliminado correctamente" });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // ============================================================
        // PRODUCTOS
        // ============================================================
        [HttpGet]
        [Route("productos")]
        public IHttpActionResult GetProductos()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                using (SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Producto", conexion))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return Ok(dt);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("productos/agregar")]
        public IHttpActionResult AddProducto([FromBody] ProductoModel data)
        {
            if (data == null) return BadRequest("Datos inválidos");

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(
                    @"INSERT INTO Producto (Nombre, Precio, Stock, IdCategoria, IdProveedor)
                      VALUES (@n,@p,@s,@c,@prov)", conexion))
                {
                    cmd.Parameters.AddWithValue("@n", data.Nombre);
                    cmd.Parameters.AddWithValue("@p", data.Precio);
                    cmd.Parameters.AddWithValue("@s", data.Stock);
                    cmd.Parameters.AddWithValue("@c", data.IdCategoria);
                    cmd.Parameters.AddWithValue("@prov", data.IdProveedor);

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    return Ok(new { mensaje = "Producto agregado correctamente" });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("productos/eliminar/{id}")]
        public IHttpActionResult DeleteProducto(int id)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Producto WHERE IdProducto=@id", conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conexion.Open();
                    int affected = cmd.ExecuteNonQuery();

                    if (affected == 0) return NotFound();
                    return Ok(new { mensaje = "Producto eliminado correctamente" });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // ============================================================
        // VENTAS
        // ============================================================
        [HttpGet]
        [Route("ventas")]
        public IHttpActionResult GetVentas()
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                using (SqlDataAdapter da = new SqlDataAdapter(
                    @"SELECT v.IdVenta, v.Fecha, v.Total, c.Nombre AS Cliente
                      FROM Venta v
                      INNER JOIN Cliente c ON v.IdCliente = c.IdCliente", conexion))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return Ok(dt);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("ventas/registrar")]
        public IHttpActionResult RegistrarVenta([FromBody] VentaModel data)
        {
            if (data == null) return BadRequest("Datos inválidos");

            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Venta (Fecha, Total, IdCliente) VALUES (GETDATE(), @tot, @cli)", conexion))
                {
                    cmd.Parameters.AddWithValue("@tot", data.Total);
                    cmd.Parameters.AddWithValue("@cli", data.IdCliente);

                    conexion.Open();
                    cmd.ExecuteNonQuery();

                    return Ok(new { mensaje = "Venta registrada correctamente" });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("ventas/eliminar/{id}")]
        public IHttpActionResult EliminarVenta(int id)
        {
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(
                    "DELETE FROM Venta WHERE IdVenta=@id", conexion))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    conexion.Open();
                    int affected = cmd.ExecuteNonQuery();

                    if (affected == 0) return NotFound();
                    return Ok(new { mensaje = "Venta eliminada correctamente" });
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }

    // ============================================================
    // MODELOS AJUSTADOS
    // ============================================================
    public class ClienteModel
    {
        public string Nombre { get; set; }
        public string Documento { get; set; }
    }

    public class ProductoModel
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int IdCategoria { get; set; }
        public int IdProveedor { get; set; }
    }

    public class VentaModel
    {
        public int IdCliente { get; set; }
        public decimal Total { get; set; }
    }
}