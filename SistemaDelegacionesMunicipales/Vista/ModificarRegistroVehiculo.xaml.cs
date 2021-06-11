using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
    /// Autor: Dan Javier Olvera Villeda
    /// Interaction logic for ModificarRegistroVehiculo.xaml
    /// </summary>
    public partial class ModificarRegistroVehiculo : Window
    {
        BDTransitoEntities bdTransitoEntities = new BDTransitoEntities();
        Vehiculo vehiculoAModificar;
        List<Conductor> conductores = new List<Conductor>();

        public ModificarRegistroVehiculo(Vehiculo vehiculoAModificar)
        {
            InitializeComponent();
            CargarConductores();
            CargarDatosVehiculoMod(vehiculoAModificar);
        }

        /// <summary>
        /// Muestra la informacion del vehiculo seleccionado
        /// en los campos de texto para modificar su registro
        /// </summary>
        /// <param name="vehiculoMod"></param>
        private void CargarDatosVehiculoMod(Vehiculo vehiculoMod)
        {
            this.vehiculoAModificar = bdTransitoEntities.Vehiculos.SingleOrDefault(vehiculo =>
            vehiculo.idVehiculo == vehiculoMod.idVehiculo);

            tb_numeroPlacas.Text = vehiculoAModificar.numeroPlaca;
            tb_nombreAseguradora.Text = vehiculoAModificar.nombreAseguradora;
            tb_numeroPolizaSeguro.Text = vehiculoAModificar.numeroPolizaSeguro;
            tb_color.Text = vehiculoAModificar.color;
            tb_anio.Text = vehiculoAModificar.año.ToString();
            tb_modelo.Text = vehiculoAModificar.modelo;
            tb_marca.Text = vehiculoAModificar.marca;
            cb_conductor.SelectedItem = vehiculoAModificar.Conductor.nombres + " " +
                                        vehiculoAModificar.Conductor.apellidoPaterno + " " +
                                        vehiculoAModificar.Conductor.apellidoMaterno;
        }


        /// <summary>
        /// Llama las validaciones para modificar la informacion
        /// del registro del vehiculo seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarInformacion())
            {
                ModificarVehiculo();
            }
        }

        /// <summary>
        /// Controla la modificacion de la informacion del
        /// vehiculo seleccionado
        /// </summary>
        private void ModificarVehiculo()
        {
            try
            {
                vehiculoAModificar.numeroPolizaSeguro = tb_numeroPolizaSeguro.Text;
                vehiculoAModificar.nombreAseguradora = tb_nombreAseguradora.Text;
                vehiculoAModificar.color = tb_color.Text;
                vehiculoAModificar.año = Int32.Parse(tb_anio.Text);
                vehiculoAModificar.modelo = tb_modelo.Text;
                vehiculoAModificar.marca = tb_marca.Text;
                vehiculoAModificar.numeroPlaca = tb_numeroPlacas.Text;
                string seleccionComboBox = cb_conductor.Text;

                //Si en el combobox decide darle a la opción otro, tiene la posibilidad de crear un conductor desde aqui
                if (("Otro conductor").Equals(seleccionComboBox))
                {
                    MessageBoxResult respuesta = MessageBox.Show("Quieres crear un nuevo conductor?", "", MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        RegistrarConductorWindow nuevaVentana = new RegistrarConductorWindow();
                        bool conductorRegistrado = (bool)nuevaVentana.ShowDialog();
                        actualizarConductores();
                        if (conductorRegistrado)
                        {
                            cb_conductor.SelectedIndex = cb_conductor.Items.Count - 1;
                        }
                        else
                        {
                            cb_conductor.SelectedIndex = 0;
                        }
                    }
                }
                else
                {
                    vehiculoAModificar.Conductor = RecuperarConductor();

                    if (VehiculoRepetido(vehiculoAModificar) == false)
                    {
                        bdTransitoEntities.SaveChanges();
                        MessageBox.Show("Vehiculo modificado exitosamente");
                    }
                    else
                    {
                        MessageBox.Show("ERROR: El vehiculo ya esta registrado en el sistema");
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error en la conexion con la base de datos", "Error", MessageBoxButton.OK);
            }
        } 
        
        /// <summary>
        /// Recupera un conductor dado una seleccion en el combobox
        /// </summary>
        /// <returns></returns>
        private Conductor RecuperarConductor()
        {
            Conductor conductorRecuperado = null;

            if (cb_conductor.SelectedItem != null)
            {
                conductorRecuperado = conductores.ElementAt(cb_conductor.SelectedIndex - 1);
            }

            return conductorRecuperado;
        }

        /// <summary>
        ///Busca en la BD la existencia de otra entidad con los mismos atributos
        /// </summary>
        /// <param name="vehiculoModificado"></param>
        /// <returns></returns>
        private bool VehiculoRepetido(Vehiculo vehiculoModificado)
        {
            bool vehiculoRepetido = false;

            if (bdTransitoEntities.Vehiculos.SingleOrDefault(
                    vehiculo =>
                    vehiculo.año == vehiculoModificado.año &&
                    vehiculo.color == vehiculoModificado.color &&
                    vehiculo.marca == vehiculoModificado.marca &&
                    vehiculo.modelo == vehiculoModificado.modelo &&
                    vehiculo.nombreAseguradora == vehiculoModificado.nombreAseguradora &&
                    vehiculo.numeroPlaca == vehiculoModificado.numeroPlaca &&
                    vehiculo.numeroPolizaSeguro == vehiculoModificado.numeroPolizaSeguro &&
                    vehiculo.Conductor.idConductor == vehiculoModificado.Conductor.idConductor) != null)
            {
                vehiculoRepetido = true;
            }
            return vehiculoRepetido;
        }

        /// <summary>
        /// Carga los conductores que exiten en la BD y los muestra en el combobox
        /// </summary>
        private void CargarConductores()
        {
            DbSet<Conductor> conductoresDBSet = bdTransitoEntities.Conductores;

            cb_conductor.Items.Add("Otro conductor");

            if (conductoresDBSet.Count() > 0)
            {
                foreach (Conductor conductor in conductoresDBSet)
                {
                    cb_conductor.Items.Add(conductor.nombres + " " + conductor.apellidoPaterno + " " + conductor.apellidoMaterno);
                    conductores.Add(conductor);
                }
            }
        }

        /// <summary>
        /// Valida que la informacion elegida sea valida para
        /// modificar el registro del conductor
        /// </summary>
        /// <returns></returns>
        private bool ValidarInformacion()
        {
            bool valido = false;

            if (!CamposTextoVacios() && ConductorElegido() && ValidezInfoCamposTexto())
            {
                valido = true;
            }

            return valido;
        }


        /// <summary>
        /// Verifica que no se introduzcan datos incorrectos en los campos de texto
        /// Por ejemplo, un numero donde solo se permiten letras, o viceversa
        /// </summary>
        /// <returns></returns>
        private bool ValidezInfoCamposTexto()
        {
            bool valida = true;

            try
            {
                Int32.Parse(tb_anio.Text);
                if (tb_numeroPolizaSeguro.Text.Length > 0)
                {
                    Int32.Parse(tb_numeroPolizaSeguro.Text); 
                }
            }
            catch (Exception)
            {
                valida = false;
                MessageBox.Show("Campos hay un campo de texto invalido");
            }

            return valida;
        }

        /// <summary>
        /// Comprueba que hay una seleccion de conductor
        /// </summary>
        /// <returns></returns>
        private bool ConductorElegido()
        {
            bool conductorElegido = false;

            if (cb_conductor.SelectedItem != null)
            {
                conductorElegido = true;
            }
            else
            {
                MessageBox.Show("No hay conductor elegido");
            }

            return conductorElegido;
        }

        /// <summary>
        /// Verifica que los campos de texto de la ventana no esten vacios
        /// </summary>
        /// <returns></returns>
        private bool CamposTextoVacios()
        {
            bool camposTextoVacio = true;
            
            if (tb_anio.Text.Length > 0 &&
                tb_color.Text.Length > 0 &&
                tb_marca.Text.Length > 0 &&
                tb_modelo.Text.Length > 0 &&
                tb_numeroPlacas.Text.Length > 0
                )
            {
                camposTextoVacio = false;
            }
            else
            {
                MessageBox.Show("Hay campos de texto vacios");
            }            

            return camposTextoVacio;
        }

        /// <summary>
        /// Controla el detenimiento de la ejecucion del programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Actualiza los conductores que estan registrados en el combobox de seleccion
        /// </summary>
        private void actualizarConductores()
        {
            conductores.Clear();
            cb_conductor.Items.Clear();
            CargarConductores();
        }
    }
}
