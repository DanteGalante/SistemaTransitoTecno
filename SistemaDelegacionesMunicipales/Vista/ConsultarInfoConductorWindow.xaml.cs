using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
    /// Interaction logic for ConsultarInfoConductor.xaml
    /// </summary>
    public partial class ConsultarInfoConductorWindow : Window
    {
        BDTransitoEntities bdTransito = new BDTransitoEntities();
        List<Conductor> conductores = new List<Conductor>();
        public ConsultarInfoConductorWindow()
        {
            InitializeComponent();
            LlenarTabla();
        }
        
        /**
         * Llenar el datagrid dg_conductor con informacion de los conductores sacada de la base de datos
         */
        private void LlenarTabla()
        {
            try
            {
                foreach (Conductor conductorBD in bdTransito.Conductores)
                {
                    conductores.Add(conductorBD);
                }

                dg_conductor.ItemsSource = conductores;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error en la conexion con la base de datos", "Error", MessageBoxButton.OK);
                Console.WriteLine(ex.Message);
            }
        }

        /**
         * Se actualiza el datagrid de la ventana. Para esto reinicia el proceso de llenado de tablas, borrando el contenido tabla y volviendolo con una mejor
         */
        private void ActualizarTabla()
        {
            conductores.Clear();
            bdTransito.Dispose();
            bdTransito = null;
            bdTransito = new BDTransitoEntities();
            dg_conductor.ItemsSource = null;
            LlenarTabla();
        }

        /**
         * Cierra la pantalla actual y vuelve a la anterior
         */
        private void btn_regresar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /**
         *  Abre la pantalla de ModificarConductor. Una vez se cierra la ventana de modificar conductor, se regresa a esta pantalla y se actualiza la tabla con los nuevos datos
         */
        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            if (SoloUnConductorSeleccionado())
            {
                ModificarConductor nuevaVentana = new ModificarConductor((Conductor)dg_conductor.SelectedItem);
                nuevaVentana.ShowDialog();
                ActualizarTabla();
            }
        }

        /**
         *  Verifica que solo se haya seleccionado un conductor en el datagrid
         */
        private bool SoloUnConductorSeleccionado()
        {
            if (dg_conductor.SelectedItems.Count == 1)
            {
                return true;
            }
            else if (dg_conductor.SelectedItems.Count > 1)
            {
                MessageBox.Show("Se ha seleccionado mas de un conductor para modificar, favor de solo seleccionar uno", "Error", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("No se ha seleccionado ningun conductor", "Error", MessageBoxButton.OK);
            }

            return false;
        }

        /**
         * Abre la ventana RegistroConductorWindow. Una vez se cierra ventana de agregar conductores, se regresa a esta pantalla y se actualiza la tabla
         */
        private void btn_registrar_Click(object sender, RoutedEventArgs e)
        {
            RegistrarConductorWindow nuevaVentana = new RegistrarConductorWindow();
            nuevaVentana.ShowDialog();
            ActualizarTabla();
        }

        /**
          *  Elimina el o los conductores seleccionados en el datagrid previo a darle clic al boton btn_eliminar
          */
        private void btn_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dg_conductor.SelectedItems.Count > 0)
            {
                List<Conductor> conductoresSeleccionados = new List<Conductor>();
                foreach (Conductor conductorDG in dg_conductor.SelectedItems)
                {
                    conductoresSeleccionados.Add(conductorDG);
                }


                MessageBoxResult respuesta;
                if (dg_conductor.SelectedItems.Count > 1)
                {
                    respuesta = MessageBox.Show("¿Estas seguro que quieres eliminar los conductores seleccionados?", "", MessageBoxButton.YesNo);
                }
                else
                {
                    respuesta = MessageBox.Show("¿Estas seguro que quieres eliminar el conductor seleccionado?", "", MessageBoxButton.YesNo);
                }

                if (respuesta == MessageBoxResult.Yes)
                {
                    foreach (Conductor conductorAEliminar in conductoresSeleccionados)
                    {   
                        /* Si encuentra que el conductor que quiere eliminar tiene una relacion con uno o mas vehiculos, entonces tiene que eliminar todos los vehiculos relacionados a el, o no borrarlo*/
                        if(conductorAEliminar.Vehiculos.Count > 0)
                        {
                            MessageBoxResult respuestaElimVehiculos = MessageBox.Show("No se puede eliminar el conductor " + conductorAEliminar.nombres + " " + conductorAEliminar.apellidoPaterno + " " + conductorAEliminar.apellidoMaterno +
                                                                         "debido a que tiene vehiculos asignados. ¿Deseas eliminar todos los vehiculos de este conductor tambien?", "Advertencia: No se puede eliminar a un conductor con vehiculos", 
                                                                         MessageBoxButton.YesNo);
                            if(respuestaElimVehiculos == MessageBoxResult.Yes)
                            {
                                EliminarVehiculosConductor(conductorAEliminar);
                                
                                bdTransito.Conductores.Remove(conductorAEliminar);
                            }
                        }
                        else
                        {
                            bdTransito.Conductores.Remove(conductorAEliminar);
                        }
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
                MessageBox.Show("No se ha seleccionado ningún conductor para eliminar", "Error", MessageBoxButton.OK);
            }

            ActualizarTabla();
        }

        /**
         * Elimina los vehiculos de un conductor especifico
         */
        private void EliminarVehiculosConductor(Conductor conductorRef)
        {
            List<Vehiculo> vehiculosConduct = conductorRef.Vehiculos.ToList();
            foreach (Vehiculo vehiculo in vehiculosConduct)
            {
                bdTransito.Vehiculos.Remove(vehiculo);
            }
            try
            {
                bdTransito.SaveChanges();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error en la conexion con la base de datos", "Error", MessageBoxButton.OK);
            }
        }
    }
}
