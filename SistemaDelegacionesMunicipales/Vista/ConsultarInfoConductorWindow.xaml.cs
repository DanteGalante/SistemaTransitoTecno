using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for ConsultarInfoConductor.xaml
    /// </summary>
    public partial class ConsultarInfoConductorWindow : Window
    {
        BDTransitoEntities bdTransito = new BDTransitoEntities();
        List<Conductor> conductores = new List<Conductor>();
        public ConsultarInfoConductorWindow()
        {
            InitializeComponent();
            LlenarTabla();
        }
        
        /**
         * Este metodo se encarga de llenar el datagrid dg_conductor con informacion de los conductores sacada de la base de datos
         */
        private void LlenarTabla()
        {
            try
            {
                foreach (Conductor conductorBD in bdTransito.Conductores)
                {
                    conductores.Add(conductorBD);
                }

                dg_conductor.ItemsSource = conductores;
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error en la conexion con la base de datos", "Error", MessageBoxButton.OK);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
