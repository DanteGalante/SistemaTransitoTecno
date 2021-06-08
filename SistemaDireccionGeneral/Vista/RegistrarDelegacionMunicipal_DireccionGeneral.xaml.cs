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
    /// Lógica de interacción para RegistrarDelegacionMunicipal_DireccionGeneral.xaml
    /// </summary>
    public partial class RegistrarDelegacionMunicipal_DireccionGeneral : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        public RegistrarDelegacionMunicipal_DireccionGeneral()
        {
            InitializeComponent();
        }

        public void ManejoRegistroDelegacion()
        {
            VerificarDelegacion();
            if (DelegacionValida())
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
    }
}
