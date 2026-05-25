using System;
using System.Windows;
using LogicaMagazine;

namespace AplicatieMagazin
{
    public partial class MainWindow : Window
    {
        private IStocareProduse adminProduse;

        public MainWindow()
        {
            InitializeComponent();
            adminProduse = new AdministrareProduseFisier("Produse.txt");
        }

        private void OnDeschideProduseClick(object sender, RoutedEventArgs e)
        {
            FereastraProduse frm = new FereastraProduse();
            frm.ShowDialog();
        }

        private void OnDeschideMagazineClick(object sender, RoutedEventArgs e)
        {
            FereastraMagazine frm = new FereastraMagazine();
            frm.ShowDialog();
        }

        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            txtCautaNume.Text = string.Empty;
            lstRezultateCautare.Items.Clear();
            txtStatusCautare.Visibility = Visibility.Collapsed;
        }

        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OnCautaClick(object sender, RoutedEventArgs e)
        {
            string cautat = txtCautaNume.Text.Trim();
            lstRezultateCautare.Items.Clear();
            txtStatusCautare.Visibility = Visibility.Collapsed;

            if (string.IsNullOrEmpty(cautat))
            {
                txtStatusCautare.Text = "Vă rugăm să introduceți un termen de căutare!";
                txtStatusCautare.Visibility = Visibility.Visible;
                return;
            }

            bool gasit = false;
            foreach (var p in adminProduse.GetProduse())
            {
                if (p.Nume.IndexOf(cautat, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    lstRezultateCautare.Items.Add(p);
                    gasit = true;
                }
            }

            if (!gasit)
            {
                txtStatusCautare.Text = "Nu s-a găsit niciun produs cu acest nume.";
                txtStatusCautare.Visibility = Visibility.Visible;
            }
        }
    }
}