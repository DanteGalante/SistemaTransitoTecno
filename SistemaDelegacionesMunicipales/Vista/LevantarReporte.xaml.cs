using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
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
    /// Lógica de interacción para LevantarReporte.xaml
    /// </summary>
    public partial class LevantarReporte : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        List<Vehiculo> listaVehiculos = new List<Vehiculo>();
        List<string> listaPlacaVehiculos = new List<string>();
        //Vehiculo vehiculoRecuperado;
        DelegacionMunicipal delegacionesMunicipales = new DelegacionMunicipal();
        List<string> listaNombreDelegaciones = new List<string>();
        List<DelegacionMunicipal> delegaciones = new List<DelegacionMunicipal>();
        List<Fotografia> fotografias = new List<Fotografia>();


        public LevantarReporte()
        {
            InitializeComponent();
            dgVehiculo.ItemsSource = new List<Vehiculo>();
            LlenarComboVehiculos();
            LlenarComboDelegaciones();
        }

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

                MessageBox.Show("Registro Exitoso");

                cerrarVentana();
            }
            else
            {
                MessageBox.Show("No se puede realizar el registro");
            }
        }
 
        private DelegacionMunicipal RecuperarDelegacionMunicipalSeleccionada()
        {
            DelegacionMunicipal item = new DelegacionMunicipal();

            if (CbDelegacion.SelectedItem != null)
            {
                item = delegaciones.ElementAt<DelegacionMunicipal>(CbDelegacion.SelectedIndex);
            }
            return item;
        }

        private ObservableCollection<Vehiculo> RecuperarVehiculos()
        {
            ObservableCollection<Vehiculo> vehiculos = new ObservableCollection<Vehiculo>();
            foreach (Vehiculo vehiculo in dgVehiculo.ItemsSource)
            {
                vehiculos.Add(vehiculo);
            }
            return vehiculos;
        }

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


        private bool CamposVacios()
        {
            bool resultado = true;
            if (TbCalle.Text.Trim() != null && TbColonia.Text.Trim() != null && TbDescripcion.Text.Trim() != null && CbVehiculos.SelectedItem != null && CbDelegacion.SelectedItem != null)
            {
                resultado = false;
            }
            return resultado;
        }

        private bool ComprobarCalle()
        {
            bool resultado = false;

            if(TbCalle.Text.Length <= 50)
            {
                resultado = true;
            }

            return resultado;
        }

        private bool ComprobarColonia()
        {
            bool resultado = false;

            if(TbColonia.Text.Length <= 50)
            {
                resultado = true;
            }

            return resultado;
        }

        private bool ComprobarDescripcion()
        {
            bool resultado = false;

            if(TbDescripcion.Text.Length <= 250)
            {
                resultado = true;
            }

            return resultado;
        }

        private bool ComprobarFotografia()
        {
            bool resultado = true;
            return resultado;
        }

        private void Agregar_Click(object sender, RoutedEventArgs e)
        {
            if(CbVehiculos.SelectedItem != null)
            {
                bool resultado = false;

                int indiceVehiculo = CbVehiculos.SelectedIndex;
                Console.WriteLine("indice: "+indiceVehiculo);
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
        
        private void SubirImagen_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Imagenes |*.jpg; *.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            openFileDialog.Title = "Seleccionar Imagen";
            
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TbFile.Text = openFileDialog.FileName;
            }

            byte[] file = null;
            Stream myStream = openFileDialog.OpenFile();
            using (MemoryStream ms = new MemoryStream())
            {
                myStream.CopyTo(ms);
                file = ms.ToArray();
            }

            Fotografia nuevaFotografia = new Fotografia();

            nuevaFotografia.imagen = file;



        }

        private void Regresar_Click(object sender, RoutedEventArgs e)
        {
            cerrarVentana();
        }

        private void cerrarVentana()
        {
            MenuAgente menuAgente = new MenuAgente();
            this.Close();
            menuAgente.Show();
        }
    }
}
