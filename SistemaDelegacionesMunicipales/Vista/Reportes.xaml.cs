using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SistemaDelegacionesMunicipales.Vista
{
    /// <summary>
    /// Lógica de interacción para Reportes.xaml
    /// </summary>
    public partial class Reportes : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        List<Reporte> listaReportes = new List<Reporte>();
        Reporte reporte = new Reporte();
        Reporte reporteElegido;
        Usuario usuarioIniciado;

        public Reportes(Usuario usuarioIniciado)
        {
            InitializeComponent();
            this.usuarioIniciado = usuarioIniciado;
            this.reporteElegido = reporteElegido;
            LlenarTabla();
        }

        private void LlenarTabla()
        {
            DbSet<Reporte> reporte = entidadesBD.Reportes;

            foreach (var item in reporte)
            {
                listaReportes.Add(item);
            }
            dgReportes.AutoGenerateColumns = false;
            dgReportes.ItemsSource = listaReportes;

        }

        private Reporte RecuperarReporte()
        {
            return reporteElegido = listaReportes.ElementAt<Reporte>(dgReportes.SelectedIndex);
        }

        public Reportes(Reporte reporteElegido)
        {
            this.reporteElegido = reporteElegido;
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            MenuAgente menuAgente = new MenuAgente(usuarioIniciado);

            this.Close();
            menuAgente.Show();
        }

        private void ConsultarReporte_Click(object sender, RoutedEventArgs e)
        {
            if (dgReportes.SelectedItem == null)
            {
                MessageBox.Show("No se ha seleccionado ningun reporte");
            }
            else
            {
                reporteElegido = RecuperarReporte();

                ConsultarReporte consultarReporte = new ConsultarReporte(reporteElegido);
                consultarReporte.ShowDialog();
            }
        }

        private void VerDictamen_Click(object sender, RoutedEventArgs e)
        {
            if (dgReportes.SelectedItem == null)
            {
                MessageBox.Show("No se ha seleccionado ningun reporte");
            }
            else
            {
                reporteElegido = RecuperarReporte();

                if (reporteElegido.estatus == "En proceso")
                {
                    MessageBox.Show("El reporte seleccionado aun esta en proceso");
                }
                else
                {
                    VerDictamenReporte verDictamenReporte = new VerDictamenReporte(reporteElegido);
                    verDictamenReporte.ShowDialog();
                }

            }
        }
    }
}
