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

namespace SistemaDireccionGeneral.Vista
{
    /// <summary>
    /// Lógica de interacción para ModificarDelegacionMunicipal_DireccionGeneral.xaml
    /// </summary>
    public partial class ModificarDelegacionMunicipal_DireccionGeneral : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        DelegacionMunicipal delegacionElegida;

        public ModificarDelegacionMunicipal_DireccionGeneral(DelegacionMunicipal delegacionElegida)
        {
            InitializeComponent();
            this.delegacionElegida = delegacionElegida;
            CargarDatosDelegacion();
        }

        /// <summary>
        /// Manejo de modificacion de delegaciones
        /// </summary>
        public void ManejoModificacionDelegacion()
        {
            if (DelegacionValida() && !DelegacionRepetida(RecuperarDelegacionNuevo()))
            {
                DelegacionMunicipal modificarDelegacion = 
                    entidadesBD.DelegacionesMunicipales.Find(delegacionElegida.idDelegacion);

                modificarDelegacion.nombre = tbNombreDelegacion.Text;
                modificarDelegacion.calle = tbCalle.Text;
                modificarDelegacion.colonia = tbColonia.Text;
                modificarDelegacion.numero = int.Parse(tbNumero.Text);
                modificarDelegacion.codigoPostal = int.Parse(tbCodigoPostal.Text);
                modificarDelegacion.correo = tbCorreoElectronico.Text;
                modificarDelegacion.telefono = tbTelefono.Text;
                modificarDelegacion.municipio = tbMunicipio.Text;

                entidadesBD.SaveChanges();

                MessageBox.Show("Modificación de la Delegación Exitosa");
            }
            else
            {
                MessageBox.Show("Delgación ya existente en la base de datos");
            }
        }

        /// <summary>
        /// Verifica que el nombre de la delegacion sea valida
        /// </summary>
        /// <returns>Booleano que representa el nombre de la delegacion valida</returns>
        private Boolean NombreDelegacionValida()
        {
            if (tbNombreDelegacion.Text.Length >= 2 && tbNombreDelegacion.Text.Length <= 50)
            {
                lbErrorNombreDelegacion.Content = "";
                return true;
            }
            else
            {
                lbErrorNombreDelegacion.Content = "Nombre invalido";
            }
            return false;
        }

        /// <summary>
        /// Verifica que el nombre de la calle de la delegación sea valido
        /// </summary>
        /// <returns>Booleano que representa la validez del nombre de la calle de la delegacion</returns>
        private Boolean CalleValida()
        {
            if (tbCalle.Text.Length >= 3 && tbCalle.Text.Length <= 50)
            {
                lbErrorCalle.Content = "";
                return true;
            }
            else
            {
                lbErrorCalle.Content = "Calle invalida";
            }
            return false;
        }

        /// <summary>
        /// Verifica que el nombre de la colonia de la delegación sea valido
        /// </summary>
        /// <returns>Booleano que representa la validez del nombre de la colonia de la delegación</returns>
        private Boolean ColoniaValida()
        {
            if (tbColonia.Text.Length >= 3 && tbColonia.Text.Length <= 50)
            {
                lbErrorColonia.Content = "";
                return true;
            }
            else
            {
                lbErrorColonia.Content = "Colonia invalida";
            }
            return false;
        }

        /// <summary>
        /// Verifica la validez del numero de casa de la delegación
        /// </summary>
        /// <returns>Booleano que representa la validez del numero de la casa de la delegacion</returns>
        private Boolean NumeroCasaValido()
        {
            if (tbNumero.Text.Length >= 1 && tbNumero.Text.Length <= 3)
            {
                lbErrorNumero.Content = "";
                return true;
            }
            else
            {
                lbErrorNumero.Content = "Número invalido";
            }
            return false;
        }

        /// <summary>
        /// Verifica que el codigo postal de la delegación sea válido
        /// </summary>
        /// <returns>Booleano que representa la validez del codigo postal</returns>
        private Boolean CodigoPostalValido()
        {
            if (tbCodigoPostal.Text.Length >= 5 && tbCodigoPostal.Text.Length <= 10)
            {
                lbErrorCodigoPostal.Content = "";
                return true;
            }
            else
            {
                lbErrorCodigoPostal.Content = "Codigo Postal invalido";
            }
            return false;
        }

        /// <summary>
        /// Verifica la validez del correo electronico de la delegación
        /// </summary>
        /// <returns>Booleano que representa la validez del correo electronico </returns>
        private Boolean CorreoElectronicoValido()
        {
            if (tbCorreoElectronico.Text.Length >= 11 && tbCorreoElectronico.Text.Length <= 50)
            {
                lbErrorCorreoElectronico.Content = "";
                return true;
            }
            else
            {
                lbErrorCorreoElectronico.Content = "Correo Electronivo invalido";
            }
            return false;
        }

