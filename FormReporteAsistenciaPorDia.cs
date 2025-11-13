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
    public partial class FormReporteAsistenciaPorDia : Form
    {
        public FormReporteAsistenciaPorDia()
        {
            InitializeComponent();
            this.Load += FormReporteAsistenciaPorDia_Load;

        }
        private List<AsistenciaPorDia> ObtenerAsistenciasPorDia()
        {
            string cadenaConexion = "Server=HP2001\\SQLEXPRESS;Database=congreso2;Trusted_Connection=True;";
            string consulta = @"
        SELECT 
            COUNT(Asistencia.Código_Asistencia) AS Asistencias,
            FORMAT(Exposición.Fecha_Exposición, 'dd-MM-yyyy') AS Fecha_Asistencia
        FROM Asistencia
        JOIN Exposición ON Exposición.Código_Exposición = Asistencia.Código_Exposición
        WHERE Asistencia.Estado_Asistencia = 1
        GROUP BY FORMAT(Exposición.Fecha_Exposición, 'dd-MM-yyyy')";

            var lista = new List<AsistenciaPorDia>();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                foreach (DataRow fila in tabla.Rows)
                {
                    lista.Add(new AsistenciaPorDia
                    {
                        Fecha_Asistencia = fila["Fecha_Asistencia"].ToString(),
                        Asistencias = Convert.ToInt32(fila["Asistencias"])
                    });
                }
            }

            return lista;
        }
        private void FormReporteAsistenciaPorDia_Load(object sender, EventArgs e)
        {
            var datos = ObtenerAsistenciasPorDia();
            ReportDataSource rds = new ReportDataSource("DataSetAsistenciaPorDia", datos);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            reportViewer1.LocalReport.ReportEmbeddedResource = "WindowsFormsApp_congreso_esc.ReporteAsistenciaPorDia.rdlc";
            reportViewer1.RefreshReport();
        }

    }
}
