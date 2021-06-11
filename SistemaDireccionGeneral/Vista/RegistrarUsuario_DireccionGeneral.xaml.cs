using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace SistemaDireccionGeneral.Vista
{
    /// <summary>
    /// Autor: Emilio Antonio Alarcon Santos
    /// Lógica de interacción para RegistrarUsuario_DireccionGeneral.xaml
    /// </summary>
    public partial class RegistrarUsuario_DireccionGeneral : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        DelegacionMunicipal delegacionesMunicipales = new DelegacionMunicipal();
        List<string> listaNombreDelegaciones = new List<string>();
        List<DelegacionMunicipal> delegaciones = new List<DelegacionMunicipal>();


        public RegistrarUsuario_DireccionGeneral()
        {
            InitializeComponent();
            CargarListaUsuarios();
            CargarListaDelegaciones();
        }
        /// <summary>
        /// Carga la combobox con una lista de tipos de usuario
        /// </summary>
        public void CargarListaUsuarios()
        {
            List<string> usuarios = new List<string>();

            usuarios.Add("Transito");
            usuarios.Add("Perito");
            usuarios.Add("Administrativo");

            cbUsuarios.ItemsSource = usuarios;
        }
        /// <summary>
        /// Carga la lista de delecaciones de la base de datos y la agrega a otra 
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
            cbDelegaciones.ItemsSource = listaNombreDelegaciones;
        }
        /// <summary>
        /// Administra el reigstro de nuevo usuario
        /// </summary>
        public void ManejoRegistroUsuario()
        {
            VerificarUsuario();
            if (UsuarioValido() && !UsuarioRepetido(RecuperarUsarioNuevo()))
            {
                Usuario nuevoUsuario = new Usuario();

                nuevoUsuario.nombreUsuario = tbNombreUsuario.Text;
                nuevoUsuario.nombres = tbNombres.Text;
                nuevoUsuario.apellidoPaterno = tbApellidoPaterno.Text;
                nuevoUsuario.apellidoMaterno = tbApellidoMaterno.Text;
                nuevoUsuario.tipoUsuario = cbUsuarios.Text;
                nuevoUsuario.contraseña = pbContrasenia.Password;
                nuevoUsuario.DelegacionMunicipal = RecuperarDelegacionMunicipalSeleccionada();

                entidadesBD.Usuarios.Add(nuevoUsuario);

                entidadesBD.SaveChanges();

                MessageBox.Show("Registro Exitoso");
            }
        }

        /// <summary>
        /// Recupera del combobox, la delegaciaon seleccionado
        /// </summary>
        /// <returns>Delegacion municipal recuperada de la base de datos</returns>
        private DelegacionMunicipal RecuperarDelegacionMunicipalSeleccionada()
        {
            DelegacionMunicipal item = new DelegacionMunicipal();

            if (cbDelegaciones.SelectedItem != null)
            {
                item = delegaciones.ElementAt<DelegacionMunicipal>(cbDelegaciones.SelectedIndex);
            }
            else
            {
                MessageBox.Show("No se selecciono una delegación");
            }
            return item;
        }

        /// <summary>
        /// Maneja las validaciones del usuario que va a ser registrado
        /// </summary>
        public void VerificarUsuario()
        {
            NombreUsuarioValido();
            NombresValidos();
            ApellidoPaternoValido();
            ApellidoMaternoValido();
            TipoUsuarioValido();
            DelegacionValida();
            ContraseniaValida();
            RepetirContraseniaValida();
        }
        /// <summary>
        /// Verifica que el nombre del usuario nuevo sea valido
        /// </summary>
        /// <returns>Booleano que representa la validez del nombre del usuario</returns>
        private Boolean NombreUsuarioValido()
        {
            if (tbNombreUsuario.Text.Length >= 5 && tbNombreUsuario.Text.Length <= 15)
            {
                lbErrorNombreUsuario.Content = "";
                return true;
            }
            else
            {
                lbErrorNombreUsuario.Content = "Nombre de usuario invalido";
            }
            return false;
        }

        /// <summary>
        /// Verifica que los nombres reales del nuevo usuario sean validos
        /// </summary>
        /// <returns>Booleano que representa que sea valido o no </returns>
        private Boolean NombresValidos()
        {
            if (tbNombres.Text.Length >= 3 && tbNombres.Text.Length <= 30)
            {
                lbErrorNombres.Content = "";
                return true;
            }
            else
            {
                lbErrorNombres.Content = "Nombres invalidos";
            }
            return false;
        }

        /// <summary>
        /// Verifica que el apellido paterno de un nuevo usuario sea valido
        /// </summary>
        /// <returns> Booleano que representa el apeliido paterno de un nuevo usuario</returns>
        private Boolean ApellidoPaternoValido()
        {
            if (tbApellidoPaterno.Text.Length >= 3 && tbApellidoPaterno.Text.Length <= 30)
            {
                lbErrorApellidoPaterno.Content = "";
                return true;
            }
            else
            {
                lbErrorApellidoPaterno.Content = "Apellido invalido";
            }
            return false;
        }

        /// <summary>
        /// Verifica que el apellido materno de un nuevo usuario sea valido
        /// </summary>
        /// <returns> Booleano que representa la validdez del apellido paterno de un nuevo usuario</returns>
        private Boolean ApellidoMaternoValido()
        {
            if (tbApellidoMaterno.Text.Length >= 3 && tbApellidoMaterno.Text.Length <= 30)
            {
                lbErrorApellidoMaterno.Content = "";
                return true;
            }
            else
            {
                lbErrorApellidoMaterno.Content = "Apellido invalido";
            }
            return false;
        }

        /// <summary>
        /// Verifica que el tipo de usuario de un nuevo usuario sea valido
        /// </summary>
        /// <returns> Booeleano que representa el tipo de usaurio elegido por el usuario</returns>
        private Boolean TipoUsuarioValido()
        {
            bool tipoUsuarioValido = false;

            if (cbUsuarios.SelectedItem != null)
            {
                lbErrorTipoUsuario.Content = "";
                tipoUsuarioValido = true;
            }
            else
            {
                lbErrorTipoUsuario.Content = "Selección invalida";
            }

            return tipoUsuarioValido;
        }

        /// <summary>
        /// Verifica que la delegacion de un nuevo usuario sea valida
        /// </summary>
        /// <returns></returns>
        private Boolean DelegacionValida()
        {
            bool delegacionValida = false;

            if (cbDelegaciones.SelectedItem != null)
            {
                lbErrorDelegacion.Content = "";
                delegacionValida = true;
            }
            else
            {
                lbErrorDelegacion.Content = "Selección invalida";
            }

            return delegacionValida;
        }

        /// <summary>
        /// Verifica que la contraseña sea valida
        /// </summary>
        /// <returns>Booelano que indica la validez de la contraseña</returns>
        private Boolean ContraseniaValida()
        {
            if(pbContrasenia.Password.Length > 6 && pbContrasenia.Password.Length < 30)
            {
                lbErrorContrasenia.Content = "";
                return true;
            }
            else
            {
                lbErrorContrasenia.Content = "Contraseña invalida";
            }
            return false;
        }

        /// <summary>
        /// Verifica que la contraseña coincida con la contraseña repetida posteriormente
        /// </summary>
        /// <returns>Booleano que representa la validez de la contraseña</returns>
        private Boolean RepetirContraseniaValida()
        {
            if(pbRepetirContrasenia.Password == "")
            {
                lbErrorRepetirContrasenia.Content = "Contraseña repetida invalida";
                return false;
            }
            if(pbContrasenia.Password == pbRepetirContrasenia.Password)
            {
                lbErrorRepetirContrasenia.Content = "";
                return true;
            }
            else
            {
                lbErrorRepetirContrasenia.Content = "Contraseña repetida erronea";
            }
            return false;
        }
        /// <summary>
        /// Verifica que el usuario sea valido
        /// </summary>
        /// <returns>Booleano que representa la validez del usuario</returns>
        private Boolean UsuarioValido()
        {
            bool aux = true;
            int contador = 8;

            while (contador >= 1 && aux==true)
            {
                switch (contador)
                {
                    case 8:
                        aux = NombreUsuarioValido();
                        break;
                    case 7:
                        aux = NombresValidos();
                    break;
                    case 6:
                        aux = ApellidoPaternoValido();
                        break;
                    case 5:
                        aux = ApellidoMaternoValido();
                        break;
                    case 4:
                        aux = TipoUsuarioValido();
                        break;
                    case 3:
                        aux = DelegacionValida();
                        break;
                    case 2:
                        aux = ContraseniaValida();
                        break;
                    case 1:
                        aux = RepetirContraseniaValida();
                        break;
                }
                contador--;
            }

            Console.WriteLine("Si llega a validar al usuario");

            return aux;
        }
        /// <summary>
        /// Maneja el evento del clic en el boton "Registrar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            ManejoRegistroUsuario();
        }
        /// <summary>
        /// Maneja el evento del clic en el boton "Cancelar"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MenuAdministrativo_DireccionGeneral ventanaMenuAdministrativo = new MenuAdministrativo_DireccionGeneral();
            ventanaMenuAdministrativo.Show();
            this.Close();
        }
        /// <summary>
        /// Verifica si el usuario esta repetido
        /// </summary>
        /// <param name="nuevoUsuario"></param>
        /// <returns></returns>
        private Boolean UsuarioRepetido(Usuario nuevoUsuario)
        {
            bool usuarioRepetido = false;

            if (entidadesBD.Usuarios.SingleOrDefault(
                usuario =>
                usuario.nombreUsuario == nuevoUsuario.nombreUsuario &&
                usuario.nombres == nuevoUsuario.nombres &&
                usuario.apellidoPaterno == nuevoUsuario.apellidoPaterno &&
                usuario.apellidoMaterno == nuevoUsuario.apellidoMaterno &&
                usuario.tipoUsuario == nuevoUsuario.tipoUsuario) != null)
            {
                usuarioRepetido = true;
            }
            return usuarioRepetido;
        }
        /// <summary>
        /// Recupera un usuario nuevo, con los elementos de la pantalla como atributos
        /// </summary>
        /// <returns>Usuario nuevo</returns>
        private Usuario RecuperarUsarioNuevo()
        {
            Usuario verificarUsuario = new Usuario();

            verificarUsuario.nombreUsuario = tbNombreUsuario.Text;
            verificarUsuario.nombres = tbNombres.Text;
            verificarUsuario.apellidoPaterno = tbApellidoPaterno.Text;
            verificarUsuario.apellidoMaterno = tbApellidoMaterno.Text;
            verificarUsuario.tipoUsuario = cbUsuarios.Text;
            verificarUsuario.contraseña = pbContrasenia.Password;
            verificarUsuario.DelegacionMunicipal = RecuperarDelegacionMunicipalSeleccionada();

            return verificarUsuario;
        }
    }
}
