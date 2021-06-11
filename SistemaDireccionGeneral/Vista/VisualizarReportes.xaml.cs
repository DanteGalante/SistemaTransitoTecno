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
    /// Autor: Alan Adair Morgado Morales
    /// Lógica de interacción para VisualizarReportes.xaml
    /// </summary>
    public partial class VisualizarReportes : Window
    {
        private BDTransitoEntities entidadesBD = new BDTransitoEntities();
        private List<Reporte> listaReportes = new List<Reporte>();
        private Reporte reporte = new Reporte();
        private Reporte reporteElegido;
        private Usuario usuarioIniciado;

        public VisualizarReportes(Usuario usuarioIniciado)
        {
            InitializeComponent();
            this.usuarioIniciado = usuarioIniciado;
            LlenarTabla();
        }

        /// <summary>
        /// Llena el datagrid de los reporte obtenidos de la base de datos
        /// </summary>
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
        /// <summary>
        /// Recupera un reporte de la lista de reportes, obtenida de la base de datos
        /// </summary>
        /// <returns>
        /// Reporte recuperado 
        /// </returns>
        private Reporte RecuperarReporte()
        {
            return reporteElegido = listaReportes.ElementAt<Reporte>(dgReportes.SelectedIndex);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reporteElegido"></param>
        public VisualizarReportes(Reporte reporteElegido)
        {
            this.reporteElegido = reporteElegido;
        }
        /// <summary>
        /// Maneja el evento de un clic sobre el boton "Dictaminar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Maneja el evento de un clic sobre el boton "VerReporte"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Maneja el evento de un clic sobre el boton "Regresar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            MenuPerito menuPerito = new MenuPerito(usuarioIniciado);

            this.Close();
            menuPerito.Show();
        }
    }
}

