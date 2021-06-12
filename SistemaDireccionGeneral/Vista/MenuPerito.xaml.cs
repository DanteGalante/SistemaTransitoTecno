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

namespace SistemaDireccionGeneral.Vista
{
    /// <summary>
    /// Lógica de interacción para MenuPerito.xaml
    /// </summary>
    public partial class MenuPerito : Window
    {
        Usuario usuarioIniciado;

        public MenuPerito(Usuario usuarioIniciado)
        {
            InitializeComponent();
            this.usuarioIniciado = usuarioIniciado;
        }

        /// <summary>
        /// Manejador de eventos del botón "Cerrar Sesión"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            IniciarSesion iniciarSesion = new IniciarSesion();

            this.Close();
            iniciarSesion.Show();
        }

        /// <summary>
        /// Manejador de eventos del botón "Visualizar reportes"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VisualizarReportes_Click(object sender, RoutedEventArgs e)
        {
            VisualizarReportes visualizarReportes = new VisualizarReportes(usuarioIniciado);

            this.Close();
            visualizarReportes.Show();
        }

        /// <summary>
        /// Manejador de eventos del botón "Chat"
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
