using capaCliente;
using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace capaCliente
{
    public partial class Agenda : Page
    {
       
    SqlConnection conexion = new SqlConnection(
        ConfigurationManager.ConnectionStrings["ConexionAzure"].ConnectionString
    );

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mvPrincipal.ActiveViewIndex = 0;
                CargarProductos();
                CargarClientes();
                CargarVentas();
            }
        }

        /* ======================= CARGAS ======================= */

        void CargarProductos()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Producto", conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);

            gvProductos.DataSource = dt;
            gvProductos.DataBind();

            ddlProductos.DataSource = dt;
            ddlProductos.DataTextField = "Nombre";
            ddlProductos.DataValueField = "IdProducto";
            ddlProductos.DataBind();
        }

        void CargarClientes()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Cliente", conexion);
            DataTable dt = new DataTable();
            da.Fill(dt);

            ddlClientes.DataSource = dt;
            ddlClientes.DataTextField = "Nombre";
            ddlClientes.DataValueField = "IdCliente";
            ddlClientes.DataBind();
        }

        void CargarVentas()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT v.IdVenta, c.Nombre AS Cliente, p.Nombre AS Producto, dv.Cantidad, dv.PrecioUnitario, " +
                "(dv.Cantidad * dv.PrecioUnitario) AS Total " +
                "FROM DetalleVenta dv " +
                "INNER JOIN Venta v ON dv.IdVenta = v.IdVenta " +
                "INNER JOIN Producto p ON dv.IdProducto = p.IdProducto " +
                "INNER JOIN Cliente c ON v.IdCliente = c.IdCliente", conexion);

            DataTable dt = new DataTable();
            da.Fill(dt);

            gvVentas.DataSource = dt;
            gvVentas.DataBind();
            gvResumen.DataSource = dt;
            gvResumen.DataBind();
        }

        /* ======================= BOTONES ======================= */

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Producto(Nombre, Precio, Stock, IdCategoria, IdProveedor) " +
                "VALUES(@n, @p, @s, 1, 1)", conexion);

            cmd.Parameters.AddWithValue("@n", txtNombreProducto.Text);
            cmd.Parameters.AddWithValue("@p", decimal.Parse(txtPrecioProducto.Text));
            cmd.Parameters.AddWithValue("@s", int.Parse(txtStockProducto.Text));

            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();

            CargarProductos();
        }

        protected void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            if (txtNuevoCliente.Text.Trim() == "") return;

            SqlCommand cmd = new SqlCommand("INSERT INTO Cliente(Nombre) VALUES(@n)", conexion);
            cmd.Parameters.AddWithValue("@n", txtNuevoCliente.Text);

            conexion.Open();
            cmd.ExecuteNonQuery();
            conexion.Close();

            CargarClientes();
        }

        protected void btnRegistrarVenta_Click(object sender, EventArgs e)
        {
            int idCliente = int.Parse(ddlClientes.SelectedValue);
            int idProducto = int.Parse(ddlProductos.SelectedValue);
            int cantidad = int.Parse(txtCantidad.Text);

            SqlCommand cmdPrecio = new SqlCommand("SELECT Precio FROM Producto WHERE IdProducto=@id", conexion);
            cmdPrecio.Parameters.AddWithValue("@id", idProducto);

            conexion.Open();
            decimal precio = Convert.ToDecimal(cmdPrecio.ExecuteScalar());
            conexion.Close();

            SqlCommand cmdVenta = new SqlCommand("INSERT INTO Venta(Fecha, Total, IdCliente) VALUES(GETDATE(), @t, @c)", conexion);
            cmdVenta.Parameters.AddWithValue("@t", precio * cantidad);
            cmdVenta.Parameters.AddWithValue("@c", idCliente);

            conexion.Open();
            cmdVenta.ExecuteNonQuery();
            conexion.Close();

            SqlCommand cmdUltima = new SqlCommand("SELECT MAX(IdVenta) FROM Venta", conexion);
            conexion.Open();
            int idVenta = Convert.ToInt32(cmdUltima.ExecuteScalar());
            conexion.Close();

            SqlCommand cmdDV = new SqlCommand(
                "INSERT INTO DetalleVenta(IdVenta, IdProducto, Cantidad, PrecioUnitario) VALUES(@v, @p, @cant, @pre)",
                conexion);

            cmdDV.Parameters.AddWithValue("@v", idVenta);
            cmdDV.Parameters.AddWithValue("@p", idProducto);
            cmdDV.Parameters.AddWithValue("@cant", cantidad);
            cmdDV.Parameters.AddWithValue("@pre", precio);

            conexion.Open();
            cmdDV.ExecuteNonQuery();
            conexion.Close();

            SqlCommand cmdStock = new SqlCommand("UPDATE Producto SET Stock = Stock - @cant WHERE IdProducto=@id", conexion);
            cmdStock.Parameters.AddWithValue("@cant", cantidad);
            cmdStock.Parameters.AddWithValue("@id", idProducto);

            conexion.Open();
            cmdStock.ExecuteNonQuery();
            conexion.Close();

            CargarVentas();
            CargarProductos();
        }

        /* ======================= NAVEGACIÓN ======================= */

        protected void btnProductos_Click(object sender, EventArgs e) => mvPrincipal.ActiveViewIndex = 0;
        protected void btnVentas_Click(object sender, EventArgs e) => mvPrincipal.ActiveViewIndex = 1;
        protected void btnResumen_Click(object sender, EventArgs e) => mvPrincipal.ActiveViewIndex = 2;
    }
}
