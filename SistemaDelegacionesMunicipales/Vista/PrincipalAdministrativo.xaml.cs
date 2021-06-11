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
    /// Interaction logic for PrincipalAdministrativo.xaml
    /// </summary>
    public partial class PrincipalAdministrativo : Window
    {
        Usuario usuarioIniciado;
        public PrincipalAdministrativo(Usuario usuarioIniciado )
        {
            InitializeComponent();
            this.usuarioIniciado = usuarioIniciado;
            lb_titulo.Content = "BIENVENIDO ADMINISTRADOR: " + usuarioIniciado.nombres.ToUpper() + " " + usuarioIniciado.apellidoPaterno.ToUpper() + " " + usuarioIniciado.apellidoMaterno.ToUpper();
        }

        private void btn_agregarVehiculo_Click(object sender, RoutedEventArgs e)
        {
            RegistroVehiculo nuevaVentana = new RegistroVehiculo();
            nuevaVentana.ShowDialog();
        }

        private void btn_agregarConductores_Click(object sender, RoutedEventArgs e)
        {
            RegistrarConductorWindow nuevaVentana = new RegistrarConductorWindow();
            nuevaVentana.ShowDialog();
        }

        private void btn_consultarConductores_Click(object sender, RoutedEventArgs e)
        {
            ConsultarInfoConductorWindow nuevaVentana = new ConsultarInfoConductorWindow();
            nuevaVentana.ShowDialog();
        }

        private void btn_consultarVehiculos_Click(object sender, RoutedEventArgs e)
        {
            ConsultarInfoVehiculo nuevaVentana = new ConsultarInfoVehiculo();
            nuevaVentana.ShowDialog();
        }

        private void btn_chat_Click(object sender, RoutedEventArgs e)
        {
            ChatGeneral nuevaVentana = new ChatGeneral(usuarioIniciado);
            nuevaVentana.ShowDialog();
        }
    }
}
