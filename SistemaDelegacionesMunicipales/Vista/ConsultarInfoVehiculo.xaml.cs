using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for ConsultarInfoVehiculo.xaml
    /// </summary>
    public partial class ConsultarInfoVehiculo : Window
    {
        List<Vehiculo> vehiculos = new List<Vehiculo>();

        public ConsultarInfoVehiculo()
        {
            InitializeComponent();
            LlenarTabla();
        }

        private void LlenarTabla()
        {
            using (BDTransitoEntities bdTransito = new BDTransitoEntities())
            {
                try
                {
                    foreach (Vehiculo vehiculoBD in bdTransito.Vehiculos)
                    {
                        vehiculos.Add(vehiculoBD);
                    }

                    dg_vehiculos.ItemsSource = vehiculos;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en la conexion con la base de datos","Error",MessageBoxButton.OK);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void btn_regresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            ModificarRegistroVehiculo nuevaVentana = new ModificarRegistroVehiculo();
            nuevaVentana.ShowDialog();
        }

        private void btn_registrar_Click(object sender, RoutedEventArgs e)
        {
            RegistroVehiculo nuevaVentana = new RegistroVehiculo();
            nuevaVentana.ShowDialog();
        }

        private void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if(dg_vehiculos.SelectedItems.Count > 0)
            {
                List<Vehiculo> vehiculosSeleccionados = new List<Vehiculo>();
                foreach (Vehiculo vehiculoDG in dg_vehiculos.SelectedItems)
                {
                    vehiculosSeleccionados.Add(vehiculoDG);
                }
                using (BDTransitoEntities bDTransito = new BDTransitoEntities())
                {
                    foreach (Vehiculo vehiculoAEliminar in vehiculosSeleccionados)
                    {
                        bDTransito.Vehiculos.Remove(vehiculoAEliminar);
                    }
                    try
                    {
                        bDTransito.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error en la conexion con la base de datos", "Error", MessageBoxButton.OK);
                        Console.WriteLine(ex.StackTrace);
                    }
                }
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningún vehiculo para eliminar", "Error", MessageBoxButton.OK);
            }
        }
    }
}
