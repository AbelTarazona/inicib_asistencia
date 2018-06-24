using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace inicibasistencia
{
    public partial class admin : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;" +
    @"Data Source=|DataDirectory|\\asistenciainicib.mdb;" +
    @"Jet OLEDB:Database Password=; Persist Security Info=False;");

        public admin()
        {
            InitializeComponent();
            panel2.Hide();
            panelAdmin.Show();
            timer1.Enabled = true;
        }

        //Fecha
        String fechita = DateTime.Now.ToString("dd/MM/yyyy");

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtContra.Text == "inicib2017mayo")
            {
                panelAdmin.Hide();
                panel2.Show();
                txtContra.Text = " ";

            }
            else
            {
                MessageBox.Show("Contraseña incorrecta");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime tt = DateTime.Now;
            lblhora.Text = tt.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            con.Open();
            try
            {
                OleDbCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE * FROM Asistencia WHERE CodAlumno= (" + txtCodigo.Text + ") AND fecha=" + "(#" + fechita + "#)";
                cmd.ExecuteNonQuery();



                //------Mostrar nombre de alumno--------
                OleDbCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "SELECT nombre from Alumno where CodAlumno =" + "(" + txtCodigo.Text + ")";
                OleDbDataReader alumno = cmd2.ExecuteReader();
                alumno.Read();
                MessageBox.Show(alumno.GetValue(0).ToString() + " eliminación de registro de asistencia exitosa");



            }
            catch
            {
                MessageBox.Show("Alumno no existente o registro de asistencia aún no realizado");
                txtCodigo.Text = " ";
            }

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtCodigo.Text = " ";
            panel2.Hide();
            panelAdmin.Show();
        }
    }
}
