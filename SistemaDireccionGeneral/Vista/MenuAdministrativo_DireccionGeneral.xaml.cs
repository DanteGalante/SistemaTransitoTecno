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
    /// Lógica de interacción para MenuAdministrativo_DireccionGeneral.xaml
    /// </summary>
    public partial class MenuAdministrativo_DireccionGeneral : Window
    {
        Usuario usuarioIniciado;
        public MenuAdministrativo_DireccionGeneral(Usuario usuarioIniciado)
        {
            InitializeComponent();
            this.usuarioIniciado = usuarioIniciado;
        }

        public MenuAdministrativo_DireccionGeneral()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Manejador del evento del botón "Registrar Usuario"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            RegistrarUsuario_DireccionGeneral registrarUsuario = new RegistrarUsuario_DireccionGeneral();
            registrarUsuario.Show();
            this.Close();
        }

        /// <summary>
        /// Manejador del evento del botón "Registrar Delegación"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegistrarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            RegistrarDelegacionMunicipal_DireccionGeneral registrarDelegacionMunicipal = 
                new RegistrarDelegacionMunicipal_DireccionGeneral();
            registrarDelegacionMunicipal.Show();
            this.Close();
        }

        /// <summary>
        /// Manejador del evento del botón "Consultar Delegación"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConsultarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            ConsultarDelegacionMunicipal_DireccionGeneral consultarDelegacionMunicipal = 
                new ConsultarDelegacionMunicipal_DireccionGeneral();
            consultarDelegacionMunicipal.Show();
            this.Close();
        }

        /// <summary>
        /// Manejador del evento del botón "Consultar Usuario"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConsultarUsuario_Click(object sender, RoutedEventArgs e)
        {
            ConsultarUsuario_DireccionGeneral consultarUsuario = new ConsultarUsuario_DireccionGeneral();
            consultarUsuario.Show();
            this.Close();
        }

        /// <summary>
        /// Manejador del evento del botón "Chat"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChat_Click(object sender, RoutedEventArgs e)
        {
            ChatGeneral ventanaChatGeneral = new ChatGeneral(usuarioIniciado);
            ventanaChatGeneral.ShowDialog();
        }

        /// <summary>
        /// Manejador del evento del botón "Cerrar Sesión"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            IniciarSesion iniciarSesion = new IniciarSesion();
            iniciarSesion.Show();
            this.Close();
        }
    }
}
