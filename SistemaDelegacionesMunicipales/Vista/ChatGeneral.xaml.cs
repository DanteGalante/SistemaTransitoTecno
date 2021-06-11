using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
    /// Autor: Emilio Antonio Santos Alarcon
    /// Lógica de interacción para ChatGeneral.xaml
    /// </summary>
    public partial class ChatGeneral : Window
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        Usuario usuarioIniciado;
        string msjServidor = "";

        public ChatGeneral(Usuario usuarioIniciado)
        {
            InitializeComponent();
            this.usuarioIniciado = usuarioIniciado;
            InicializarChat();
        }

        /// <summary>
        /// Se encarga de inciarlizar el chat mediante la creacion del cliente y conectandolo al servior de
        /// mensajes de chat donde se puede indentificar
        /// </summary>
        private void InicializarChat()
        {
            try
            {
                Console.WriteLine("Conectando al servidor...");
                //local host y un puerto
                clientSocket.Connect("127.0.0.1", 1234);
                serverStream = clientSocket.GetStream();
                string nombreChat = usuarioIniciado.nombreUsuario;
                byte[] outStream = Encoding.ASCII.GetBytes(nombreChat + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
                Thread threadListen = new Thread(escucharMensajes);
                threadListen.Start();

                lbNombreCliente.Content = "Conectado como: " + nombreChat;
            }
            catch (Exception er)
            {
                MessageBox.Show("Error: " + er.Message, "Error de conexión al servidor");
            }
        }

        /// <summary>
        /// Controlador para finalizar la ejecucion del programa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Metodo para el envio de mensajes mediante un sockets
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnviarMsj_Click(object sender, RoutedEventArgs e)
        {
            if (tbMensaje.Text.Length > 0)
            {
                byte[] outStream = Encoding.ASCII.GetBytes(tbMensaje.Text + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
                tbMensaje.Text = "";
            }
            else
            {
                MessageBox.Show("Debes escribir un mensaje para ser enviado por el chat", "Mensaje obligatorio");
            }
        }

        /// <summary>
        /// Se encarga de escuchar los mensajes que recibe de otro mienbro del chat
        /// </summary>
        public void escucharMensajes()
        {
            while (true)
            {
                serverStream = clientSocket.GetStream();
                byte[] inStream = new byte[65537];
                serverStream.Read(inStream, 0, clientSocket.ReceiveBufferSize);

                string returndata = Encoding.ASCII.GetString(inStream);
                msjServidor += returndata + " ";

                Console.WriteLine("Msj del servidor: " + returndata);

                tbContenido.Dispatcher.Invoke((ThreadStart)delegate
                {
                    tbContenido.Clear();
                    tbContenido.Text = msjServidor;
                });
            }
        }
    }
}
