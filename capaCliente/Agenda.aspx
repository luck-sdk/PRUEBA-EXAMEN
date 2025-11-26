<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Agenda.aspx.cs" Inherits="capaCliente.Agenda" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sistema de Gestión de Ventas</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <style>
        :root {
            --primary: #2c5aa0;
            --primary-dark: #1e3d72;
            --primary-light: #4a7bd8;
            --secondary: #00c897;
            --accent: #ff6b6b;
            --light: #f8fafc;
            --dark: #2d3748;
            --gray: #718096;
            --gray-light: #e2e8f0;
            --border-radius: 10px;
            --shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            --shadow-hover: 0 8px 15px rgba(0, 0, 0, 0.15);
            --transition: all 0.3s ease;
        }

        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body {
            font-family: 'Segoe UI', system-ui, -apple-system, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: var(--dark);
            line-height: 1.6;
            min-height: 100vh;
            padding: 20px;
        }

        .system-container {
            max-width: 1200px;
            margin: 0 auto;
            background: white;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
            overflow: hidden;
        }

        .system-header {
            background: linear-gradient(135deg, var(--primary) 0%, var(--primary-dark) 100%);
            color: white;
            padding: 25px 30px;
            text-align: center;
            border-bottom: 4px solid var(--secondary);
        }

        .system-header h1 {
            font-size: 2.2rem;
            font-weight: 700;
            margin-bottom: 8px;
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 15px;
        }

        .system-header p {
            opacity: 0.9;
            font-size: 1.1rem;
        }

        .navigation {
            background: var(--light);
            padding: 20px 30px;
            border-bottom: 1px solid var(--gray-light);
        }

        .nav-buttons {
            display: flex;
            gap: 15px;
            justify-content: center;
            flex-wrap: wrap;
        }

        .nav-btn {
            display: flex;
            align-items: center;
            gap: 10px;
            padding: 14px 28px;
            border: none;
            border-radius: var(--border-radius);
            background: white;
            color: var(--primary);
            font-weight: 600;
            font-size: 1rem;
            cursor: pointer;
            transition: var(--transition);
            box-shadow: var(--shadow);
            border: 2px solid transparent;
        }

        .nav-btn:hover {
            background: var(--primary);
            color: white;
            transform: translateY(-3px);
            box-shadow: var(--shadow-hover);
        }

        .nav-btn.active {
            background: var(--primary);
            color: white;
            border-color: var(--secondary);
        }

        .content-area {
            padding: 30px;
            background: var(--light);
            min-height: 500px;
        }

        .view-section {
            background: white;
            border-radius: var(--border-radius);
            box-shadow: var(--shadow);
            margin-bottom: 25px;
            overflow: hidden;
        }

        .section-header {
            background: linear-gradient(135deg, var(--primary-light) 0%, var(--primary) 100%);
            color: white;
            padding: 20px 25px;
            display: flex;
            align-items: center;
            gap: 12px;
        }

        .section-header i {
            font-size: 1.4rem;
        }

        .section-header h3 {
            font-size: 1.4rem;
            font-weight: 600;
        }

        .form-container {
            padding: 25px;
        }

        .form-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
            gap: 20px;
            margin-bottom: 20px;
        }

        .form-group {
            margin-bottom: 15px;
        }

        .form-label {
            display: block;
            margin-bottom: 8px;
            font-weight: 600;
            color: var(--dark);
            font-size: 0.95rem;
        }

        .form-control {
            width: 100%;
            padding: 12px 15px;
            border: 2px solid var(--gray-light);
            border-radius: var(--border-radius);
            font-size: 1rem;
            transition: var(--transition);
            background: var(--light);
        }

        .form-control:focus {
            border-color: var(--primary);
            box-shadow: 0 0 0 3px rgba(44, 90, 160, 0.1);
            outline: none;
            background: white;
        }

        .form-actions {
            display: flex;
            gap: 12px;
            margin-top: 25px;
            flex-wrap: wrap;
        }

        .btn {
            display: inline-flex;
            align-items: center;
            gap: 8px;
            padding: 12px 24px;
            border: none;
            border-radius: var(--border-radius);
            font-weight: 600;
            cursor: pointer;
            transition: var(--transition);
            font-size: 0.95rem;
        }

        .btn-primary {
            background: var(--primary);
            color: white;
        }

        .btn-primary:hover {
            background: var(--primary-dark);
            transform: translateY(-2px);
            box-shadow: var(--shadow-hover);
        }

        .btn-success {
            background: var(--secondary);
            color: white;
        }

        .btn-success:hover {
            background: #00b286;
            transform: translateY(-2px);
        }

        .data-container {
            padding: 20px;
        }

        .data-table {
            width: 100%;
            border-collapse: collapse;
            border-radius: var(--border-radius);
            overflow: hidden;
            box-shadow: var(--shadow);
        }

        .data-table th {
            background: var(--primary);
            color: white;
            padding: 16px;
            text-align: left;
            font-weight: 600;
            font-size: 0.95rem;
        }

        .data-table td {
            padding: 14px 16px;
            border-bottom: 1px solid var(--gray-light);
        }

        .data-table tr:nth-child(even) {
            background: var(--light);
        }

        .data-table tr:hover {
            background: rgba(44, 90, 160, 0.05);
            transform: scale(1.01);
            transition: var(--transition);
        }

        .stats-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 20px;
            margin-bottom: 25px;
        }

        .stat-card {
            background: white;
            padding: 20px;
            border-radius: var(--border-radius);
            box-shadow: var(--shadow);
            text-align: center;
            border-top: 4px solid var(--primary);
        }

        .stat-card i {
            font-size: 2rem;
            color: var(--primary);
            margin-bottom: 10px;
        }

        .stat-value {
            font-size: 1.8rem;
            font-weight: bold;
            color: var(--dark);
            margin: 8px 0;
        }

        .stat-label {
            color: var(--gray);
            font-size: 0.9rem;
            font-weight: 500;
        }

        @media (max-width: 768px) {
            body {
                padding: 10px;
            }
            
            .system-header h1 {
                font-size: 1.8rem;
                flex-direction: column;
                gap: 8px;
            }
            
            .nav-buttons {
                flex-direction: column;
            }
            
            .nav-btn {
                width: 100%;
                justify-content: center;
            }
            
            .form-grid {
                grid-template-columns: 1fr;
            }
            
            .form-actions {
                flex-direction: column;
            }
            
            .btn {
                width: 100%;
                justify-content: center;
            }
            
            .stats-grid {
                grid-template-columns: 1fr;
            }
        }

        /* Animaciones suaves */
        .view-section {
            animation: fadeInUp 0.5s ease;
        }

        @keyframes fadeInUp {
            from {
                opacity: 0;
                transform: translateY(20px);
            }
            to {
                opacity: 1;
                transform: translateY(0);
            }
        }

        /* Efectos de carga suaves */
        .form-control, .btn, .nav-btn {
            transition: var(--transition);
        }

        /* Mejoras visuales para dropdowns */
        select.form-control {
            appearance: none;
            background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='12' height='12' fill='%232c5aa0' viewBox='0 0 16 16'%3E%3Cpath d='M7.247 11.14 2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z'/%3E%3C/svg%3E");
            background-repeat: no-repeat;
            background-position: right 15px center;
            background-size: 12px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="system-container">

            <div class="system-header">
                <h1><i class="fas fa-store"></i> Sistema de Gestión de Ventas</h1>
                <p>Gestión integral de productos, ventas y clientes</p>
            </div>

            <div class="navigation">
                <div class="nav-buttons">
                    <asp:Button ID="btnProductos" runat="server" Text="📦 Gestión de Productos" OnClick="btnProductos_Click" CssClass="nav-btn active" />
                    <asp:Button ID="btnVentas" runat="server" Text="💰 Procesar Ventas" OnClick="btnVentas_Click" CssClass="nav-btn" />
                    <asp:Button ID="btnResumen" runat="server" Text="📊 Dashboard" OnClick="btnResumen_Click" CssClass="nav-btn" />
                </div>
            </div>

            <div class="content-area">
                <asp:MultiView ID="mvPrincipal" runat="server" ActiveViewIndex="0">

                    <!-- ================== VISTA PRODUCTOS ================== -->
                    <asp:View ID="viewProductos" runat="server">
                        <div class="view-section">
                            <div class="section-header">
                                <i class="fas fa-boxes"></i><h3>Gestión de Inventario</h3>
                            </div>

                            <div class="form-container">
                                <div class="form-grid">
                                    <div class="form-group">
                                        <asp:Label Text="Nombre" runat="server" CssClass="form-label" />
                                        <asp:TextBox ID="txtNombreProducto" runat="server" CssClass="form-control" />
                                    </div>

                                    <div class="form-group">
                                        <asp:Label Text="Categoría" runat="server" CssClass="form-label" />
                                        <asp:TextBox ID="txtCategoria" runat="server" CssClass="form-control" />
                                    </div>

                                    <div class="form-group">
                                        <asp:Label Text="Marca" runat="server" CssClass="form-label" />
                                        <asp:TextBox ID="txtMarca" runat="server" CssClass="form-control" />
                                    </div>

                                    <div class="form-group">
                                        <asp:Label Text="Precio" runat="server" CssClass="form-label" />
                                        <asp:TextBox ID="txtPrecioProducto" runat="server" CssClass="form-control" />
                                    </div>

                                    <div class="form-group">
                                        <asp:Label Text="Stock" runat="server" CssClass="form-label" />
                                        <asp:TextBox ID="txtStockProducto" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="form-actions">
                                    <asp:Button ID="btnAgregarProducto" runat="server" Text="➕ Agregar Producto"
                                        CssClass="btn btn-primary" OnClick="btnAgregarProducto_Click" />
                                </div>
                            </div>
                        </div>

                        <div class="view-section">
                            <div class="section-header">
                                <i class="fas fa-list"></i><h3>Inventario Actual</h3>
                            </div>

                            <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="True"
                                CssClass="data-table" GridLines="None">
                            </asp:GridView>
                        </div>

                    </asp:View>

                    <!-- ================== VISTA VENTAS ================== -->
                    <asp:View ID="viewVentas" runat="server">
                        <div class="view-section">
                            <div class="section-header">
                                <i class="fas fa-cash-register"></i><h3>Registro de Ventas</h3>
                            </div>

                            <div class="form-container">
                                <div class="form-grid">

                                    <div class="form-group">
                                        <asp:Label Text="Cliente" runat="server" CssClass="form-label" />
                                        <asp:DropDownList ID="ddlClientes" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label Text="Producto" runat="server" CssClass="form-label" />
                                        <asp:DropDownList ID="ddlProductos" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <asp:Label Text="Cantidad" runat="server" CssClass="form-label" />
                                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control" />
                                    </div>

                                    <div class="form-group">
                                        <asp:Label Text="Nuevo Cliente" runat="server" CssClass="form-label" />
                                        <asp:TextBox ID="txtNuevoCliente" runat="server" CssClass="form-control" />
                                    </div>

                                </div>

                                <div class="form-actions">
                                    <asp:Button ID="btnNuevoCliente" runat="server" Text="👤 Registrar Cliente"
                                        CssClass="btn btn-success" OnClick="btnNuevoCliente_Click" />

                                    <asp:Button ID="btnRegistrarVenta" runat="server" Text="💾 Registrar Venta"
                                        CssClass="btn btn-primary" OnClick="btnRegistrarVenta_Click" />
                                </div>

                            </div>
                        </div>

                        <div class="view-section">
                            <div class="section-header">
                                <i class="fas fa-history"></i><h3>Historial de Ventas</h3>
                            </div>

                            <asp:GridView ID="gvVentas" runat="server" AutoGenerateColumns="True"
                                CssClass="data-table" />
                        </div>

                    </asp:View>

                    <!-- ================== VISTA RESUMEN ================== -->
                    <asp:View ID="viewResumen" runat="server">
                        <div class="view-section">
                            <div class="section-header"><i class="fas fa-chart-bar"></i><h3>Resumen</h3></div>

                            <asp:GridView ID="gvResumen" runat="server" AutoGenerateColumns="True"
                                CssClass="data-table" />
                        </div>
                    </asp:View>

                </asp:MultiView>
            </div>

        </div>
    </form>
</body>