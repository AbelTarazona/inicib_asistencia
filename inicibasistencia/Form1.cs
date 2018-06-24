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
    public partial class H : Form
    {

        OleDbConnection con = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;" +
            @"Data Source=|DataDirectory|\\asistenciainicib.mdb;" +
            @"Jet OLEDB:Database Password=; Persist Security Info=False;");

        public H()
        {
            InitializeComponent();
            timer1.Enabled = true;
            lblprueba.Hide();
            labelprueba2.Hide();
        }

        //Fecha
        String fechita = DateTime.Now.ToString("MM/dd/yyyy");


        private void btnEntrada_Click(object sender, EventArgs e)
        {
            con.Open();
            String horita = DateTime.Now.ToString("HH:mm");
            try
            {

                //------Mostrar nombre de alumno--------
                OleDbCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "SELECT nombre from Alumno where CodAlumno =" + "(" + txtCodigo.Text + ")";
                OleDbDataReader alumno = cmd2.ExecuteReader();
                alumno.Read();
                //---------Ver si marco entrada por alumno y fecha--------
                OleDbCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "SELECT Count(horaEntrada) AS horaEntrada FROM Asistencia where codAlumno= (" + txtCodigo.Text + ") AND fecha=" + "(#" + fechita + "#)";
                OleDbDataReader he = cmd3.ExecuteReader();
                he.Read();
                lblprueba.Text = he.GetValue(0).ToString();

                if (lblprueba.Text == "0")
                {
                    OleDbCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO Asistencia values(" + "(" + txtCodigo.Text + ")," + "(#" + fechita + "#)," + "(#" + horita + "#)," + "null )";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(alumno.GetValue(0).ToString() + "\n\nHora de entrada: " + lblHora.Text);
                    txtCodigo.Text = "";
                }
                else
                {
                    MessageBox.Show("Ya marcó entrada el día de hoy.");
                }
            }
            catch
            {
                MessageBox.Show("Alumno o Asesor no existente ");
                txtCodigo.Text = " ";
            }

            con.Close();


        }

        private void btnSalida_Click(object sender, EventArgs e)
        {
            con.Open();
            String horita = DateTime.Now.ToString("HH:mm");

            try
            {

                //------------------------------------------
                OleDbCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "SELECT nombre from Alumno where CodAlumno =" + "(" + txtCodigo.Text + ")";
                OleDbDataReader alumno = cmd2.ExecuteReader();
                alumno.Read();

                //---------Ver si marco entrada por alumno y fecha--------
                OleDbCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "SELECT Count(horaEntrada) AS horaEntrada FROM Asistencia where codAlumno= (" + txtCodigo.Text + ") AND fecha=" + "(#" + fechita + "#)";
                OleDbDataReader he = cmd3.ExecuteReader();
                he.Read();
                lblprueba.Text = he.GetValue(0).ToString();
                //-------------Ver si marco salida por alumno y fecha------------
                OleDbCommand cmd4 = con.CreateCommand();
                cmd4.CommandType = CommandType.Text;
                cmd4.CommandText = "SELECT Count(horaSalida) AS horaSalida FROM Asistencia where codAlumno= (" + txtCodigo.Text + ") AND fecha=" + "(#" + fechita + "#)";
                OleDbDataReader hs = cmd4.ExecuteReader();
                hs.Read();
                labelprueba2.Text = hs.GetValue(0).ToString();


                if (lblprueba.Text != "0" && labelprueba2.Text != "0")
                {
                    MessageBox.Show("Ya registró asistencia el día de hoy.");
                }
                else
                {
                    if (lblprueba.Text == "0")
                    {
                        MessageBox.Show("Debe marcar entrada primero.");
                    }
                    else
                    {
                        OleDbCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "UPDATE Asistencia set horaSalida=" + "(#" + horita + "#) where codAlumno= (" + txtCodigo.Text + ") AND fecha=" + "(#" + fechita + "#)";
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(alumno.GetValue(0).ToString() + "\n\nHora de salida: " + lblHora.Text);
                        txtCodigo.Text = "";
                    }
                }
            }
            catch
            {
                MessageBox.Show("Alumno o Asesor no existente");
                txtCodigo.Text = " ";
            }

            con.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime tt = DateTime.Now;
            lblHora.Text = tt.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("En caso de ayuda llamar a: 970080498\nAbel Tarazona");
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            admin forma = new admin();
            forma.Show();
        }
    }
}
