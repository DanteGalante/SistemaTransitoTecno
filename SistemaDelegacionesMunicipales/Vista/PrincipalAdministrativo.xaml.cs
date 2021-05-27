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
        public PrincipalAdministrativo()
        {
            InitializeComponent();
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
    }
}
