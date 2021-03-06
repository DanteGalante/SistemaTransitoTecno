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
    /// Autor: Dan Javier Olvera Villeda
    /// Interaction logic for ModificarConductor.xaml
    /// </summary>
    public partial class ModificarConductor : Window
    {
        BDTransitoEntities bdTransitoEntities = new BDTransitoEntities();
        Conductor conductorAModificar;

        public ModificarConductor(Conductor conductorAModificar)
        {
            InitializeComponent();
            CargarDatosConductorMod(conductorAModificar);
        }

        /// <summary>
        /// Llena la ventana de los datos del conductor que se desea modificar
        /// </summary>
        /// <param name="conductorMod"></param>
        private void CargarDatosConductorMod(Conductor conductorMod)
        {
            this.conductorAModificar = bdTransitoEntities.Conductores.SingleOrDefault(conductor =>
            conductor.idConductor == conductorMod.idConductor);

            tb_nombre.Text = conductorAModificar.nombres;
            tb_apellidoPat.Text = conductorAModificar.apellidoPaterno;
            tb_apellidoMat.Text = conductorAModificar.apellidoMaterno;
            dp_fechaNacimiento.SelectedDate = conductorAModificar.fechaNacimiento;
            tb_licenciaCond.Text = conductorAModificar.numeroLicenciaConducir.ToString();

        }

        /// <summary>
        /// Comprueba que la informacion es valida y si regresa true
        /// modifica la informacion del conducotr seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_modificar_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarInformacion())
            {
                ModificarAConductor();
            }
        }

        /// <summary>
        /// COmprueba que la informacion ingresada sea valida para 
        /// realizar el registro
        /// </summary>
        /// <returns></returns>
        private bool ValidarInformacion()
        {
            bool valido = false;

            if (!CamposTextoVacios() && ValidezInfoCamposTexto())
            {
                valido = true;
            }

            return valido;
        }
        
        /// <summary>
        /// Verifica que los campos de texto de la ventana no esten vacios
        /// </summary>
        /// <returns></returns>
        private bool CamposTextoVacios()
        {
            bool camposTextoVacio = true;

            if (tb_nombre.Text.Length > 0 &&
                tb_apellidoPat.Text.Length > 0 &&
                tb_apellidoMat.Text.Length > 0 &&
                dp_fechaNacimiento.SelectedDate.HasValue == true &&
                tb_licenciaCond.Text.Length > 0
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
        /// Verifica que no se introduzcan datos incorrectos en los campos de texto
        /// Por ejemplo, un numero donde solo se permiten letras, o viceversa
        /// </summary>
        /// <returns></returns>
        private bool ValidezInfoCamposTexto()
        {
            bool valida = true;

            try
            {
                Int32.Parse(tb_licenciaCond.Text);
            }
            catch (Exception)
            {
                valida = false;
                MessageBox.Show("Campos hay un campo de texto invalido");
            }

            return valida;
        }
        
        /// <summary>
        /// Se toma la informacion de los campos de texto y se actualiza con eso al conductor
        /// </summary>
        private void ModificarAConductor()
        {
            try
            {
                conductorAModificar.nombres = tb_nombre.Text;
                conductorAModificar.apellidoPaterno = tb_apellidoPat.Text;
                conductorAModificar.apellidoMaterno = tb_apellidoMat.Text;
                conductorAModificar.fechaNacimiento = (DateTime)dp_fechaNacimiento.SelectedDate;
                conductorAModificar.numeroLicenciaConducir = tb_licenciaCond.Text;

                if (ConductorRepetido(conductorAModificar) == false)
                {
                    bdTransitoEntities.SaveChanges();
                    MessageBox.Show("Vehiculo modificado exitosamente");
                }
                else
                {
                    MessageBox.Show("ERROR: El vehiculo ya esta registrado en el sistema");
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error en la conexion con la base de datos", "Error", MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Busca en la BD la existencia de otra entidad con los mismos atributos
        /// </summary>
        /// <param name="conductorAModificar"></param>
        /// <returns></returns>
        private bool ConductorRepetido(Conductor conductorAModificar)
        {
            bool vehiculoRepetido = false;

            if (bdTransitoEntities.Conductores.SingleOrDefault(
                    cond =>
                    cond.nombres == conductorAModificar.nombres &&
                    cond.apellidoPaterno == conductorAModificar.apellidoPaterno &&
                    cond.apellidoMaterno == conductorAModificar.apellidoMaterno && 
                    cond.fechaNacimiento == conductorAModificar.fechaNacimiento &&
                    cond.numeroLicenciaConducir == conductorAModificar.numeroLicenciaConducir)
                    != null)
            {
                vehiculoRepetido = true;
            }
            return vehiculoRepetido;
        }

        /// <summary>
        /// Controla la detencion de la ejecuciond el programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
