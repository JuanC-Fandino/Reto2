using System;
using System.Data;
using System.Windows.Forms;
using Capa_Entidad;
using Capa_Negocio;

namespace Reto2
{
    public partial class Form1 : Form
    {
        ClaseNegocio _claseNegocio = new ClaseNegocio();
        Libro miLibro = new Libro();

        public Form1()
        {
            InitializeComponent();
        }

        private void Mantenimiento(string accion)
        {
            miLibro.Codigo = !string.IsNullOrEmpty(txtCodigo.Text) ? Convert.ToInt32(txtCodigo.Text) : 0;
            miLibro.Titulo = txtTitulo.Text;
            miLibro.Autor = txtAutor.Text;
            miLibro.Editorial = txtEditorial.Text;
            miLibro.Precio = Convert.ToSingle(txtPrecio.Text);
            miLibro.Cantidad = (int) txtCantidad.Value;
            miLibro.Accion = accion;
            var mensaje = _claseNegocio.MantenimientoLibros(miLibro);
            MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Limpiar()
        {
            txtCodigo.Text = "";
            txtTitulo.Text = "";
            txtAutor.Text = "";
            txtEditorial.Text = "";
            txtPrecio.Text = "";
            txtCantidad.Value = 1;
            dataGridView1.DataSource = _claseNegocio.ListarLibros();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = _claseNegocio.ListarLibros();
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void registrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("El codigo debe estar vacío!", "Mensaje", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            
            if (string.IsNullOrEmpty(txtTitulo.Text) || string.IsNullOrEmpty(txtAutor.Text) ||
                string.IsNullOrEmpty(txtEditorial.Text) || string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Existen campos vacíos!", "Mensaje", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            
            if (MessageBox.Show("¿Deseas añadir '" + txtTitulo.Text + "'?", "Mensaje", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information) != System.Windows.Forms.DialogResult.Yes) return;
            Mantenimiento("1");
            Limpiar();
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("El código no puede estar vacio!", "Mensaje", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("¿Deseas modificar '" + txtTitulo.Text + "'?", "Mensaje", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information) != System.Windows.Forms.DialogResult.Yes) return;
            Mantenimiento("2");
            Limpiar();
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("El código no puede estar vacio", "Mensaje", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("¿Deseas eliminar '" + txtTitulo.Text + "'?", "Mensaje", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information) != System.Windows.Forms.DialogResult.Yes) return;
            Mantenimiento("3");
            Limpiar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtBuscar.Text))
            {
                miLibro.Titulo = txtBuscar.Text;
                var dataTable = new DataTable();
                dataTable = _claseNegocio.BuscarLibro(miLibro);
                dataGridView1.DataSource = dataTable;
            }
            else
            {
                dataGridView1.DataSource = _claseNegocio.ListarLibros();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var fila = dataGridView1.CurrentCell.RowIndex;
            txtCodigo.Text = dataGridView1[0, fila].Value.ToString();
            txtTitulo.Text = dataGridView1[1, fila].Value.ToString();
            txtAutor.Text = dataGridView1[2, fila].Value.ToString();
            txtEditorial.Text = dataGridView1[3, fila].Value.ToString();
            txtPrecio.Text = dataGridView1[4, fila].Value.ToString();
            txtCantidad.Value = decimal.Parse(dataGridView1[5, fila].Value.ToString());
        }

        private void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPrecio.Text)) return;
            if (float.TryParse(txtPrecio.Text, out _)) return;
            MessageBox.Show("El valor ingresado debe ser de tipo numérico (use ',' para denotar decimales)!", "Mensaje", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtPrecio.Text = "";

        }
        
    }
}