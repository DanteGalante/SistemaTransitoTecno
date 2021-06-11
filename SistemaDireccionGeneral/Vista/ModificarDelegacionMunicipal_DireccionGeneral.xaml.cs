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

        public void ManejoModificacionDelegacion()
        {
            VerificarDelegacion();
            if (DelegacionValida() && !DelegacionRepetida(RecuperarDelegacionNuevo()))
            {
                DelegacionMunicipal modificarDelegacion = entidadesBD.DelegacionesMunicipales.Find(delegacionElegida.idDelegacion);

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

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            ManejoModificacionDelegacion();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            ConsultarDelegacionMunicipal_DireccionGeneral regresarConsultarDelegaciones = new ConsultarDelegacionMunicipal_DireccionGeneral();
            regresarConsultarDelegaciones.Show();
            this.Close();
        }
        
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
