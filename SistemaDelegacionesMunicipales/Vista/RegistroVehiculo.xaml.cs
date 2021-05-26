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
    /// Lógica de interacción para IniciarSesion.xaml
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
            cargarConductores();
        }

        private void cargarConductores()
        {
            DbSet<Conductor> conductoresDBSet = entidadesBD.Conductores;

            if (conductoresDBSet.Count() > 0)
            {
                foreach (Conductor conductor in conductoresDBSet)
                {
                    cb_conductor.Items.Add(conductor.nombres + " " + conductor.apellidoPaterno + " " + conductor.apellidoMaterno);
                    conductores.Add(conductor);
                }
            }
        }

        private void btn_registrar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarInformacion())
            {
                RegistrarVehiculo();
            }
        }

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
                if ((string)cb_conductor.SelectedValue == "Otro conductor")
                {
                    MessageBoxResult respuesta = MessageBox.Show("Quieres crear un nuevo conductor?","",MessageBoxButton.YesNo);
                    if (respuesta == MessageBoxResult.Yes)
                    {
                        RegistrarConductor nuevaVentana = new RegistrarConductor();
                        nuevaVentana.ShowDialog();
                        //FUNCIONALIDAD. abre la ventana de registrar conductor, cuando lo haya registrado regresa y carga de nuevo el combobox
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
                        MessageBox.Show("Vehiculo ya esta registrado en el sistema");
                        LimpiarVentana();
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

        private bool VehiculoRepetido(Vehiculo nuevoVehiculo)
        {
            bool vehiculoRepetido = false;
 
            if(entidadesBD.Vehiculos.SingleOrDefault(
                    vehiculo => 
                    vehiculo.año == nuevoVehiculo.año &&
                    vehiculo.color == nuevoVehiculo.color &&
                    vehiculo.marca == nuevoVehiculo.marca &&
                    vehiculo.modelo == nuevoVehiculo.modelo &&
                    vehiculo.nombreAseguradora == nuevoVehiculo.nombreAseguradora &&
                    vehiculo.numeroPlaca == nuevoVehiculo.numeroPlaca &&
                    vehiculo.numeroPolizaSeguro == nuevoVehiculo.numeroPolizaSeguro) != null)
            {
                vehiculoRepetido = true;
            }
            return vehiculoRepetido;
        }

        private Conductor RecuperarConductor()
        {
            Conductor conductorRecuperado = null;

            if (cb_conductor.SelectedItem != null)
            {
                conductorRecuperado = conductores.ElementAt(cb_conductor.SelectedIndex-1);
            }

            return conductorRecuperado;
        }

        private bool ValidarInformacion()
        {
            bool valido = false;

            if (!CamposTextoVacios() && ConductorElegido() && ValidezInfoCamposTexto())
            {
                valido = true;
            }

            return valido;
        }

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

        private void AgregarComponentes_Seguro()
        {
            lb_numPolizaSeguro.Content = "Número de poliza de seguro";
            lb_nombreAseguradora.Content = "Nombre de aseguradora";

            UIElementCollection componentes = sp_componentes.Children;
            componentes.Insert(componentes.IndexOf(c_seguro), lb_numPolizaSeguro);
            componentes.Insert(componentes.IndexOf(lb_numPolizaSeguro), tb_numPolizaSeguro);
            componentes.Insert(componentes.IndexOf(tb_numPolizaSeguro), lb_nombreAseguradora);
            componentes.Insert(componentes.IndexOf(lb_nombreAseguradora), tb_nombreAseguradora);
        }

        private void c_seguro_Unchecked(object sender, RoutedEventArgs e)
        {
            UIElementCollection componentes = sp_componentes.Children;
            componentes.Remove(lb_numPolizaSeguro);
            componentes.Remove(tb_numPolizaSeguro);
            componentes.Remove(lb_nombreAseguradora);
            componentes.Remove(tb_nombreAseguradora);
        }

        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

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
