using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ChatServidor
{
    class SocketServer
    {
        public static Hashtable listaClientes = new Hashtable();
        static int tamanioBuffer = 65537;

        public static void Conectar()
        {
            TcpListener serverSocket = new TcpListener(1234);
            TcpClient clientSocket = default(TcpClient);
            int contador = 0;

            serverSocket.Start();
            Console.WriteLine("Servidor de Chat inicializado...");

            while (true)
            {
                contador++;
                clientSocket = serverSocket.AcceptTcpClient();
                byte[] bytesRecibidos = new byte[tamanioBuffer];
                string msjCliente = "";
                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Read(bytesRecibidos, 0, clientSocket.ReceiveBufferSize);
                msjCliente = Encoding.ASCII.GetString(bytesRecibidos);
                msjCliente = msjCliente.Substring(0, msjCliente.IndexOf("$"));
                Console.WriteLine("Msj cliente: " + msjCliente);

                listaClientes.Add(msjCliente, clientSocket);
                Console.WriteLine(msjCliente + "se unio al chat...");

                //Método de notificación a todos
                notificarClientes(msjCliente + " se ha unido", msjCliente, false);

                //Asignación de la clase responsable del socket cliente
                ClienteRemoto clienteRemoto = new ClienteRemoto();
                clienteRemoto.inicializarCliente(clientSocket, msjCliente);
            }
        }
        public static void notificarClientes(String mensaje, string nomCliente, bool flag)
        {
            foreach (DictionaryEntry Item in listaClientes)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)Item.Value;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;

                if (flag == true)
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(nomCliente + " escribio: " + mensaje);
                }
                else
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(mensaje);
                }

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush();
            }
        }
    }

    public class ClienteRemoto
    {
        TcpClient clientSocket;
        string nomCliente;
        int tamanioBuffer = 65537;

        public void inicializarCliente(TcpClient clientSocket, string nomCliente)
        {
            this.clientSocket = clientSocket;
            this.nomCliente = nomCliente;
            Thread ctThread = new Thread(escucharChat);
            ctThread.Start();
        }

        public void escucharChat()
        {
            byte[] bytesFrom = new byte[tamanioBuffer];
            string datoFromCliente = null;
            while (true)
            {
                try
                {
                    NetworkStream networkStream = clientSocket.GetStream();
                    networkStream.Read(bytesFrom, 0, clientSocket.ReceiveBufferSize);
                    datoFromCliente = Encoding.ASCII.GetString(bytesFrom);
                    datoFromCliente = datoFromCliente.Substring(0, datoFromCliente.IndexOf("$"));
                    Console.WriteLine("Del cliente - " + nomCliente + ":" + datoFromCliente);

                    SocketServer.notificarClientes(datoFromCliente, nomCliente, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
