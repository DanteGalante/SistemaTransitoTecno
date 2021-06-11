﻿using System;
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
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class MenuAgente : Window
    {
        public MenuAgente()
        {
            InitializeComponent();
        }

        private void GenerarReporte_Click(object sender, RoutedEventArgs e)
        {
            LevantarReporte levantarReporte = new LevantarReporte();
            this.Close();
            levantarReporte.Show();

        }

        private void HistorialReportes_Click(object sender, RoutedEventArgs e)
        {
            Reportes reportes = new Reportes();

            this.Close();
            reportes.Show();
        }

        private void Salir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
