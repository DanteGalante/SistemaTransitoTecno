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
    /// Lógica de interacción para ConsultarUsuario_DireccionGeneral.xaml
    /// </summary>
    public partial class ConsultarUsuario_DireccionGeneral : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        Usuario usuarioElegido;
        List<Usuario> listaUsuarios = new List<Usuario>();

        public ConsultarUsuario_DireccionGeneral()
        {
            InitializeComponent();
            LlenarTabla();
        }

        /// <summary>
        /// Llena la tabla con una lista de usuarios que llena en este mismo método
        /// </summary>
        private void LlenarTabla()
        {
            DbSet<Usuario> usuarios = entidadesBD.Usuarios;

            foreach (var item in usuarios)
            {
                listaUsuarios.Add(item);
            }
            dgUsuarios.AutoGenerateColumns = false;
            dgUsuarios.ItemsSource = listaUsuarios;
        }

        /// <summary>
        /// Vacía la tabla y la lista de los usuarios
        /// </summary>
        private void VaciarTabla()
        {
            listaUsuarios.Clear();
            dgUsuarios.AutoGenerateColumns = false;
            dgUsuarios.ItemsSource = null;
        }


        /// <summary>
        /// Manejador del evento del  botón "Regresar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegresar_Click(object sender, RoutedEventArgs e)
        {
            MenuAdministrativo_DireccionGeneral menuAdministrativo = new MenuAdministrativo_DireccionGeneral();
            menuAdministrativo.Show();
            this.Close();
        }


        /// <summary>
        /// Manejador del evento del botón "Modificar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsuarios.SelectedItem == null)
            {
                MessageBox.Show("No se ha seleccionado ningún Usuario");
            }
            else
            {
                usuarioElegido = RecuperarUsuario();
                ModificarUsuario_DireccionGeneral modificarUsuario = 
                    new ModificarUsuario_DireccionGeneral(usuarioElegido);
                modificarUsuario.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Recupera al usuario elegido
        /// </summary>
        /// <returns>Usuario</returns>
        private Usuario RecuperarUsuario()
        {
            return usuarioElegido = listaUsuarios.ElementAt<Usuario>(dgUsuarios.SelectedIndex);
        }

        /// <summary>
        /// Manejador del evento botón "Eliminar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            int indiceSeleccion = dgUsuarios.SelectedIndex;
            if (indiceSeleccion >= 0)
            {
                Usuario usuarioEliminar = listaUsuarios[indiceSeleccion];
                MessageBoxResult resultado = MessageBox.Show("¿Estas seguro de eliminar al usuario?" +
                    usuarioEliminar.nombres + usuarioEliminar.apellidoPaterno + usuarioEliminar.apellidoMaterno +
                    "?", "Confirmar acción",
                    MessageBoxButton.OKCancel);
                if (resultado == MessageBoxResult.OK)
                {
                    entidadesBD.Usuarios.Remove(usuarioEliminar);

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
                MessageBox.Show("Para eliminar un usuario debes seleccionarlo", "Sin selección");
            }
        }
    }
}
