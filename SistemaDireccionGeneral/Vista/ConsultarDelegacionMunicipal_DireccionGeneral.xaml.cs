using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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

        
        /// <summary>
        /// Llena la tabla con delegaciones municipales
        /// </summary>
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

        /// <summary>
        /// Vacia la tabla que contiene las validaciones municipales
        /// </summary>
        private void VaciarTabla()
        {
            listaDelegaciones.Clear();
            dgDelegaciones.AutoGenerateColumns = false;
            dgDelegaciones.ItemsSource = null;
        }

        /// <summary>
        /// Maneja el evento del clic en el botón "Modificar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (dgDelegaciones.SelectedItem == null)
            {
                MessageBox.Show("No se ha seleccionado ninguna Delegación");
            }
            else
            {
                delegacionElegida = RecuperarDelegacion();
                ModificarDelegacionMunicipal_DireccionGeneral modificarDelegacion = 
                    new ModificarDelegacionMunicipal_DireccionGeneral(delegacionElegida);
                modificarDelegacion.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Recupera una delegación elegida
        /// </summary>
        /// <returns>DelegacionMunicipal</returns>
        private DelegacionMunicipal RecuperarDelegacion()
        {
            return delegacionElegida = listaDelegaciones.ElementAt<DelegacionMunicipal>(dgDelegaciones.SelectedIndex);
        }

        /// <summary>
        /// Maneja el evento del clic en el botón "Eliminar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int indiceSeleccion = dgDelegaciones.SelectedIndex;
            int idEliminar;

            if (indiceSeleccion >= 0)
            {
                DelegacionMunicipal delegacionEliminar = listaDelegaciones[indiceSeleccion];
                MessageBoxResult resultado = MessageBox.Show("¿Estas seguro de eliminar la delegación? " +
                    delegacionEliminar.nombre + "", "Confirmar acción",
                    MessageBoxButton.OKCancel);
                if (resultado == MessageBoxResult.OK)
                {
                    idEliminar = delegacionEliminar.idDelegacion;
                    
                    RecuperarUsuariosDeDelegacion(idEliminar);
                    
                    entidadesBD.DelegacionesMunicipales.Remove(delegacionEliminar);

                    try
                    {
                        entidadesBD.SaveChanges();
                        VaciarTabla();
                        LlenarTabla();
                    }
                    catch (DbEntityValidationException a)
                    {
                        foreach (var eve in a.EntityValidationErrors)
                        {
                            Console.WriteLine("Entidad \"{0}\" Estado \"{1}\" ",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Propiedad: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Para eliminar un alumno debes seleccionarlo", "Sin selección");
            }
        }

        /// <summary>
        /// Recupera una lista de los usuarios relacionados con la delegación a eliminar
        /// </summary>
        /// <param name="idEliminar"></param>
        /// <returns></returns>
        private List<Usuario> RecuperarUsuariosDeDelegacion(int idEliminar)
        {
            DbSet<Usuario> usuarios = entidadesBD.Usuarios;

            foreach (var item in usuarios)
            {
                listaUsuarios.Add(item);
            }

            foreach (var usuario in listaUsuarios)
            {
                if(usuario.DelegacionMunicipal != null)
                {
                    if (usuario.DelegacionMunicipal.idDelegacion == idEliminar)
                    {
                        usuario.DelegacionMunicipal = null;
                    }
                }
            }

            try
            { 
                entidadesBD.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidad \"{0}\" Estado \"{1}\" ",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Propiedad: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }

            return listaUsuarios;
        } 

        /// <summary>
        /// Maneja el evento del botón "Regresar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MenuAdministrativo_DireccionGeneral regresarMenu = new MenuAdministrativo_DireccionGeneral();
            regresarMenu.Show();
            this.Close();
        }
    }
}
