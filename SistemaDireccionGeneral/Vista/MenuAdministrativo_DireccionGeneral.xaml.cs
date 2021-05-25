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
        public MenuAdministrativo_DireccionGeneral()
        {
            InitializeComponent();
        }

        private void btnRegistrarUsuario_Click(object sender, RoutedEventArgs e)
        {
            RegistrarUsuario_DireccionGeneral registrarUsuario = new RegistrarUsuario_DireccionGeneral();
            registrarUsuario.Show();
            this.Close();
        }

        private void btnRegistrarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            RegistrarDelegacionMunicipal_DireccionGeneral registrarDelegacionMunicipal = new RegistrarDelegacionMunicipal_DireccionGeneral();
            registrarDelegacionMunicipal.Show();
            this.Close();
        }

        private void btnConsultarDelegacion_Click(object sender, RoutedEventArgs e)
        {
            ConsultarDelegacionMunicipal_DireccionGeneral consultarDelegacionMunicipal = new ConsultarDelegacionMunicipal_DireccionGeneral();
            consultarDelegacionMunicipal.Show();
            this.Close();
        }
    }
}
