using BaseDeDatos;
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
    /// Autor: Dan Javier Olvera Villeda
    /// Interaction logic for RegistrarConductor.xaml
    /// </summary>
    public partial class RegistrarConductorWindow : Window
    {
        private BDTransitoEntities bdTransitoEntities = new BDTransitoEntities();

        public RegistrarConductorWindow()
        {
            InitializeComponent();
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
        /// Controla los metodos de registrar un conductor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_registrar_Click(object sender, RoutedEventArgs e)
        {
            if (InformacionValida())
            {
                RegistrarConductor();
            }
        }

        /// <summary>
        /// Comprueba que la inforacion ingresada sea valida
        /// </summary>
        /// <returns></returns>
        private bool InformacionValida()
        {
            bool valido = false;

            if (!CamposTextoVacios() && FechaSeleccionada())
            {
                valido = true;
            }

            return valido;
        }

        /// <summary>
        /// Comprueba que los campos de texto no esten vacios
        /// </summary>
        /// <returns></returns>
        private bool CamposTextoVacios()
        {
            bool camposTextVacios = true;

            if (tb_nombre.Text.Length > 0 &&
                tb_apellidoPat.Text.Length > 0 &&
                tb_nombre.Text.Length > 0 &&
                tb_numLicenciaConducir.Text.Length > 0)
            {
                camposTextVacios = false;
            }
            else
            {
                MessageBox.Show("Hay campos de texto vacios");
            }

            return camposTextVacios;
        }

        /// <summary>
        /// Comprueba que se selecciono una fecha
        /// </summary>
        /// <returns></returns>
        private bool FechaSeleccionada()
        {
            DateTime? fechaNac = dp_fechaNacimiento.SelectedDate;
            
            bool fechaSeleccionada = false;
            if (fechaNac != null)
            {
                fechaSeleccionada = true;
            }
            else
            {
                MessageBox.Show("No se ha seleccionado una fecha de nacimiento");
            }

            return fechaSeleccionada;
        }

        /// <summary>
        /// Controla el registro de la informacion de un 
        /// nuevo condutor en la base de datos
        /// </summary>
        private void RegistrarConductor()
        {
            Conductor nuevoConductor = new Conductor();

            nuevoConductor.nombres = tb_nombre.Text;
            nuevoConductor.apellidoPaterno = tb_apellidoPat.Text;
            nuevoConductor.apellidoMaterno = tb_apellidoMat.Text;
            nuevoConductor.fechaNacimiento = (DateTime)dp_fechaNacimiento.SelectedDate.Value;
            nuevoConductor.numeroLicenciaConducir = tb_numLicenciaConducir.Text;

            if (ConductorRepetido(nuevoConductor) == false)
            {
                bdTransitoEntities.Conductores.Add(nuevoConductor);
                bdTransitoEntities.SaveChanges();
                MessageBox.Show("Conductor registrado exitosamente");
                LimpiarVentana();
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("ERROR: El conductor ya esta registrado en el sistema");
                this.DialogResult = false;
            }
            

        }

        /// <summary>
        /// Limpia los campos de texto de la informacion
        /// </summary>
        private void LimpiarVentana()
        {
            tb_nombre.Text = "";
            tb_apellidoPat.Text = "";
            tb_apellidoMat.Text = "";
            dp_fechaNacimiento = null;
            tb_numLicenciaConducir.Text = "";
        }


        /// <summary>
        /// Comprueba que el regstro del conductor no este repetido
        /// en la base de datos del sistema
        /// </summary>
        /// <param name="nuevoConductor"></param>
        /// <returns></returns>
        private bool ConductorRepetido(Conductor nuevoConductor)
        {
            bool conductorRepetido = false;

            if (bdTransitoEntities.Conductores.SingleOrDefault(
                    conductor =>
                    conductor.nombres == nuevoConductor.nombres &&
                    conductor.apellidoPaterno == nuevoConductor.apellidoPaterno &&
                    conductor.apellidoMaterno == nuevoConductor.apellidoMaterno &&
                    conductor.fechaNacimiento == nuevoConductor.fechaNacimiento &&
                    conductor.numeroLicenciaConducir == nuevoConductor.numeroLicenciaConducir
                    ) != null)
            {
                conductorRepetido = true;
            }
            return conductorRepetido;
        }
    }
}
