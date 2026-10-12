namespace MiTiendaPOS.Forms
{
    partial class FrmVentas
    {
        
        
        
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpEncabezado = new System.Windows.Forms.GroupBox();
            this.lblVenta = new System.Windows.Forms.Label();
            this.cboUsuario = new System.Windows.Forms.ComboBox();
            this.cboCliente = new System.Windows.Forms.ComboBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.grpDetalle = new System.Windows.Forms.GroupBox();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.cboProducto = new System.Windows.Forms.ComboBox();
            this.lblProducto = new System.Windows.Forms.Label();
            this.dgvDetalle = new System.Windows.Forms.DataGridView();
            this.grpHistorial = new System.Windows.Forms.GroupBox();
            this.dgvVentas = new System.Windows.Forms.DataGridView();
            this.lblTotal = new System.Windows.Forms.Label();
            this.btnNueva = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            
            
            
            this.Text = "Registro de Ventas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.ClientSize = new System.Drawing.Size(1020, 660);
            this.Load += new System.EventHandler(this.FrmVentas_Load);

            
            this.grpEncabezado.Text = "Encabezado de la venta";
            this.grpEncabezado.SetBounds(12, 12, 996, 100);
            
            this.lblVenta.Font = new System.Drawing.Font(this.lblVenta?.Font, System.Drawing.FontStyle.Bold);
            this.lblVenta.Text = "Venta nueva";
            this.lblVenta.SetBounds(10, 20, 300, 24);
            
            this.lblCliente.Text = "Cliente:";
            this.lblCliente.Font = new System.Drawing.Font(this.lblCliente?.Font, System.Drawing.FontStyle.Bold);
            this.lblCliente.SetBounds(10, 50, 60, 20);
            
            this.cboCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCliente.SetBounds(80, 48, 300, 24);
            
            this.lblUsuario.Text = "Usuario:";
            this.lblUsuario.Font = new System.Drawing.Font(this.lblUsuario?.Font, System.Drawing.FontStyle.Bold);
            this.lblUsuario.SetBounds(400, 50, 60, 20);
            
            this.cboUsuario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsuario.SetBounds(470, 48, 200, 24);

            this.grpEncabezado.Controls.Add(this.lblVenta);
            this.grpEncabezado.Controls.Add(this.lblCliente);
            this.grpEncabezado.Controls.Add(this.cboCliente);
            this.grpEncabezado.Controls.Add(this.lblUsuario);
            this.grpEncabezado.Controls.Add(this.cboUsuario);
            this.Controls.Add(this.grpEncabezado);

            
            this.grpDetalle.Text = "Detalle de productos";
            this.grpDetalle.SetBounds(12, 120, 660, 380);

            
            this.lblProducto.Text = "Producto:";
            this.lblProducto.Font = new System.Drawing.Font(this.lblProducto?.Font, System.Drawing.FontStyle.Bold);
            this.lblProducto.SetBounds(10, 20, 70, 20);
            
            this.cboProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProducto.SetBounds(90, 18, 300, 24);
            
            this.lblCantidad.Text = "Cant.:";
            this.lblCantidad.Font = new System.Drawing.Font(this.lblCantidad?.Font, System.Drawing.FontStyle.Bold);
            this.lblCantidad.SetBounds(400, 20, 40, 20);
            
            this.nudCantidad.Minimum = 1;
            this.nudCantidad.Maximum = 1000;
            this.nudCantidad.Value = 1;
            this.nudCantidad.SetBounds(445, 18, 70, 24);
            
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.SetBounds(530, 16, 60, 26);
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            
            this.btnQuitar.Text = "Quitar";
            this.btnQuitar.SetBounds(600, 16, 60, 26);
            this.btnQuitar.Click += new System.EventHandler(this.btnQuitar_Click);

            
            this.dgvDetalle.ReadOnly = true;
            this.dgvDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalle.AllowUserToAddRows = false;
            this.dgvDetalle.SetBounds(10, 50, 640, 320);

            this.grpDetalle.Controls.Add(this.lblProducto);
            this.grpDetalle.Controls.Add(this.cboProducto);
            this.grpDetalle.Controls.Add(this.lblCantidad);
            this.grpDetalle.Controls.Add(this.nudCantidad);
            this.grpDetalle.Controls.Add(this.btnAgregar);
            this.grpDetalle.Controls.Add(this.btnQuitar);
            this.grpDetalle.Controls.Add(this.dgvDetalle);
            this.Controls.Add(this.grpDetalle);

            
            this.grpHistorial.Text = "Historial de Ventas";
            this.grpHistorial.SetBounds(680, 12, 328, 488);
            this.dgvVentas.ReadOnly = true;
            this.dgvVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVentas.AllowUserToAddRows = false;
            this.dgvVentas.SetBounds(690, 40, 308, 440);
            this.dgvVentas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVentas_CellClick);
            this.grpHistorial.Controls.Add(this.dgvVentas);
            this.Controls.Add(this.grpHistorial);

            
            this.lblTotal.Font = new System.Drawing.Font(this.lblTotal?.Font.FontFamily, 14, System.Drawing.FontStyle.Bold);
            this.lblTotal.Text = "Total: $0.00";
            this.lblTotal.SetBounds(12, 510, 300, 30);
            this.Controls.Add(this.lblTotal);

            
            this.btnNueva.Text = "Nueva venta";
            this.btnNueva.SetBounds(320, 510, 100, 30);
            this.btnNueva.Click += new System.EventHandler(this.btnNueva_Click);
            this.Controls.Add(this.btnNueva);

            this.btnGuardar.Text = "Guardar venta";
            this.btnGuardar.SetBounds(430, 510, 120, 30);
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            this.Controls.Add(this.btnGuardar);

            this.btnEliminar.Text = "Eliminar venta";
            this.btnEliminar.SetBounds(560, 510, 120, 30);
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            this.Controls.Add(this.btnEliminar);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpEncabezado;
        private System.Windows.Forms.Label lblVenta;
        private System.Windows.Forms.ComboBox cboUsuario;
        private System.Windows.Forms.ComboBox cboCliente;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.GroupBox grpDetalle;
        private System.Windows.Forms.Button btnQuitar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.ComboBox cboProducto;
        private System.Windows.Forms.Label lblProducto;
        private System.Windows.Forms.DataGridView dgvDetalle;
        private System.Windows.Forms.GroupBox grpHistorial;
        private System.Windows.Forms.DataGridView dgvVentas;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Button btnNueva;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnEliminar;
    }
}
