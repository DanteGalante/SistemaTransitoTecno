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
    /// Lógica de interacción para ChatGeneral.xaml
    /// </summary>
    public partial class ChatGeneral : Window
    {
        public ChatGeneral()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            MenuAdministrativo_DireccionGeneral ventanaMenuAdministrativo = new MenuAdministrativo_DireccionGeneral();
            ventanaMenuAdministrativo.Show();
            this.Close();
        }
    }
}
