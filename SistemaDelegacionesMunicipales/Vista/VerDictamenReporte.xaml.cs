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
    /// Lógica de interacción para VerDictamenReporte.xaml
    /// </summary>
    public partial class VerDictamenReporte : Window
    {

        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        Reporte reporteElegido;
        Dictamen dictamenRecuperado;

        public VerDictamenReporte(Reporte reporteElegido)
        {
            InitializeComponent();
            this.reporteElegido = reporteElegido;
            CargarDatosDictamen();
        }


        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            MenuAgente menuAgente = new MenuAgente();

            this.Close();
            menuAgente.Show();
        }

        private void CargarDatosDictamen()
        {
            dictamenRecuperado = reporteElegido.Dictamen;

            LbNombrePerito.Content = dictamenRecuperado.nombreCompletoPerito;
            LbFecha.Content = dictamenRecuperado.fecha;
            LbHora.Content = dictamenRecuperado.hora;
            LbFolio.Content = dictamenRecuperado.folio;
            TBDescripcion.Text = dictamenRecuperado.descripcion;
        }

    }
}
