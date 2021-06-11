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
    /// Lógica de interacción para DictaminarReporte.xaml
    /// </summary>
    public partial class DictaminarReporte : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        Reporte reporteElegido;
        Usuario usuarioIniciado;

        public DictaminarReporte(Reporte reporteElegido, Usuario usuarioIniciado)
        {
            InitializeComponent();
            this.reporteElegido = reporteElegido;
            this.usuarioIniciado = usuarioIniciado;
        }

        private void Guardar_Click(object sender, RoutedEventArgs e)
        {
            if (ComprobarCampos())
            {
                Dictamen nuevoDictamen = new Dictamen();

                reporteElegido.estatus = "Dictaminado";
                nuevoDictamen.descripcion = TbDescripcion.Text;
                nuevoDictamen.fecha = DateTime.Today;
                nuevoDictamen.hora = DateTime.Now.TimeOfDay;
                nuevoDictamen.folio = GenerarFolio();
                nuevoDictamen.nombreCompletoPerito = usuarioIniciado.nombres;
                entidadesBD.Dictamenes.Add(nuevoDictamen);
                //entidadesBD.Reportes.Add(reporteElegido);
                entidadesBD.SaveChanges();

                MessageBox.Show("Dictamen registrado con exito");
                cerrarVentana();


            }
            else
            {
                MessageBox.Show("Debe ingresar un dictamen");
            }

        }

        private string GenerarFolio()
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            int longitudContrasenia = 10;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }

            return contraseniaAleatoria;
        }

        private bool ComprobarCampos()
        {
            bool resultado = false;

            if (!TbDescripcion.Text.Trim().Equals(""))
            {
                resultado = true;
            }

            return resultado;
        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            cerrarVentana();
        }

        private void cerrarVentana()
        {
            MenuPerito menuPerito = new MenuPerito(usuarioIniciado);
            this.Close();
            menuPerito.Show();
        }
    }
}
