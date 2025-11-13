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
    public partial class FormReporteAsistenciaDetallada : Form
    {
        public FormReporteAsistenciaDetallada()
        {
            InitializeComponent();
        }
        private List<AlumnoAsistenciaDetallada> ObtenerAsistenciaDetallada()
        {
            string cadenaConexion = "Server=HP2001\\SQLEXPRESS;Database=congreso2;Trusted_Connection=True;";
            string consulta = @"
        SELECT
            Persona.Carnet_Persona AS Carne,
            Persona.Nombres_Persona AS Nombres,
            Persona.Apellidos_Persona AS Apellidos,
            Carrera.Nombre_Carrera AS Carrera,
            Exposición.Fecha_Exposición AS Fecha
        FROM Persona
        JOIN Asistencia ON Asistencia.Código_Persona = Persona.Código_Persona
        JOIN Carrera ON Carrera.Código_Carrera = Persona.Código_Carrera
        JOIN Exposición ON Exposición.Código_Exposición = Asistencia.Código_Exposición
        WHERE Asistencia.Estado_Asistencia = 1
        ORDER BY Exposición.Fecha_Exposición";

            var lista = new List<AlumnoAsistenciaDetallada>();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                foreach (DataRow fila in tabla.Rows)
                {
                    lista.Add(new AlumnoAsistenciaDetallada
                    {
                        Carne = fila["Carne"].ToString(),
                        Nombres = fila["Nombres"].ToString(),
                        Apellidos = fila["Apellidos"].ToString(),
                        Carrera = fila["Carrera"].ToString(),
                        Fecha = Convert.ToDateTime(fila["Fecha"])
                    });
                }
            }

            return lista;
        }
        private void FormReporteAsistenciaDetallada_Load(object sender, EventArgs e)
        {
            var datos = ObtenerAsistenciaDetallada();
            ReportDataSource rds = new ReportDataSource("DataSetAsistenciaDetallada", datos);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApp_congreso_esc.ReporteAsistenciaDetallada.rdlc";
            reportViewer1.RefreshReport();
        }
    }
}
