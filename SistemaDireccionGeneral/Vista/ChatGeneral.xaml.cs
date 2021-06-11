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

namespace SistemaDireccionGeneral.Vista
{
    /// <summary>
    /// Lógica de interacción para ChatGeneral.xaml
    /// </summary>
    public partial class ChatGeneral : Window
    {
        TcpClient clientSocket = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        Usuario usuario;
        string msjServidor = "";

        public ChatGeneral()
        {
            InitializeComponent();
            InicializarChat();
        }

        private void InicializarChat()
        {
            try
            {
                Console.WriteLine("Conectando al servidor...");
                clientSocket.Connect("127.0.0.1", 1234);
                serverStream = clientSocket.GetStream();
                string nombreChat = "Emilio"; //usuario.nombreUsuario; //Falta checar, no va a regresar nada. Solo un bonito "null"
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

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            MenuAdministrativo_DireccionGeneral ventanaMenuAdministrativo = new MenuAdministrativo_DireccionGeneral();
            ventanaMenuAdministrativo.Show();
            this.Close();
        }

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
