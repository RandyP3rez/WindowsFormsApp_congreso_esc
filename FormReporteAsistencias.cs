using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_congreso_esc
{
    public partial class FormReporteAsistencias : Form
    {
        public FormReporteAsistencias()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable tabla = ObtenerDatosDesdeBD();

            ReportDataSource rds = new ReportDataSource("DataSetAsistencias", tabla);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApp_congreso_esc.ReporteAsistencias.rdlc";
            reportViewer1.RefreshReport();
        }

        
        private DataTable ObtenerDatosDesdeBD()
        {
            string cadenaConexion = "Server=HP2001\\SQLEXPRESS;Database=congreso2;Trusted_Connection=True;";
            string consulta = "SELECT NombreAlumno, Curso, Asistencias FROM Asistencias";

            DataTable tabla = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(tabla);
            }

            return tabla;
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    // Crear la tabla en memoria
        //    DataTable tabla = new DataTable();
        //    tabla.Columns.Add("NombreAlumno");
        //    tabla.Columns.Add("Curso");
        //    tabla.Columns.Add("Asistencias", typeof(int));

        //    // Agregar datos simulados
        //    tabla.Rows.Add("Ana López", "Matemáticas", 12);
        //    tabla.Rows.Add("Carlos Pérez", "Historia", 9);
        //    tabla.Rows.Add("María Gómez", "Biología", 15);

        //    // Configurar el ReportViewer

        //    ReportDataSource rds = new ReportDataSource("DataSetAsistencias", tabla);
        //    reportViewer1.LocalReport.DataSources.Clear();
        //    reportViewer1.LocalReport.DataSources.Add(rds);
        //    reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApp_congreso_esc.ReporteAsistencias.rdlc";
        //    reportViewer1.RefreshReport();
        //}


    }
}
