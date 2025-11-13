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
    public partial class FormMenuPrincipal : Form
    {
        public FormMenuPrincipal()
        {
            InitializeComponent();
        }

       

        

        private void FormMenuPrincipal_Load(object sender, EventArgs e)
        {

            
        }
        private void btnReporteSemestreJornada_Click(object sender, EventArgs e)
        {
            var form = new FormReporteSemestreJornada();
            form.Show();
        }

        private void btnReporteAsistencias_Click(object sender, EventArgs e)
        {
            var form = new FormReporteAsistencias();
            form.Show();
        }

        private void btnReporteDia_Click(object sender, EventArgs e)
        {
             var form = new FormReporteAsistenciaPorDia();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var form = new FormReporteAsistenciaDetallada();
            form.Show();
        }
    }
}
