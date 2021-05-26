using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Lógica de interacción para LevantarReporte.xaml
    /// </summary>
    public partial class LevantarReporte : Window
    {
        public LevantarReporte()
        {
            InitializeComponent();
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            MenuAgente menuAgente = new MenuAgente();
            this.Close();
            menuAgente.Show();

        }

        private void Guardar_Cick(object sender, RoutedEventArgs e)
        {
            ComprobarCampos();
        }

        private bool ComprobarCampos()
        {
            bool resultado = false;
            if (TbCalle.Text.Trim() != null && TbColonia.Text.Trim() != null && TbDescripcion.Text.Trim() != null)
            {

                resultado = true;
            }
            else
            {
                MessageBox.Show("Ingrese la informacion en los campos de texto");
            }

            return resultado;
        }

        private bool comprobarCalle()
        {
            bool resultado = false;

            return resultado;
        }

        private void SubirImagen_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe");

        }
    }
}
