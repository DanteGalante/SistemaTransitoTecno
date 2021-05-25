using BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Lógica de interacción para ConsultarDelegacionMunicipal_DireccionGeneral.xaml
    /// </summary>
    public partial class ConsultarDelegacionMunicipal_DireccionGeneral : Window
    {
        BDTransitoEntities entidadesBD = new BDTransitoEntities();
        List<DelegacionMunicipal> listaDelegaciones = new List<DelegacionMunicipal>();

        public ConsultarDelegacionMunicipal_DireccionGeneral()
        {
            InitializeComponent();
            LlenarTabla();
        }

        private void LlenarTabla()
        {
            DbSet<DelegacionMunicipal> delegaciones = entidadesBD.DelegacionesMunicipales;
            
            foreach (var item in delegaciones)
            {
                listaDelegaciones.Add(item);
            }
            dgDelegaciones.AutoGenerateColumns = false;
            dgDelegaciones.ItemsSource = listaDelegaciones;
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
