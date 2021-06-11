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
    /// Autor: Alan Adair Morgado Morales
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class MenuAgente : Window
    {
        Usuario usuarioIniciado;
        public MenuAgente(Usuario usuarioIniciado)
        {
            InitializeComponent();
            this.usuarioIniciado = usuarioIniciado;
        }

        public MenuAgente()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Controlador para cambiar a la pantalla "LevantarReporte.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            LevantarReporte levantarReporte = new LevantarReporte(usuarioIniciado);
            this.Close();
            levantarReporte.ShowDialog();

        }

        /// <summary>
        /// Conrolador para cambiar a la pantalla Reportes.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HistorialReportes_Click(object sender, RoutedEventArgs e)
        {
            Reportes reportes = new Reportes(usuarioIniciado);

            this.Close();
            reportes.ShowDialog();
        }

        /// <summary>
        /// Conntrolador para mostrar el IniciarSesion.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            IniciarSesion iniciarSesion = new IniciarSesion();
            iniciarSesion.Show();
            this.Close();
        }

        /// <summary>
        /// Muestra la ventana del ChatGneral.xaml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ChatGeneral nuevaVentana = new ChatGeneral(usuarioIniciado);
            nuevaVentana.ShowDialog();
        }
    }
}
