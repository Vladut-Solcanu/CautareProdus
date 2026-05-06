using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LogicaMagazine;
using ModeleMagazin;

namespace AplicatieMagazin
{
    public partial class MainWindow : Window
    {
        private const int MAX_LUNGIME_NUME = 15;
        private const int MAX_CULOAR = 20;
        private const int MAX_RAFT = 50;

        private IStocareProduse adminProduse;
        private IStocareMagazine adminMagazine;

        public MainWindow()
        {
            InitializeComponent();

            adminProduse = new AdministrareProduseFisier("Produse.txt");
            adminMagazine = new AdministrareMagazineFisier("Magazine.txt");

            var magazineExistente = adminMagazine.GetMagazine();
            if (magazineExistente.Count == 0)
            {
                adminMagazine.AddMagazin(new Magazin(1, "Lidl", "Centru"));
                adminMagazine.AddMagazin(new Magazin(2, "Kaufland", "Vest"));
                adminMagazine.AddMagazin(new Magazin(3, "Profi", "Est"));
                magazineExistente = adminMagazine.GetMagazine();
            }

            cmbCategorie.ItemsSource = Enum.GetValues(typeof(CategorieProdus));

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
            ReseteazaErori();

            if (ValideazaDateProdus())
            {
                try
                {
                    string nume = txtNume.Text;
                    CategorieProdus cat = (CategorieProdus)cmbCategorie.SelectedItem;
                    int culoar = int.Parse(txtCuloar.Text);
                    int raft = int.Parse(txtRaft.Text);

                    Magazin magazinSelectat = null;
                    foreach (var copil in pnlMagazine.Children)
                    {
                        if (copil is RadioButton rb && rb.IsChecked == true)
                        {
                            magazinSelectat = (Magazin)rb.Tag;
                            break;
                        }
                    }

                    EticheteProdus etichete = EticheteProdus.Niciuna;
                    if (chkFaraZahar.IsChecked == true) etichete |= EticheteProdus.FaraZahar;
                    if (chkFaraGluten.IsChecked == true) etichete |= EticheteProdus.FaraGluten;
                    if (chkLocal.IsChecked == true) etichete |= EticheteProdus.ProdusLocal;
                    if (chkOferta.IsChecked == true) etichete |= EticheteProdus.OfertaSpeciala;

                    Random rnd = new Random();
                    int idNou = rnd.Next(1, 10000);

                    Produs produsNou = new Produs(idNou, magazinSelectat.Id, nume, cat, etichete, culoar, raft);
                    adminProduse.AddProdus(produsNou);

                    txtStatus.Text = $"Produsul '{nume}' a fost salvat în {magazinSelectat.Brand}!";
                    txtStatus.Foreground = Brushes.Green;
                    txtStatus.Visibility = Visibility.Visible;

                    CurataCampuri();
                }
                catch (Exception ex)
                {
                    txtStatus.Text = "Eroare la salvare: " + ex.Message;
                    txtStatus.Foreground = Brushes.Red;
                    txtStatus.Visibility = Visibility.Visible;
                }
            }
        }

        private bool ValideazaDateProdus()
        {
            bool dateValide = true;

            bool magazinSelectat = false;
            foreach (var copil in pnlMagazine.Children)
            {
                if (copil is RadioButton rb && rb.IsChecked == true)
                {
                    magazinSelectat = true; break;
                }
            }
            if (!magazinSelectat)
            {
                lblMagazin.Foreground = Brushes.Red;
                errMagazin.Visibility = Visibility.Visible;
                dateValide = false;
            }

            if (string.IsNullOrWhiteSpace(txtNume.Text) || txtNume.Text.Length > MAX_LUNGIME_NUME)
            {
                lblNume.Foreground = Brushes.Red;
                errNume.Visibility = Visibility.Visible;
                dateValide = false;
            }

            if (cmbCategorie.SelectedItem == null)
            {
                lblCategorie.Foreground = Brushes.Red;
                errCategorie.Visibility = Visibility.Visible;
                dateValide = false;
            }

            if (!int.TryParse(txtCuloar.Text, out int culoarVal) || culoarVal <= 0 || culoarVal > MAX_CULOAR)
            {
                lblCuloar.Foreground = Brushes.Red;
                errCuloar.Visibility = Visibility.Visible;
                dateValide = false;
            }

            if (!int.TryParse(txtRaft.Text, out int raftVal) || raftVal <= 0 || raftVal > MAX_RAFT)
            {
                lblRaft.Foreground = Brushes.Red;
                errRaft.Visibility = Visibility.Visible;
                dateValide = false;
            }

            return dateValide;
        }

        private void ReseteazaErori()
        {
            txtStatus.Visibility = Visibility.Collapsed;
            lblMagazin.Foreground = Brushes.Black;
            lblNume.Foreground = Brushes.Black;
            lblCategorie.Foreground = Brushes.Black;
            lblCuloar.Foreground = Brushes.Black;
            lblRaft.Foreground = Brushes.Black;

            errMagazin.Visibility = Visibility.Collapsed;
            errNume.Visibility = Visibility.Collapsed;
            errCategorie.Visibility = Visibility.Collapsed;
            errCuloar.Visibility = Visibility.Collapsed;
            errRaft.Visibility = Visibility.Collapsed;
        }

        private void CurataCampuri()
        {
            txtNume.Text = string.Empty;
            cmbCategorie.SelectedIndex = -1;
            txtCuloar.Text = string.Empty;
            txtRaft.Text = string.Empty;

            chkFaraZahar.IsChecked = false;
            chkFaraGluten.IsChecked = false;
            chkLocal.IsChecked = false;
            chkOferta.IsChecked = false;

            foreach (var copil in pnlMagazine.Children)
            {
                if (copil is RadioButton rb) rb.IsChecked = false;
            }
        }

        private void OnCautaClick(object sender, RoutedEventArgs e)
        {
            string numeCautat = txtCautaNume.Text.Trim();

            lstRezultateCautare.Items.Clear();
            txtStatusCautare.Visibility = Visibility.Collapsed;

            if (string.IsNullOrEmpty(numeCautat))
            {
                txtStatusCautare.Text = "Introduceți un nume pentru a căuta!";
                txtStatusCautare.Visibility = Visibility.Visible;
                return;
            }

            #modifica (acum iti preia toata lista si abia apoi verifica ce e bun)
            var toateProdusele = adminProduse.GetProduse();
            bool gasit = false;

            foreach (var p in toateProdusele)
            {
                if (p.Nume.IndexOf(numeCautat, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    lstRezultateCautare.Items.Add(p.Info());
                    gasit = true;
                }
            }

            if (!gasit)
            {
                txtStatusCautare.Text = "Nu s-a găsit niciun produs care să conțină acest nume.";
                txtStatusCautare.Visibility = Visibility.Visible;
            }
        }

        private void OnResetClick(object sender, RoutedEventArgs e)
        {
            CurataCampuri();
            ReseteazaErori();
        }

        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
