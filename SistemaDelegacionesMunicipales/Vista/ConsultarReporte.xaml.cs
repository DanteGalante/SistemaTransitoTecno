using BaseDeDatos;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para ConsultarReporte.xaml
    /// </summary>
    public partial class ConsultarReporte : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        Reporte reporteElegido;

        public ConsultarReporte(Reporte reporteElegido)
        {
            InitializeComponent();
            this.reporteElegido = reporteElegido;
            CargarDatosReporte();
        }

        private void CargarDatosReporte()
        {
            LbCalle.Content = reporteElegido.calle;
            LbColonia.Content = reporteElegido.colonia;
            LbFecha.Content = reporteElegido.fecha;
            LbEstatus.Content = reporteElegido.estatus;
            TBDescripcion.Text = reporteElegido.descripcion;
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            Reportes reportes = new Reportes();

            this.Close();
            reportes.Show();
        }

        private void VerDictamen_Click(object sender, RoutedEventArgs e)
        {
            VerDictamenReporte dictamenReporte = new VerDictamenReporte();

            this.Close();
            dictamenReporte.Show();
        }
    }
}
