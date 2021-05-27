﻿using BaseDeDatos;
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
    /// Lógica de interacción para ModificarUsuario_DireccionGeneral.xaml
    /// </summary>
    public partial class ModificarUsuario_DireccionGeneral : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        DelegacionMunicipal delegacionesMunicipales = new DelegacionMunicipal();
        List<string> listaNombreDelegaciones = new List<string>();
        List<DelegacionMunicipal> delegaciones = new List<DelegacionMunicipal>();
        Usuario usuarioElegido;

        public ModificarUsuario_DireccionGeneral(Usuario usuarioElegido)
        {
            InitializeComponent();
            this.usuarioElegido = usuarioElegido;
            CargarListaUsuarios();
            CargarListaDelegaciones();
            CargarDatosUsuario();
        }

        public void CargarListaUsuarios()
        {
            List<string> usuarios = new List<string>();

            usuarios.Add("Transito");
            usuarios.Add("Perito");
            usuarios.Add("Administrativo");

            cbUsuarios.ItemsSource = usuarios;
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
            cbDelegaciones.ItemsSource = listaNombreDelegaciones;
        }

        public void ManejoModificacionUsuario()
        {
            VerificarUsuario();
            if (UsuarioValido() && !UsuarioRepetido(RecuperarUsarioNuevo()))
            {
                Usuario modificarUsuario = entidadesBD.Usuarios.Find(usuarioElegido.idUsuario);

                modificarUsuario.nombreUsuario = tbNombreUsuario.Text;
                modificarUsuario.nombres = tbNombres.Text;
                modificarUsuario.apellidoPaterno = tbApellidoPaterno.Text;
                modificarUsuario.apellidoMaterno = tbApellidoMaterno.Text;
                modificarUsuario.tipoUsuario = cbUsuarios.Text;
                modificarUsuario.contraseña = pbContrasenia.Password;
                modificarUsuario.DelegacionMunicipal = RecuperarDelegacionMunicipalSeleccionada();

                entidadesBD.SaveChanges();

                MessageBox.Show("Modificación de usuario Exitosa");
            }
            else
            {
                MessageBox.Show("Usuario ya existente en la base de datos");
            }
        }

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

        private Boolean ContraseniaValida()
        {
            if (pbContrasenia.Password.Length > 6 && pbContrasenia.Password.Length < 30)
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

        private Boolean RepetirContraseniaValida()
        {
            if (pbRepetirContrasenia.Password == "")
            {
                lbErrorRepetirContrasenia.Content = "Contraseña repetida invalida";
                return false;
            }
            if (pbContrasenia.Password == pbRepetirContrasenia.Password)
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

        private Boolean UsuarioValido()
        {
            bool aux = true;
            int contador = 8;

            while (contador >= 1 && aux == true)
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

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            MenuAdministrativo_DireccionGeneral ventanaMenuAdministrativo = new MenuAdministrativo_DireccionGeneral();
            ventanaMenuAdministrativo.Show();
            this.Close();
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            ManejoModificacionUsuario();
        }

        public int RecuperarDelegacion()
        {
            int res = 0;
            if (usuarioElegido.DelegacionMunicipal != null)
            {
                res = usuarioElegido.DelegacionMunicipal.idDelegacion;
                return res;
            }

            return res;
        }

        public void CargarDatosUsuario()
        {
            tbNombreUsuario.Text = usuarioElegido.nombreUsuario;
            tbNombres.Text = usuarioElegido.nombres;
            tbApellidoPaterno.Text = usuarioElegido.apellidoPaterno;
            tbApellidoMaterno.Text = usuarioElegido.apellidoMaterno;
            cbUsuarios.Text = usuarioElegido.tipoUsuario;
            pbContrasenia.Password = usuarioElegido.contraseña;
            cbDelegaciones.SelectedIndex = RecuperarDelegacion();
        }

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
