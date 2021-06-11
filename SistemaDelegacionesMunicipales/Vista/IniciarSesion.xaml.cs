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

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            ManejadorLogin();
        }

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

        private void VerificarInformacionUsuario()
        {
            VerificarUsername();
            VerificarContrasenia();
        }

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

        private void IrPantallaPrincipalAdministrativo()
        {
            PrincipalAdministrativo ventanaPrincipalAdministrativo = new PrincipalAdministrativo(usuarioEncontrado);
            ventanaPrincipalAdministrativo.Show();
            this.Close();
        }

        private void IrPantallaMenuAgente()
        {
            MenuAgente ventanaMenuAgente = new MenuAgente();
            ventanaMenuAgente.Show();
            this.Close();
        }

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
