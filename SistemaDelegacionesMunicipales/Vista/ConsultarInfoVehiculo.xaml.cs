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
        BDTransitoEntities bdTransito = new BDTransitoEntities();
        List<Vehiculo> vehiculos = new List<Vehiculo>();

        public ConsultarInfoVehiculo()
        {
            InitializeComponent();
            LlenarTabla();
        }
        
        /**
         * Este metodo se encarga de llenar el datagrid dg_vehiculo con informacion de los vehiculos sacada de la base de datos
         */
        private void LlenarTabla()
        {
            try
            {
                foreach (Vehiculo vehiculoBD in bdTransito.Vehiculos)
                {
                    vehiculos.Add(vehiculoBD);
                }

                dg_vehiculos.ItemsSource = vehiculos;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error en la conexion con la base de datos","Error",MessageBoxButton.OK);
                Console.WriteLine(ex.Message);
            }
        }
        /**
         *  Este metodo cierra la pantalla pantalla actual y vuelve a la anterior
         */
        private void btn_regresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /**
         *  Este metodo abre la pantalla de ModificarRegistroVehiculo. Una vez se cierra la ventana de modificar vehiculos, se regresa a esta pantalla y se actualiza la tabla con los nuevos datos
         */
        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            if (SoloUnVehiculoSeleccionado())
            {
                ModificarRegistroVehiculo nuevaVentana = new ModificarRegistroVehiculo((Vehiculo)dg_vehiculos.SelectedItem);
                nuevaVentana.ShowDialog();
                ActualizarTabla();
            }
        }

        private bool SoloUnVehiculoSeleccionado()
        {
            if (dg_vehiculos.SelectedItems.Count == 1)
            {
                return true;
            }
            else if (dg_vehiculos.SelectedItems.Count > 1)
            {
                MessageBox.Show("Se ha seleccionado mas de un vehiculo para modificar, favor de solo seleccionar uno", "Error", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun vehiculo", "Error", MessageBoxButton.OK);
            }
            
            return false;
        }

        private void btn_registrar_Click(object sender, RoutedEventArgs e)
        {
            RegistroVehiculo nuevaVentana = new RegistroVehiculo();
            nuevaVentana.ShowDialog();
            
            ActualizarTabla();
        }

        private void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dg_vehiculos.SelectedItems.Count > 0)
            {
                List<Vehiculo> vehiculosSeleccionados = new List<Vehiculo>();
                foreach (Vehiculo vehiculoDG in dg_vehiculos.SelectedItems)
                {
                    vehiculosSeleccionados.Add(vehiculoDG);
                }


                MessageBoxResult respuesta;
                if(dg_vehiculos.SelectedItems.Count > 1)
                {
                    respuesta = MessageBox.Show("¿Estas seguro que quieres eliminar los vehiculos seleccionados?","",MessageBoxButton.YesNo);
                }
                else
                {
                    respuesta = MessageBox.Show("¿Estas seguro que quieres eliminar el vehiculo seleccionado?", "", MessageBoxButton.YesNo);
                }

                if (respuesta == MessageBoxResult.Yes)
                {
                    foreach (Vehiculo vehiculoAEliminar in vehiculosSeleccionados)
                    {
                        bdTransito.Vehiculos.Remove(vehiculoAEliminar);
                    }

                    try
                    {
                        bdTransito.SaveChanges();
                    }
                    catch (SqlException ex)
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

            ActualizarTabla();
        }

        private void ActualizarTabla()
        {
            vehiculos.Clear();
            //dg_vehiculos.Items.Refresh();
            bdTransito.Dispose();
            bdTransito = null;
            bdTransito = new BDTransitoEntities();
            dg_vehiculos.ItemsSource = null;
            LlenarTabla();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            bdTransito.Dispose();
        }
    }
}
