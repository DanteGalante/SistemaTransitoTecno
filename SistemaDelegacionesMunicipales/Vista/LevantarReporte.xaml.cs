using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace SistemaDelegacionesMunicipales.Vista
{
    /// <summary>
    /// Autor: Alan Adair Morgado Morales
    /// Lógica de interacción para LevantarReporte.xaml
    /// </summary>
    public partial class LevantarReporte : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        List<Vehiculo> listaVehiculos = new List<Vehiculo>();
        List<string> listaPlacaVehiculos = new List<string>();
        List<string> listaNombreDelegaciones = new List<string>();
        List<DelegacionMunicipal> delegaciones = new List<DelegacionMunicipal>();
        OpenFileDialog openFileDialog = new OpenFileDialog();
        List<byte[]> imagenes = new List<byte[]>();

        Usuario usuarioIniciado;

        public LevantarReporte(Usuario usuarioIniciado)
        {
            InitializeComponent();
            this.usuarioIniciado = usuarioIniciado;
            dgVehiculo.ItemsSource = new List<Vehiculo>();
            LlenarComboVehiculos();
            LlenarComboDelegaciones();
        }

        /// <summary>
        /// Llena el comboBox con los vehiculos que esten registrados en la base de datos del sistema
        /// </summary>
        private void LlenarComboVehiculos()
        {
            DbSet<Vehiculo> vehiculos = entidadesBD.Vehiculos;
            foreach (var item in vehiculos)
            {
                listaVehiculos.Add(item);
            }

            foreach (var item in listaVehiculos)
            {
                listaPlacaVehiculos.Add(item.numeroPlaca);
            }
            CbVehiculos.ItemsSource = listaPlacaVehiculos;
        }

        /// <summary>
        /// Llena el ComboBox con las delegaciones que estan registradas en la base de datos del sistema
        /// </summary>
        private void LlenarComboDelegaciones()
        {
            foreach (var item in entidadesBD.DelegacionesMunicipales)
            {
                delegaciones.Add(item);
            }

            foreach (var item in delegaciones)
            {
                listaNombreDelegaciones.Add(item.nombre);
            }
            CbDelegacion.ItemsSource = listaNombreDelegaciones;
        }

        /// <summary>
        /// Guarda un reporte con la informacion una vez validada
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Guardar_Cick(object sender, RoutedEventArgs e)
        {
            if (ComprobarCampos())
            {
                Reporte nuevoReporte = new Reporte();

                nuevoReporte.calle = TbCalle.Text;
                nuevoReporte.colonia = TbColonia.Text;
                nuevoReporte.descripcion = TbDescripcion.Text;
                nuevoReporte.fecha = DateTime.Now;
                nuevoReporte.estatus = "En proceso";
                nuevoReporte.DelegacionMunicipal = RecuperarDelegacionMunicipalSeleccionada();
                nuevoReporte.Vehiculos = RecuperarVehiculos();
                entidadesBD.Reportes.Add(nuevoReporte);
                entidadesBD.SaveChanges();
                GuardarImagen(nuevoReporte);
                MessageBox.Show("Registro Exitoso");

                cerrarVentana();
            }
            else
            {
                MessageBox.Show("No se puede realizar el registro");
            }
        }

        /// <summary>
        /// Recupera la instancia de una delegacion municipal que ha sido
        /// seleccionada en el combobox
        /// </summary>
        /// <returns></returns>
        private DelegacionMunicipal RecuperarDelegacionMunicipalSeleccionada()
        {
            DelegacionMunicipal item = new DelegacionMunicipal();

            if (CbDelegacion.SelectedItem != null)
            {
                item = delegaciones.ElementAt<DelegacionMunicipal>(CbDelegacion.SelectedIndex);
            }
            return item;
        }

        /// <summary>
        /// Recupera la instacion de un vehiculo que ha sido seleccionado del combobox
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<Vehiculo> RecuperarVehiculos()
        {
            ObservableCollection<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();
            foreach (Vehiculo vehiculo in dgVehiculo.ItemsSource)
            {
                vehiculos.Add(vehiculo);
            }
            return vehiculos;
        }

        /// <summary>
        /// Comprueba que los campos son validos mediante el tamaño minio y maximo
        /// </summary>
        /// <returns></returns>
        private bool ComprobarCampos()
        {
            bool aux = true;

            if (CamposVacios() == true)
            {
                aux = false;
            }
            else
            {
                int contador = 4;

                while (contador >= 1 && aux == true)
                {
                    switch (contador)
                    {
                        case 4:
                            aux = ComprobarCalle();
                            break;
                        case 3:
                            aux = ComprobarColonia();
                            break;
                        case 2:
                            aux = ComprobarDescripcion();
                            break;
                        case 1:
                            aux = ComprobarFotografia();
                            break;
                    }
                    contador--;
                }

                Console.WriteLine("Si llega a validar al usuario");
            }

            return aux;
        }

        /// <summary>
        /// Comprueba que los campos no esten vacios para realizar el registro del reporte
        /// </summary>
        /// <returns></returns>
        private bool CamposVacios()
        {
            bool resultado = true;
            if (TbCalle.Text.Trim() != null && TbColonia.Text.Trim() != null && TbDescripcion.Text.Trim() != null
                && CbVehiculos.SelectedItem != null && CbDelegacion.SelectedItem != null)
            {
                resultado = false;
            }
            return resultado;
        }

        /// <summary>
        /// Comprueba que el tamaño del texto es valido
        /// </summary>
        /// <returns></returns>
        private bool ComprobarCalle()
        {
            bool resultado = false;

            if (TbCalle.Text.Length <= 50)
            {
                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Comprueba que el tamaño del texto es valido
        /// </summary>
        /// <returns></returns>
        private bool ComprobarColonia()
        {
            bool resultado = false;

            if (TbColonia.Text.Length <= 50)
            {
                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Comprueba que el tamaño del texto es valido
        /// </summary>
        /// <returns></returns>
        private bool ComprobarDescripcion()
        {
            bool resultado = false;

            if (TbDescripcion.Text.Length <= 250)
            {
                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Comprueba que hay almenos 3 fotografias para realizar el registro del reporte
        /// </summary>
        /// <returns></returns>
        private bool ComprobarFotografia()
        {
            bool resultado = false;

            if (imagenes.Count() >= 3)
            {
                resultado = true;
            }

            return resultado;
        }

        /// <summary>
        /// Agrega el vehiculo seleccionado del combobo a un datagrid y comprobando que 
        /// esta seleccion no se repita
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if (CbVehiculos.SelectedItem != null)
            {
                bool resultado = false;

                int indiceVehiculo = CbVehiculos.SelectedIndex;
                Console.WriteLine("indice: " + indiceVehiculo);
                Vehiculo vehiculoRecuperado = listaVehiculos.ElementAt(indiceVehiculo);
                List<Vehiculo> itemsActualizados = (List<Vehiculo>)dgVehiculo.ItemsSource;


                foreach (var item in itemsActualizados)
                {
                    if (item == vehiculoRecuperado)
                    {
                        resultado = true;
                    }
                }
                if (resultado != true)
                {
                    itemsActualizados.Add(vehiculoRecuperado);
                    dgVehiculo.AutoGenerateColumns = false;
                    dgVehiculo.ItemsSource = itemsActualizados;
                    dgVehiculo.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Ya selecciono este vehiculo");
                }
            }
            else
            {
                MessageBox.Show("Selecciona un vehiculo para poder realizar el reporte");
            }

        }

        /// <summary>
        /// Abre el explorador de archivos con un opendialog para seleccionar una imagen del archivo local
        /// , la cual formara parte del reporte
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbrirExplorador_Click(object sender, RoutedEventArgs e)
        {

            if (imagenes.Count() < 8)
            {

                openFileDialog.Filter = "Imagenes |*.jpg; *.png";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                openFileDialog.Title = "Seleccionar Imagen";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    byte[] file = null;
                    Stream myStream = openFileDialog.OpenFile();
                    MemoryStream ms = new MemoryStream();

                    myStream.CopyTo(ms);
                    file = ms.ToArray();
                    imagenes.Add(file);
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(openFileDialog.FileName);
                    bmp.EndInit();

                    if (imagen1.Source == null)
                    {
                        imagen1.Source = bmp;
                    }
                    else if (imagen2.Source == null)
                    {
                        imagen2.Source = bmp;
                    }
                    else if (imagen3.Source == null)
                    {
                        imagen3.Source = bmp;
                    }
                    else if (imagen4.Source == null)
                    {
                        imagen4.Source = bmp;
                    }
                    else if (imagen5.Source == null)
                    {
                        imagen5.Source = bmp;
                    }
                    else if (imagen6.Source == null)
                    {
                        imagen6.Source = bmp;
                    }
                    else if (imagen7.Source == null)
                    {
                        imagen7.Source = bmp;
                    }
                    else if (imagen8.Source == null)
                    {
                        imagen8.Source = bmp;
                    }

                }
                else
                {
                    MessageBox.Show("Ya no puede agregar mas fotografias");
                }
            }
        }

        /// <summary>
        /// Recibe la el uevo reporte creado para guardar las fotografias asociadas a el
        /// </summary>
        /// <param name="nuevoReporte"></param>
        private void GuardarImagen(Reporte nuevoReporte)
        {

            foreach (var item in imagenes)
            {
                Fotografia nuevaFotografia = new Fotografia();
                nuevaFotografia.imagen = item;
                nuevaFotografia.Reporte = nuevoReporte;
                entidadesBD.Fotografias.Add(nuevaFotografia);
            }

            entidadesBD.SaveChanges();

        }

        /// <summary>
        /// Controlador para cerrar la ventana actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            cerrarVentana();
        }

        /// <summary>
        /// Cierra la ventana actual y uestra la ventana MenuAgente.xaml
        /// </summary>
        private void cerrarVentana()
        {
            MenuAgente menuAgente = new MenuAgente(usuarioIniciado);
            this.Close();
            menuAgente.Show();
        }
    }
}
