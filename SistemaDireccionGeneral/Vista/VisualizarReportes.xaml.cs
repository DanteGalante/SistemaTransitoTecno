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

namespace SistemaDireccionGeneral.Vista
{
    /// <summary>
    /// Lógica de interacción para VisualizarReportes.xaml
    /// </summary>
    public partial class VisualizarReportes : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        List<Reporte> listaReportes = new List<Reporte>();
        Reporte reporte = new Reporte();
        Reporte reporteElegido;
        Usuario usuarioIniciado;

        public VisualizarReportes(Usuario usuarioIniciado)
        {
            InitializeComponent();
            this.usuarioIniciado = usuarioIniciado;
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

        public VisualizarReportes(Reporte reporteElegido)
        {
            this.reporteElegido = reporteElegido;
        }

        private void Dictaminar_Click(object sender, RoutedEventArgs e)
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
                    DictaminarReporte dictaminarReporte = new DictaminarReporte(reporteElegido, usuarioIniciado);
                    dictaminarReporte.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("El reporte seleccionado ya ha sido dictaminado");
                }
            }
        }

        private void VerReporte_Click(object sender, RoutedEventArgs e)
        {

            if (dgReportes.SelectedItem == null)
            {
                MessageBox.Show("No se ha seleccionado ningun reporte");
            }
            else
            {
                reporteElegido = RecuperarReporte();
                VerDetalleReporte verDetalleReporte = new VerDetalleReporte(reporteElegido, usuarioIniciado);
                verDetalleReporte.Show();
                this.Close();
            }
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            MenuPerito menuPerito = new MenuPerito(usuarioIniciado);

            this.Close();
            menuPerito.Show();
        }
    }
}

