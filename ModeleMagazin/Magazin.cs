using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModeleMagazin
{
    // Implementăm interfața cerută de laborator pentru Binding bidirecțional
    public class Magazin : INotifyPropertyChanged
    {
        private int id;
        private string brand;
        private string filiala;

        public int Id
        {
            get => id;
            set { id = value; OnPropertyChanged(); }
        }

        public string Brand
        {
            get => brand;
            set { brand = value; OnPropertyChanged(); }
        }

        public string Filiala
        {
            get => filiala;
            set { filiala = value; OnPropertyChanged(); }
        }

        public Magazin(int id, string brand, string filiala)
        {
            Id = id; Brand = brand; Filiala = filiala;
        }

        public Magazin(string linieFisier)
        {
            string[] date = linieFisier.Split(';');
            Id = int.Parse(date[0]);
            Brand = date[1];
            Filiala = date[2];
        }

        public Magazin() { }

        public string ConversieLaSirPentruFisier() => $"{Id};{Brand};{Filiala}";

        public override string ToString() => $"{Brand} - Filiala: {Filiala} (ID: {Id})";

        // --- Mecanismul de notificare cerut de WPF ---
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}