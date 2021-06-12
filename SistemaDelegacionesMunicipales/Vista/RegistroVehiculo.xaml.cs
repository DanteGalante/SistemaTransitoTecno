using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
    /// Autor: Dan Javier Olvera Villleda
    /// Lógica de interacción para RegistrarVehiculo.xaml
    /// </summary>
    public partial class RegistroVehiculo : Window
    {
        private BDTransitoEntities entidadesBD = new BDTransitoEntities();
        private List<Conductor> conductores = new List<Conductor>();
        Label lb_numPolizaSeguro = new Label();
        TextBox tb_numPolizaSeguro = new TextBox();
        Label lb_nombreAseguradora = new Label();
        TextBox tb_nombreAseguradora = new TextBox();

        public RegistroVehiculo()
        {
            InitializeComponent();
            CargarConductores();
        }

        /// <summary>
        /// Carga los conductores del base de datos
        /// en un comboBox
        /// </summary>
        private void CargarConductores()
        {
            DbSet<Conductor> conductoresDBSet = entidadesBD.Conductores;
            cb_conductor.Items.Add("Otro conductor");

            if (conductoresDBSet.Count() > 0)
            {
                foreach (Conductor conductor in conductoresDBSet)
                {

                    cb_conductor.Items.Add(conductor.nombres + " " +
                                           conductor.apellidoPaterno + " " +
                                           conductor.apellidoMaterno);
                    conductores.Add(conductor);
                }
            }
        }

        /// <summary>
        /// Gestiona el registro de un vehiculo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_registrar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarInformacion())
            {
                RegistrarVehiculo();
            }
        }
        /// <summary>
        /// Guarda la informacion de nuevo vehiculo 
        /// </summary>
        private void RegistrarVehiculo()
        {
            try
            {
                Vehiculo nuevoVehiculo = new Vehiculo();

                nuevoVehiculo.numeroPolizaSeguro = tb_numPolizaSeguro.Text;
                nuevoVehiculo.nombreAseguradora = tb_nombreAseguradora.Text;
                nuevoVehiculo.color = tb_color.Text;
                nuevoVehiculo.año = Int32.Parse(tb_anio.Text);
                nuevoVehiculo.modelo = tb_modelo.Text;
                nuevoVehiculo.marca = tb_marca.Text;
                nuevoVehiculo.numeroPlaca = tb_numeroPlacas.Text;
                string seleccionComboBox = cb_conductor.Text;

                if ((seleccionComboBox).Equals("Otro conductor"))
                {
                    MessageBoxResult respuesta = MessageBox.Show("Quieres crear un nuevo conductor?",
                        "",MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        RegistrarConductorWindow nuevaVentana = new RegistrarConductorWindow();
                        bool conductorRegistrado = (bool)nuevaVentana.ShowDialog();
                        ActualizarConductores();
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
                    nuevoVehiculo.Conductor = RecuperarConductor();

                    if (VehiculoRepetido(nuevoVehiculo) == false)
                    {
                        entidadesBD.Vehiculos.Add(nuevoVehiculo);
                        entidadesBD.SaveChanges();
                        MessageBox.Show("Vehiculo registrado exitosamente");
                        LimpiarVentana();
                    }
                    else
                    {
                        MessageBox.Show("ERROR: El vehiculo ya esta registrado en el sistema");
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidad \"{0}\" Estado \"{1}\" ",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Propiedad: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }

        /// <summary>
        /// Actualiza los componentes de las tablas y los conbobox
        /// que tiene la ventana RegistroVehiculo.xaml
        /// </summary>
        private void ActualizarConductores()
        {
            conductores.Clear();
            cb_conductor.Items.Clear();
            CargarConductores();
        }

        /// <summary>
        /// Valida que el registro del conductor no 
        /// este repetido en la base de datos
        /// </summary>
        /// <param name="nuevoVehiculo"></param>
        /// <returns></returns>
        private bool VehiculoRepetido(Vehiculo nuevoVehiculo)
        {
            bool vehiculoRepetido = false;
 
            if(entidadesBD.Vehiculos.SingleOrDefault(
                    vehiculo => 
                    vehiculo.idVehiculo == nuevoVehiculo.idVehiculo ||
                    vehiculo.numeroPlaca == nuevoVehiculo.numeroPlaca &&
                    vehiculo.numeroPolizaSeguro == nuevoVehiculo.numeroPolizaSeguro) != null)
            {
                vehiculoRepetido = true;
            }
            return vehiculoRepetido;
        }


        /// <summary>
        /// Recupera la instacia del vehiculo seleccionado
        /// </summary>
        /// <returns></returns>
        private Conductor RecuperarConductor()
        {
            Conductor conductorRecuperado = null;

            if (cb_conductor.SelectedItem != null)
            {
                conductorRecuperado = conductores.ElementAt(cb_conductor.SelectedIndex-1);
            }

            return conductorRecuperado;
        }

        /// <summary>
        /// Llama las funciones apra comprobar que la informacion ingresada
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
        /// Valida que la informacion de los campos de texto
        /// </summary>
        /// <returns></returns>
        private bool ValidezInfoCamposTexto()
        {
            bool valida = true;

            try
            {
                Int32.Parse(tb_anio.Text);
                if ((bool)c_seguro.IsChecked)
                {
                    Int32.Parse(tb_numPolizaSeguro.Text);
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
        /// Comprueba que se selecciono un conductor
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
        /// Comprueba que los campos de texto no esten vacios
        /// </summary>
        /// <returns></returns>
        private bool CamposTextoVacios()
        {
            bool camposTextoVacio = true;

            if ((bool)c_seguro.IsChecked)
            {
                if (tb_anio.Text.Length > 0 &&
                    tb_color.Text.Length > 0 &&
                    tb_marca.Text.Length > 0 &&
                    tb_modelo.Text.Length > 0 &&
                    tb_numeroPlacas.Text.Length > 0 &&
                    tb_numPolizaSeguro.Text.Length > 0 &&
                    tb_nombreAseguradora.Text.Length > 0)
                {
                    camposTextoVacio = false;
                }
                else
                {
                    MessageBox.Show("Hay campos de texto vacios");
                }
            }
            else
            {
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
            }
            

            return camposTextoVacio;
        }


        private void c_seguro_Checked(object sender, RoutedEventArgs e)
        {
            AgregarComponentes_Seguro();
        }

        /// <summary>
        /// Agrega componentes a la ventana en caso de que
        /// se marque la casilla de seguro
        /// </summary>
        private void AgregarComponentes_Seguro()
        {
            lb_numPolizaSeguro.Content = "Número de poliza de seguro";
            lb_nombreAseguradora.Content = "Nombre de aseguradora";
            tb_nombreAseguradora.Height = 23;
            tb_numPolizaSeguro.Height = 23;

            UIElementCollection componentes = sp_componentes.Children;
            componentes.Insert(componentes.IndexOf(lb_color), tb_numPolizaSeguro);
            componentes.Insert(componentes.IndexOf(tb_numPolizaSeguro), lb_numPolizaSeguro);
            componentes.Insert(componentes.IndexOf(lb_numPolizaSeguro), tb_nombreAseguradora);
            componentes.Insert(componentes.IndexOf(tb_nombreAseguradora), lb_nombreAseguradora);
        }

        /// <summary>
        /// Quita los componentes a la vetnana en caso de que
        /// no se marque la casilla de seguro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void c_seguro_Unchecked(object sender, RoutedEventArgs e)
        {
            UIElementCollection componentes = sp_componentes.Children;
            componentes.Remove(lb_numPolizaSeguro);
            componentes.Remove(tb_numPolizaSeguro);
            componentes.Remove(lb_nombreAseguradora);
            componentes.Remove(tb_nombreAseguradora);
        }

        /// <summary>
        /// Concluye la ejecucion del programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Limpia los campos de texto
        /// </summary>
        private void LimpiarVentana()
        {
            tb_numPolizaSeguro.Text = "";
            tb_nombreAseguradora.Text = "";
            tb_color.Text = "";
            tb_anio.Text = "";
            tb_modelo.Text = "";
            tb_marca.Text = "";
            tb_numeroPlacas.Text = "";
            cb_conductor.SelectedItem = null;
        }
    }
}
