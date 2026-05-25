using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using LogicaMagazine;
using ModeleMagazin;
using System.Windows.Controls;

namespace AplicatieMagazin
{
    public partial class FereastraMagazine : Window
    {
        private IStocareMagazine adminMagazine;

        // Folosim ObservableCollection cum cere laboratorul pentru a actualiza automat UI-ul
        public ObservableCollection<Magazin> ListaMagazine { get; set; }

        public FereastraMagazine()
        {
            InitializeComponent();
            DataContext = this;

            adminMagazine = new AdministrareMagazineFisier("Magazine.txt");

            // Citim din fișier și transformăm în ObservableCollection
            var magazineDinFisier = adminMagazine.GetMagazine();
            ListaMagazine = new ObservableCollection<Magazin>(magazineDinFisier);

            // Setăm sursa de date pentru ListBox
            lstMagazine.ItemsSource = ListaMagazine;
        }

        // Bound properties shown in XAML (simple stubs)
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Filiala { get; set; }

        // --- READ: Când dăm click pe un magazin în listă ---
        private void OnMagazinSelectat(object sender, SelectionChangedEventArgs e)
        {
            if (lstMagazine.SelectedItem is Magazin magazinSelectat)
            {
                // Setăm DataContext-ul panoului din dreapta la magazinul selectat
                // Datorită DataBinding-ului, TextBox-urile se vor completa automat!
                panouDetalii.DataContext = magazinSelectat;
            }
        }

        // --- CREATE: Adăugare ---
        private void OnAdaugaClick(object sender, RoutedEventArgs e)
        {
            // Creăm un magazin nou cu un ID generat random pentru testare
            Magazin magazinNou = new Magazin(new Random().Next(100, 999), "Brand Nou", "Locație Nouă");

            adminMagazine.AddMagazin(magazinNou); // Salvăm în fișier
            ListaMagazine.Add(magazinNou);        // Adăugăm în UI (apare instant datorită ObservableCollection)

            lstMagazine.SelectedItem = magazinNou; // Îl selectăm automat
            MessageBox.Show("Magazin adăugat! Editați datele în panoul din dreapta și apăsați Actualizează.");
        }

        // --- UPDATE: Modificare ---
        private void OnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (lstMagazine.SelectedItem is Magazin magazinSelectat)
            {
                // Datorită Binding-ului TwoWay, 'magazinSelectat' are deja noile valori scrise în TextBox-uri!
                adminMagazine.UpdateMagazin(magazinSelectat);
                MessageBox.Show("Magazinul a fost actualizat cu succes în fișier!");
            }
        }

        // --- DELETE: Ștergere ---
        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            if (lstMagazine.SelectedItem is Magazin magazinSelectat)
            {
                // Ștergem din fișier (Asigură-te că ai metoda DeleteMagazin(Magazin m) în IStocareMagazine)
                adminMagazine.DeleteMagazin(magazinSelectat);

                // Ștergem din UI
                ListaMagazine.Remove(magazinSelectat);
                panouDetalii.DataContext = null; // Curățăm formularele

                MessageBox.Show("Magazinul a fost șters!");
            }
            else
            {
                MessageBox.Show("Selectați un magazin din listă mai întâi!");
            }
        }
    }
}