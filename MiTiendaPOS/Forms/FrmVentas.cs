using System.ComponentModel;
using MiTiendaPOS.Dtos;
using MiTiendaPOS.Models;
using MiTiendaPOS.Services;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MiTiendaPOS.Forms
{
    public partial class FrmVentas : Form
    {
        private readonly VentaService _servicio = new();
        private readonly BindingList<LineaVenta> _lineas = new();
        private int _ventaId = 0;   

        public FrmVentas()
        {
            InitializeComponent();
        }

        private void FrmVentas_Load(object sender, EventArgs e)
        {
            CargarCatalogos();
            dgvDetalle.DataSource = _lineas;
            dgvDetalle.Columns["ProductoId"].Visible = false;
            dgvDetalle.Columns["PrecioUnitario"].HeaderText = "Precio";
            dgvDetalle.Columns["PrecioUnitario"].DefaultCellStyle.Format = "N2";
            dgvDetalle.Columns["Subtotal"].DefaultCellStyle.Format = "N2";
            CargarHistorial();
            NuevaVenta();
        }

        
        private void CargarCatalogos()
        {
            cboCliente.DisplayMember = "Nombre";
            cboCliente.ValueMember = "Id";
            cboCliente.DataSource = _servicio.ListarClientes();

            cboUsuario.DisplayMember = "NombreUsuario";
            cboUsuario.ValueMember = "Id";
            cboUsuario.DataSource = _servicio.ListarUsuarios();

            cboProducto.DisplayMember = "Nombre";
            cboProducto.ValueMember = "Id";
            cboProducto.DataSource = _servicio.ListarProductosActivos();
        }

        private void CargarHistorial()
        {
            dgvVentas.DataSource = _servicio.ListarVentas()
                .Select(v => new
                {
                    v.Id,
                    Fecha = v.Fecha.ToString("dd/MM/yyyy HH:mm"),
                    Cliente = v.Cliente.Nombre,
                    v.Total
                })
                .ToList();
        }

        private void NuevaVenta()
        {
            _ventaId = 0;
            _lineas.Clear();
            nudCantidad.Value = 1;
            lblVenta.Text = "Venta nueva";
            btnEliminar.Enabled = false;
            dgvVentas.ClearSelection();
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            lblTotal.Text = $"Total: ${_lineas.Sum(l => l.Subtotal):N2}";
        }

        
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboProducto.SelectedItem is not Producto producto) return;
            int cantidad = (int)nudCantidad.Value;

            LineaVenta? existente =
                _lineas.FirstOrDefault(l => l.ProductoId == producto.Id);
            if (existente != null)
            {
                existente.Cantidad += cantidad;
                _lineas.ResetBindings();  
            }
            else
            {
                _lineas.Add(new LineaVenta
                {
                    ProductoId = producto.Id,
                    Producto = producto.Nombre,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.PrecioUnitario
                });
            }
            nudCantidad.Value = 1;
            ActualizarTotal();
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            if (dgvDetalle.CurrentRow?.DataBoundItem is LineaVenta linea)
            {
                _lineas.Remove(linea);
                ActualizarTotal();
            }
        }

        private void btnNueva_Click(object sender, EventArgs e) => NuevaVenta();

        
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedValue is not int clienteId ||
                cboUsuario.SelectedValue is not int usuarioId)
            {
                MessageBox.Show("Selecciona el cliente y el usuario.", "MiTiendaPOS");
                return;
            }

            try
            {
                if (_ventaId == 0)
                {
                    int id = _servicio.Registrar(clienteId, usuarioId, _lineas);
                    MessageBox.Show($"Venta #{id} registrada.", "MiTiendaPOS",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    _servicio.Actualizar(_ventaId, clienteId, usuarioId, _lineas);
                    MessageBox.Show($"Venta #{_ventaId} actualizada.", "MiTiendaPOS",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                CargarCatalogos();   
                CargarHistorial();
                NuevaVenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo guardar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        
        private void dgvVentas_CellClick(object sender,
                                         DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int id = Convert.ToInt32(dgvVentas.Rows[e.RowIndex].Cells["Id"].Value);

            Venta? venta = _servicio.ObtenerConDetalle(id);
            if (venta == null) return;

            _ventaId = venta.Id;
            cboCliente.SelectedValue = venta.ClienteId;
            cboUsuario.SelectedValue = venta.UsuarioId;

            _lineas.Clear();
            foreach (var d in venta.Detalles)
            {
                _lineas.Add(new LineaVenta
                {
                    ProductoId = d.ProductoId,
                    Producto = d.Producto.Nombre,
                    Cantidad = d.Cantidad,
                    PrecioUnitario = d.PrecioUnitario
                });
            }
            lblVenta.Text = $"Editando venta #{venta.Id}";
            btnEliminar.Enabled = true;
            ActualizarTotal();
        }

        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_ventaId == 0) return;
            var respuesta = MessageBox.Show(
                $"¿Eliminar la venta #{_ventaId}? Se devolverá el stock.",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes) return;

            try
            {
                _servicio.Eliminar(_ventaId);
                CargarCatalogos();
                CargarHistorial();
                NuevaVenta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No se pudo eliminar",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
