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
    /// Autor: Emilio Antonio Alarcon Santos
    /// Lógica de interacción para RegistrarDelegacionMunicipal_DireccionGeneral.xaml
    /// </summary>
    public partial class RegistrarDelegacionMunicipal_DireccionGeneral : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        public RegistrarDelegacionMunicipal_DireccionGeneral()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Menaja el registro de delegaciones, checando que cumplan ciertas validaciones
        /// </summary>
        public void ManejoRegistroDelegacion()
        {
            VerificarDelegacion();
            if (DelegacionValida() && !DelegacionRepetida(RecuperarDelegacionNuevo()))
            {
                DelegacionMunicipal nuevaDelegacion = new DelegacionMunicipal();

                nuevaDelegacion.nombre = tbNombreDelegacion.Text;
                nuevaDelegacion.calle = tbCalle.Text;
                nuevaDelegacion.colonia = tbColonia.Text;
                nuevaDelegacion.numero = int.Parse(tbNumero.Text);
                nuevaDelegacion.codigoPostal = int.Parse(tbCodigoPostal.Text);
                nuevaDelegacion.correo = tbCorreoElectronico.Text;
                nuevaDelegacion.telefono = tbTelefono.Text;
                nuevaDelegacion.municipio = tbMunicipio.Text;

                entidadesBD.DelegacionesMunicipales.Add(nuevaDelegacion);

                entidadesBD.SaveChanges();

                MessageBox.Show("Registro Delegación Exitoso");
            }
        }
        /// <summary>
        /// Verifica varias validaciones a la entradas de la ventana
        /// </summary>
        public void VerificarDelegacion()
        {
            NombreDelegacionValida();
            CalleValida();
            ColoniaValida();
            NumeroCasaValido();
            CodigoPostalValido();
            CorreoElectronicoValido();
            TelefonoValido();
            MunicipioValido();
        }
        /// <summary>
        /// Verifica que el nombre de la delegacion sea valido
        /// </summary>
        /// <returns>Booleano que representa la validez del nombre de la delegacion</returns>
        private Boolean NombreDelegacionValida()
        {
            if(tbNombreDelegacion.Text.Length >= 2 && tbNombreDelegacion.Text.Length <= 50)
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

            while (contador >=1 && aux == true)
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
                contador --;
            }
            return aux;
        }    
        /// <summary>
        /// Maneja el evento del clic en el botón "Registrar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            ManejoRegistroDelegacion();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MenuAdministrativo_DireccionGeneral regresarMenuAdministrativo = new MenuAdministrativo_DireccionGeneral();
            regresarMenuAdministrativo.Show();
            this.Close();
        }

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
