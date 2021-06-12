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
    /// Lógica de interacción para IniciarSesion.xaml
    /// </summary>
    public partial class IniciarSesion : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        Usuario usuario;
        List<Usuario> usuarios = new List<Usuario>();
        Usuario usuarioEncontrado;

        public IniciarSesion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Recibe el evento del clic y direcciona hacía el manejador del Login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            ManejadorLogin();
        }

        /// <summary>
        /// LLeva a cabo las verificaciones correspondientes con la finalidad de decidir si el usuario ingresado,
        /// puede o no entrar al sistema y si es el caso saber a que ventana será dirigido.
        /// </summary>
        public void ManejadorLogin()
        {
            VerificarInformacionUsuario();

            if (BuscarValoresUsuario(tbUsername.Text) == true)
            {
                RecuperarUsuario(tbUsername.Text);
                if (usuarioEncontrado.tipoUsuario == "Administrativo")
                {
                    MessageBox.Show("Bienvenido al sistema: " + usuarioEncontrado.nombres + " " +
                        usuarioEncontrado.apellidoPaterno + " " + 
                        usuarioEncontrado.apellidoMaterno, "Usuario encontrado");
                    IrPantallaMenuAdministrativo();
                }
                else if (usuarioEncontrado.tipoUsuario == "Perito")
                {
                    MessageBox.Show("Bienvenido al sistema: " + usuarioEncontrado.nombres + " " +
                        usuarioEncontrado.apellidoPaterno + " " + usuarioEncontrado.apellidoMaterno,
                        "Usuario encontrado");
                    IrPantallaMenuPerito();
                }
                else
                {
                    MessageBox.Show("Tipo de usuario no valido, comuniquese con la dirección general", 
                        "Tipo usuario invalido");
                }
            }
        }

        /// <summary>
        /// Llama a métodos que verificaran la información del usuario ingresado
        /// </summary>
        private void VerificarInformacionUsuario()
        {
            VerificarUsername();
            VerificarContrasenia();
        }

        /// <summary>
        /// Verifica si el username ingresado cumple con las características correspondientes
        /// </summary>
        /// <returns>Boolean</returns>
        private Boolean VerificarUsername()
        {
            bool verificar = false;
            if (tbUsername.Text.Length >= 5 && tbUsername.Text.Length <= 15)
            {
                verificar = true;
            }
            else
            {
                MessageBox.Show("Datos Erroneos, por favor verifique.", "Datos Erroneos");
            }
            return verificar;
        }

        /// <summary>
        /// Verifica si la contraseña ingresada cumple con las características correspondientes
        /// </summary>
        /// <returns>Boolean</returns>
        private Boolean VerificarContrasenia()
        {
            bool verificar = false;
            if (pbContrasenia.Password.Length > 6 && pbContrasenia.Password.Length < 30)
            {
                verificar = true;
            }
            else
            {
                MessageBox.Show("Datos Erroneos, por favor verifique.", "Datos Erroneos");
            }
            return verificar;
        }

        /// <summary>
        /// Verifica que los valores introducidos por el usuario sean correspondientes a un usuario
        /// dentro de la base de datos
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>bool</returns>
        public bool BuscarValoresUsuario(string nombre)
        {
            bool existeUsuario = false;

            foreach (var item in entidadesBD.Usuarios)
            {
                usuarios.Add(item);
            }

            foreach (var item in usuarios)
            {
                if (nombre == item.nombreUsuario && pbContrasenia.Password == item.contraseña)
                {
                    return existeUsuario = true;
                }
            }
            if (existeUsuario == false)
            {
                MessageBox.Show("Datos incorrectos, porfavor verifique sus datos", "Datos incorrectos");
            }
            return existeUsuario;
        }

        /// <summary>
        /// Recupera al usuariEncontrado que sera el que ingresara en el sistema
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns>Usuario</returns>
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
        /// Es la navegabilidad que dirige a la pantalla de de Menú administrativo
        /// </summary>
        private void IrPantallaMenuAdministrativo()
        {
            MenuAdministrativo_DireccionGeneral ventanaMenuAdministrativo = 
                new MenuAdministrativo_DireccionGeneral(usuarioEncontrado);
            ventanaMenuAdministrativo.Show();
            this.Close();
        }

        /// <summary>
        /// Es la navegabilidad que dirige a la pantalla de Menú perito
        /// </summary>
        public void IrPantallaMenuPerito()
        {
            MenuPerito ventanaMenuAdministrativo = new MenuPerito(usuarioEncontrado);
            ventanaMenuAdministrativo.Show();
            this.Close();
        }
    }
}
