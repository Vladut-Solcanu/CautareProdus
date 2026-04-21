using System;
using System.Windows;
using System.Windows.Media;
using LogicaMagazine;
using ModeleMagazin;

namespace AplicatieMagazin
{
    public partial class MainWindow : Window
    {
        // Limite pentru validare
        private const int MAX_LUNGIME_NUME = 15;
        private const int MAX_CULOAR = 20;
        private const int MAX_RAFT = 50;

        // Administratorii pentru lucrul cu fișierele
        private IStocareProduse adminProduse;
        private IStocareMagazine adminMagazine;

        public MainWindow()
        {
            InitializeComponent();

            // 1. Inițializăm legătura cu fișierele text
            adminProduse = new AdministrareProduseFisier("Produse.txt");
            adminMagazine = new AdministrareMagazineFisier("Magazine.txt");

            // --- 2. LOGICA DE ADĂUGARE AUTOMATĂ (SEEDING) ---
            var magazineExistente = adminMagazine.GetMagazine();

            // Dacă fișierul de magazine e gol, le cream noi automat pe primele 3
            if (magazineExistente.Count == 0)
            {
                adminMagazine.AddMagazin(new Magazin(1, "Lidl", "Centru"));
                adminMagazine.AddMagazin(new Magazin(2, "Kaufland", "Vest"));
                adminMagazine.AddMagazin(new Magazin(3, "Profi", "Est"));

                // Reîncărcăm lista din fișier după ce le-am adăugat
                magazineExistente = adminMagazine.GetMagazine();
            }

            // 3. Umplem meniul derulant cu Magazine
            cmbMagazin.ItemsSource = magazineExistente;
            cmbMagazin.DisplayMemberPath = "Brand"; // Afișăm pe ecran doar Numele Brandului

            // 4. Umplem meniul derulant cu Categoriile din Enum
            cmbCategorie.ItemsSource = Enum.GetValues(typeof(CategorieProdus));
        }

        private void OnAdaugaClick(object sender, RoutedEventArgs e)
        {
            // Ascundem erorile vechi înainte de o nouă verificare
            ReseteazaErori();

            // Executăm salvarea DOAR dacă datele introduse sunt corecte
            if (ValideazaDateProdus())
            {
                try
                {
                    // Preluăm magazinul selectat pentru a-i afla ID-ul
                    Magazin magazinSelectat = (Magazin)cmbMagazin.SelectedItem;
                    int idMagazin = magazinSelectat.Id;

                    string nume = txtNume.Text;
                    CategorieProdus categorieSelectata = (CategorieProdus)cmbCategorie.SelectedItem;

                    int culoar = int.Parse(txtCuloar.Text);
                    int raft = int.Parse(txtRaft.Text);

                    // Generăm un ID temporar pentru produs
                    Random rnd = new Random();
                    int idNou = rnd.Next(1, 10000);

                    // Creăm obiectul folosind constructorul tău complet
                    Produs produsNou = new Produs(idNou, idMagazin, nume, categorieSelectata, culoar, raft);

                    // Salvăm fizic în fișier (la finalul listei din Produse.txt)
                    adminProduse.AddProdus(produsNou);

                    // Afișăm un mesaj de succes verde în josul ecranului
                    txtStatus.Text = $"Produsul '{nume}' a fost salvat cu succes în {magazinSelectat.Brand}!";
                    txtStatus.Foreground = Brushes.Green;
                    txtStatus.Visibility = Visibility.Visible;

                    // Curățăm câmpurile ca să fim gata pentru următorul produs
                    CurataCampuri();
                }
                catch (Exception ex)
                {
                    // Dacă apare o problemă la scrierea în fișier
                    txtStatus.Text = "A apărut o eroare la salvare: " + ex.Message;
                    txtStatus.Foreground = Brushes.Red;
                    txtStatus.Visibility = Visibility.Visible;
                }
            }
        }

        private bool ValideazaDateProdus()
        {
            bool dateValide = true;

            // Validare Magazin
            if (cmbMagazin.SelectedItem == null)
            {
                lblMagazin.Foreground = Brushes.Red;
                errMagazin.Visibility = Visibility.Visible;
                dateValide = false;
            }

            // Validare Nume Produs
            if (string.IsNullOrWhiteSpace(txtNume.Text) || txtNume.Text.Length > MAX_LUNGIME_NUME)
            {
                lblNume.Foreground = Brushes.Red;
                errNume.Visibility = Visibility.Visible;
                dateValide = false;
            }

            // Validare Categorie
            if (cmbCategorie.SelectedItem == null)
            {
                lblCategorie.Foreground = Brushes.Red;
                errCategorie.Visibility = Visibility.Visible;
                dateValide = false;
            }

            // Validare Culoar
            if (!int.TryParse(txtCuloar.Text, out int culoarVal) || culoarVal <= 0 || culoarVal > MAX_CULOAR)
            {
                lblCuloar.Foreground = Brushes.Red;
                errCuloar.Visibility = Visibility.Visible;
                dateValide = false;
            }

            // Validare Raft
            if (!int.TryParse(txtRaft.Text, out int raftVal) || raftVal <= 0 || raftVal > MAX_RAFT)
            {
                lblRaft.Foreground = Brushes.Red;
                errRaft.Visibility = Visibility.Visible;
                dateValide = false;
            }

            return dateValide; // Va returna TRUE doar dacă toate if-urile de mai sus au fost evitate
        }

        private void ReseteazaErori()
        {
            txtStatus.Visibility = Visibility.Collapsed;

            // Readucem culorile etichetelor la negru
            lblMagazin.Foreground = Brushes.Black;
            lblNume.Foreground = Brushes.Black;
            lblCategorie.Foreground = Brushes.Black;
            lblCuloar.Foreground = Brushes.Black;
            lblRaft.Foreground = Brushes.Black;

            // Ascundem textele roșii de eroare
            errMagazin.Visibility = Visibility.Collapsed;
            errNume.Visibility = Visibility.Collapsed;
            errCategorie.Visibility = Visibility.Collapsed;
            errCuloar.Visibility = Visibility.Collapsed;
            errRaft.Visibility = Visibility.Collapsed;
        }

        private void CurataCampuri()
        {
            cmbMagazin.SelectedIndex = -1;
            txtNume.Text = string.Empty;
            cmbCategorie.SelectedIndex = -1;
            txtCuloar.Text = string.Empty;
            txtRaft.Text = string.Empty;
        }
    }
}