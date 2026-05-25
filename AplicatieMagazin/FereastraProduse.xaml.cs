using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LogicaMagazine;
using ModeleMagazin;

namespace AplicatieMagazin
{
    public partial class FereastraProduse : Window
    {
        private MainWindowViewModel viewModel;
        private IStocareProduse adminProduse;
        private IStocareMagazine adminMagazine;

        public FereastraProduse()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel();
            this.DataContext = viewModel;

            adminProduse = new AdministrareProduseFisier("Produse.txt");
            adminMagazine = new AdministrareMagazineFisier("Magazine.txt");

            cmbCategorie.ItemsSource = Enum.GetValues(typeof(CategorieProdus));
            IncarcaMagazineInInterfata();
        }

        private void IncarcaMagazineInInterfata()
        {
            pnlMagazine.Children.Clear();
            var magazineExistente = adminMagazine.GetMagazine();
            foreach (var magazin in magazineExistente)
            {
                RadioButton rb = new RadioButton
                {
                    Content = magazin.Brand,
                    Tag = magazin,
                    GroupName = "Magazine",
                    Margin = new Thickness(0, 0, 15, 0),
                    VerticalAlignment = VerticalAlignment.Center
                };
                pnlMagazine.Children.Add(rb);
            }
        }

        private void OnAdaugaClick(object sender, RoutedEventArgs e)
        {
            if (!viewModel.EsteFormularValid || cmbCategorie.SelectedItem == null || PreluareMagazinBifat() == 0)
            {
                txtStatus.Text = "Completați corect datele roșii, prețul, categoria și magazinul!";
                txtStatus.Foreground = Brushes.Red;
                txtStatus.Visibility = Visibility.Visible;
                return;
            }

            try
            {
                Produs p = new Produs(
                    new Random().Next(1, 10000),
                    PreluareMagazinBifat(),
                    viewModel.ProdusCurent.Nume,
                    viewModel.ProdusCurent.Pret,
                    (CategorieProdus)cmbCategorie.SelectedItem,
                    PreluareEticheteBifate(),
                    viewModel.ProdusCurent.Culoar,
                    viewModel.ProdusCurent.Raft
                );

                adminProduse.AddProdus(p);
                txtStatus.Text = $"Produsul '{p.Nume}' a fost salvat!";
                txtStatus.Foreground = Brushes.Green;
                txtStatus.Visibility = Visibility.Visible;

                viewModel.ProdusCurent = new Produs(); // Reset formular
                cmbCategorie.SelectedIndex = -1;
                foreach (var copil in pnlMagazine.Children)
                    if (copil is RadioButton rb) rb.IsChecked = false;
            }
            catch (Exception ex)
            {
                txtStatus.Text = "Eroare: " + ex.Message;
                txtStatus.Foreground = Brushes.Red;
                txtStatus.Visibility = Visibility.Visible;
            }
        }

        private int PreluareMagazinBifat()
        {
            foreach (var copil in pnlMagazine.Children)
                if (copil is RadioButton rb && rb.IsChecked == true) return ((Magazin)rb.Tag).Id;
            return 0;
        }

        private EticheteProdus PreluareEticheteBifate()
        {
            EticheteProdus e = EticheteProdus.Niciuna;
            if (chkFaraZahar.IsChecked == true) e |= EticheteProdus.FaraZahar;
            if (chkFaraGluten.IsChecked == true) e |= EticheteProdus.FaraGluten;
            if (chkLocal.IsChecked == true) e |= EticheteProdus.ProdusLocal;
            if (chkOferta.IsChecked == true) e |= EticheteProdus.OfertaSpeciala;
            return e;
        }
    }
}