        /// <summary>
        /// Verifica que el numero de la delegación sea valido
        /// </summary> 
        /// <returns>
        /// Booleano que representa la validez del numero telefonico</returns>
        private Boolean TelefonoValido()
        {
            if (tbTelefono.Text.Length == 10)
            {
                lbErrorTelefono.Content = "";
                return true;
            }
            else
            {
                lbErrorTelefono.Content = "Teléfono invalido";
            }
            return false;
        }

        /// <summary>
        /// Verifica que el municipio de la delegación sea valido
        /// </summary>
        /// <returns>Booleano que representa la validez del numero télefonico</returns>
        private Boolean MunicipioValido()
        {
            if (tbCalle.Text.Length >= 3 && tbCalle.Text.Length <= 50)
            {
                lbErrorMunicipio.Content = "";
                return true;
            }
            else
            {
                lbErrorMunicipio.Content = "Municipio invalido";
            }
            return false;
        }

        /// <summary>
        /// Verifica que la delegación sea valida para el registro
        /// </summary>
        /// <returns>Booleano que representa la validez de la delegación</returns>
        public Boolean DelegacionValida()
        {
            bool aux = true;
            int contador = 8;

            while (contador >= 1 && aux == true)
            {
                switch (contador)
                {
                    case 8:
                        aux = NombreDelegacionValida();
                        break;
                    case 7:
                        aux = CalleValida();
                        break;
                    case 6:
                        aux = ColoniaValida();
                        break;
                    case 5:
                        aux = NumeroCasaValido();
                        break;
                    case 4:
                        aux = CodigoPostalValido();
                        break;
                    case 3:
                        aux = CorreoElectronicoValido();
                        break;
                    case 2:
                        aux = TelefonoValido();
                        break;
                    case 1:
                        aux = MunicipioValido();
                        break;
                }
                contador--;
            }
            return aux;
        }

        /// <summary>
        /// Maneja el evento del clic en el botón "Modificar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            ManejoModificacionDelegacion();
        }

        /// <summary>
        /// Maneja el evento de clic en el boton "Cancelar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            ConsultarDelegacionMunicipal_DireccionGeneral regresarConsultarDelegaciones = 
                new ConsultarDelegacionMunicipal_DireccionGeneral();
            regresarConsultarDelegaciones.Show();
            this.Close();
        }
        
        /// <summary>
        /// Carga los datos de la ventana con la informacion de la delegacion elegida
        /// </summary>
        public void CargarDatosDelegacion()
        {
            tbNombreDelegacion.Text = delegacionElegida.nombre;
            tbCalle.Text = delegacionElegida.calle;
            tbColonia.Text = delegacionElegida.colonia;
            tbNumero.Text = delegacionElegida.numero.ToString();
            tbCodigoPostal.Text = delegacionElegida.codigoPostal.ToString();
            tbCorreoElectronico.Text = delegacionElegida.correo;
            tbTelefono.Text = delegacionElegida.telefono;
            tbMunicipio.Text = delegacionElegida.municipio;
        }

        /// <summary>
        /// Verifica que la delegacion especificada no este repetida en la base de datos
        /// </summary>
        /// <param name="nuevaDelegacion"></param>
        /// <returns>Booleano que representa si una delegacion esta repetida</returns>
        private Boolean DelegacionRepetida(DelegacionMunicipal nuevaDelegacion)
        {
            bool delegacionRepetido = false;

            if (entidadesBD.DelegacionesMunicipales.SingleOrDefault(
                delegacion =>
                delegacion.nombre == nuevaDelegacion.nombre &&
                delegacion.calle == nuevaDelegacion.calle &&
                delegacion.colonia == nuevaDelegacion.colonia &&
                delegacion.numero == nuevaDelegacion.numero &&
                delegacion.codigoPostal == nuevaDelegacion.codigoPostal &&
                delegacion.correo == nuevaDelegacion.correo &&
                delegacion.telefono == nuevaDelegacion.telefono &&
                delegacion.municipio == nuevaDelegacion.municipio) != null)
            {
                delegacionRepetido = true;
            }
            return delegacionRepetido;
        }

        /// <summary>
        /// Crea una delegacion nueva a partir de los datos introducidos a la ventana
        /// </summary>
        /// <returns>Delegacion nueva</returns>
        private DelegacionMunicipal RecuperarDelegacionNuevo()
        {
            DelegacionMunicipal verificarDelegacion = new DelegacionMunicipal();

            verificarDelegacion.nombre = tbNombreDelegacion.Text;
            verificarDelegacion.calle = tbCalle.Text;
            verificarDelegacion.colonia = tbColonia.Text;
            verificarDelegacion.numero = int.Parse(tbNumero.Text);
            verificarDelegacion.codigoPostal = int.Parse(tbCodigoPostal.Text);
            verificarDelegacion.correo = tbCorreoElectronico.Text;
            verificarDelegacion.telefono = tbTelefono.Text;
            verificarDelegacion.municipio = tbMunicipio.Text;

            return verificarDelegacion;
        }
    }
}
