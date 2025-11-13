using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_congreso_esc
{
    public partial class FormReporteSemestreJornada : Form
    {
        public FormReporteSemestreJornada()
        {
            InitializeComponent();
            this.Load += FormReporteSemestreJornada_Load;

        }

        private void FormReporteSemestreJornada_Load(object sender, EventArgs e)
        {
            List<AlumnoSemestreJornada> datos = ObtenerDatosComoLista();

            ReportDataSource rds = new ReportDataSource("DataSetSemestreJornada", datos);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApp_congreso_esc.ReporteSemestreJornada.rdlc";
            reportViewer1.RefreshReport();
            
        }

        private DataTable ObtenerAlumnosPorSemestreYJornada()
        {
            string cadenaConexion = "Server=HP2001\\SQLEXPRESS;Database=congreso2;Trusted_Connection=True;";
            string consulta = @"
    SELECT DISTINCT
        Persona.Carnet_Persona AS Carne,
        Persona.Nombres_Persona AS Nombres,
        Persona.Apellidos_Persona AS Apellidos,
        Semestre.Nombre_Semestre AS Semestre,
        Jornada.Nombre_Jornada AS Jornada
    FROM Persona
    LEFT JOIN Carrera ON Carrera.Código_Carrera = Persona.Código_Carrera
    LEFT JOIN Pensum ON Pensum.Código_Carrera = Carrera.Código_Carrera
    LEFT JOIN Semestre ON Semestre.Código_Semestre = Pensum.Código_Semestre
    LEFT JOIN Jornada ON Jornada.Código_Jornada = Pensum.Código_Jornada";
            DataTable tabla = new DataTable();

            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                adaptador.Fill(tabla);
            }

            return tabla;
        }

        private List<AlumnoSemestreJornada> ObtenerDatosComoLista()
        {
            var lista = new List<AlumnoSemestreJornada>();
            var tabla = ObtenerAlumnosPorSemestreYJornada();

            foreach (DataRow fila in tabla.Rows)
            {
                lista.Add(new AlumnoSemestreJornada
                {
                    Carne = fila["Carne"].ToString(),
                    Nombres = fila["Nombres"].ToString(),
                    Apellidos = fila["Apellidos"].ToString(),
                    Semestre = fila["Semestre"].ToString(),
                    Jornada = fila["Jornada"].ToString()
                });
            }

            return lista;
        }

        private void FormReporteSemestreJornada_Load_1(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();

            
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }

   
}

   