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

namespace SistemaDelegacionesMunicipales.Vista
{
    /// <summary>
    /// Autor: Emilio Antonio Alarcon Santos
    /// Lógica de interacción para IniciarSesion.xaml
    /// </summary>
    public partial class IniciarSesion : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        Usuario usuario;
        List<Usuario> usuarios = new List<Usuario>();
        Usuario usuarioEncontrado;
        List<DelegacionMunicipal> delegaciones = new List<DelegacionMunicipal>();
        List<string> listaNombreDelegaciones = new List<string>();

        public IniciarSesion()
        {
            InitializeComponent();
            CargarListaDelegaciones();
        }

        /// <summary>
        /// Controlador para manejar el incio de sesion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            ManejadorLogin();
        }

        /// <summary>
        /// Comprueba el tipo de usuario y lo deja ingresar al sistema segun el tipo de usuario que sea
        /// </summary>
        public void ManejadorLogin()
        {
            VerificarInformacionUsuario();

            if (BuscarValoresUsuario(tbUsername.Text) == true)
            {
                RecuperarUsuario(tbUsername.Text);
                MessageBox.Show("Bienvenido al sistema: " + usuarioEncontrado.nombres + " " +
                    usuarioEncontrado.apellidoPaterno + " " + usuarioEncontrado.apellidoMaterno, "Usuario encontrado");
                if (usuarioEncontrado.tipoUsuario == "Administrativo")
                {
                    IrPantallaPrincipalAdministrativo();
                }else if(usuarioEncontrado.tipoUsuario == "Transito")
                {
                    IrPantallaMenuAgente();
                }
            }
        }

        /// <summary>
        /// Verifica la informacion ingresada por el usuario, el caso de que sea valida podra iniciar sesion
        /// </summary>
        private void VerificarInformacionUsuario()
        {
            VerificarUsername();
            VerificarContrasenia();
        }

        /// <summary>
        /// Se encarga de verificar 
        /// </summary>
        /// <returns></returns>
        private Boolean VerificarUsername()
        {
            bool verificar = false;
            if (tbUsername.Text.Length >= 5 && tbUsername.Text.Length <= 15)
            {
                lbErrorUsername.Content = "";
                verificar = true;
            }
            else
            {
                MessageBox.Show("Datos Erroneos, por favor verifique.", "Datos Erroneos");
            }
            return verificar;
        }

        /// <summary>
        /// Comprueba que la contraseña ingresada por el suario es valida en su tamaño
        /// </summary>
        /// <returns></returns>
        private Boolean VerificarContrasenia()
        {
            bool verificar = false;
            if (pbContrasenia.Password.Length > 6 && pbContrasenia.Password.Length < 30)
            {
                lbErrorContrasenia.Content = "";
                verificar = true;
            }
            else
            {
                MessageBox.Show("Datos Erroneos, por favor verifique.", "Datos Erroneos");
            }
            return verificar;
        }

        /// <summary>
        /// Controlador para ir a la ventana PrincipalAdministrativo.xaml
        /// </summary>
        private void IrPantallaPrincipalAdministrativo()
        {
            PrincipalAdministrativo ventanaPrincipalAdministrativo = new PrincipalAdministrativo(usuarioEncontrado);
            ventanaPrincipalAdministrativo.Show();
            this.Close();
        }

        /// <summary>
        /// Controlador para ir a la ventana MenuAgente.xaml 
        /// </summary>
        private void IrPantallaMenuAgente()
        {
            MenuAgente ventanaMenuAgente = new MenuAgente(usuarioEncontrado);
            ventanaMenuAgente.Show();
            this.Close();
        }

        /// <summary>
        /// Busca la informacion ingresada por el usuario para verificar que existe y es verdadera
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public bool BuscarValoresUsuario(string nombre)
        {
            bool existeUsuario = false;

            foreach (var item in entidadesBD.Usuarios)
            {
                usuarios.Add(item);
            }

            foreach (var item in usuarios)
            {
                if(nombre == item.nombreUsuario && pbContrasenia.Password == item.contraseña)
                {
                    if(item.DelegacionMunicipal != null)
                    {
                        if (item.DelegacionMunicipal.nombre == (string)cbDelegacion.SelectedValue)
                        {
                            return existeUsuario = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Delegación invalida, por favor comuniquese con la dirección general", "Delegación invalida");
                        return existeUsuario = false;
                    }
                }
            }
            if(existeUsuario == false)
            {
                MessageBox.Show("Datos incorrectos, porfavor verifique sus datos", "Datos incorrectos");
            }
            return existeUsuario;
        }

        /// <summary>
        /// Recupera la instacia del usuario
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public Usuario RecuperarUsuario(string nombre)
        {
            usuarios.Clear();

            foreach (var item in entidadesBD.Usuarios)
            {
                usuarios.Add(item);
            }

            foreach (var item in usuarios)
            {
                if (nombre == item.nombreUsuario)
                {
                    return usuarioEncontrado = item;
                }
            }

            return usuarioEncontrado;
        }

        /// <summary>
        /// Carga la lista de delegaciones que hay registradas en la base de datos del sistema
        /// </summary>
        public void CargarListaDelegaciones()
        {
            foreach (var item in entidadesBD.DelegacionesMunicipales)
            {
                delegaciones.Add(item);
            }

            foreach (var item in delegaciones)
            {
                listaNombreDelegaciones.Add(item.nombre);
            }
            cbDelegacion.ItemsSource = listaNombreDelegaciones;
        }
    }
}
