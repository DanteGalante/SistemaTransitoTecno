using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Lógica de interacción para ConsultarDelegacionMunicipal_DireccionGeneral.xaml
    /// </summary>
    public partial class ConsultarDelegacionMunicipal_DireccionGeneral : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        List<DelegacionMunicipal> listaDelegaciones = new List<DelegacionMunicipal>();
        List<Usuario> listaUsuarios = new List<Usuario>();
        DelegacionMunicipal delegacionElegida;
        Usuario usuario = new Usuario();

        public ConsultarDelegacionMunicipal_DireccionGeneral()
        {
            InitializeComponent();
            LlenarTabla();
        }

        private void LlenarTabla()
        {
            DbSet<DelegacionMunicipal> delegaciones = entidadesBD.DelegacionesMunicipales;
            
            foreach (var item in delegaciones)
            {
                listaDelegaciones.Add(item);
            }
            dgDelegaciones.AutoGenerateColumns = false;
            dgDelegaciones.ItemsSource = listaDelegaciones;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (dgDelegaciones.SelectedItem == null)
            {
                MessageBox.Show("No se ha seleccionado ninguna Delegación");
            }
            else
            {
                delegacionElegida = RecuperarDelegacion();
                ModificarDelegacionMunicipal_DireccionGeneral modificarDelegacion = new ModificarDelegacionMunicipal_DireccionGeneral(delegacionElegida);
                modificarDelegacion.Show();
                this.Close();
            }
        }

        private DelegacionMunicipal RecuperarDelegacion()
        {
            return delegacionElegida = listaDelegaciones.ElementAt<DelegacionMunicipal>(dgDelegaciones.SelectedIndex);
        }

        public ConsultarDelegacionMunicipal_DireccionGeneral(DelegacionMunicipal delegacionElegida)
        {
            this.delegacionElegida = delegacionElegida;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int indiceSeleccion = dgDelegaciones.SelectedIndex;
            if (indiceSeleccion >= 0)
            {
                DelegacionMunicipal delegacionEliminar = listaDelegaciones[indiceSeleccion];
                MessageBoxResult resultado = MessageBox.Show("¿Estas seguro de eliminar la delegación?" +
                    delegacionEliminar.nombre + "?", "Confirmar acción",
                    MessageBoxButton.OKCancel);
                if (resultado == MessageBoxResult.OK)
                {
                    //RecuperarUsuariosDeDelegacion();
                    entidadesBD.DelegacionesMunicipales.Remove(delegacionEliminar);

                    foreach (var item in listaUsuarios)
                    {
                        //usuario.
                    }
                    
                    //entidadesBD.Usuarios.Remove(usuario);
                    
                    entidadesBD.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Para eliminar un alumno debes seleccionarlo", "Sin selección");
            }
        }

        private List<Usuario> RecuperarUsuariosDeDelegacion()
        {
            DbSet<Usuario> usuarios = entidadesBD.Usuarios;

            foreach (var item in entidadesBD.Usuarios)
            {
                listaUsuarios.Add(item);
                Console.WriteLine(item);
            }

            /*foreach (var item in listaUsuarios)
            {
                if (delegacionElegida.idDelegacion == true)
                {

                }
                l
            }*/
            return listaUsuarios;
        } 

        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MenuAdministrativo_DireccionGeneral regresarMenu = new MenuAdministrativo_DireccionGeneral();
            regresarMenu.Show();
            this.Close();
        }
    }
}
