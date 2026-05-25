using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModeleMagazin
{
    public class Produs : INotifyPropertyChanged, IDataErrorInfo
    {
        private const char SEPARATOR = ';';

        private int id;
        private int magazinId;
        private string nume;
        private decimal pret; // NOU: Prețul produsului
        private CategorieProdus categorie;
        private EticheteProdus etichete;
        private int culoar;
        private int raft;

        public int Id { get => id; set { id = value; OnPropertyChanged(); } }
        public int MagazinId { get => magazinId; set { magazinId = value; OnPropertyChanged(); OnPropertyChanged(nameof(NumeMagazin)); } }
        public string Nume { get => nume; set { nume = value; OnPropertyChanged(); } }
        public decimal Pret { get => pret; set { pret = value; OnPropertyChanged(); } } // NOU
        public CategorieProdus Categorie { get => categorie; set { categorie = value; OnPropertyChanged(); } }
        public EticheteProdus Etichete { get => etichete; set { etichete = value; OnPropertyChanged(); } }
        public int Culoar { get => culoar; set { culoar = value; OnPropertyChanged(); } }
        public int Raft { get => raft; set { raft = value; OnPropertyChanged(); } }

        public string NumeMagazin
        {
            get
            {
                switch (MagazinId)
                {
                    case 1: return "Lidl";
                    case 2: return "Kaufland";
                    case 3: return "Profi";
                    default: return $"Magazin #{MagazinId}";
                }
            }
        }

        public Produs() { }

        // Constructor actualizat
        public Produs(int id, int magazinId, string nume, decimal pret, CategorieProdus categorie, EticheteProdus etichete, int culoar, int raft)
        {
            Id = id; MagazinId = magazinId; Nume = nume; Pret = pret;
            Categorie = categorie; Etichete = etichete; Culoar = culoar; Raft = raft;
        }

        // Citirea din fișier (protejată împotriva produselor vechi fără preț)
        public Produs(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR);
            Id = int.Parse(date[0]);
            MagazinId = int.Parse(date[1]);
            Nume = date[2];
            Categorie = (CategorieProdus)int.Parse(date[3]);
            Etichete = (EticheteProdus)int.Parse(date[4]);
            Culoar = int.Parse(date[5]);
            Raft = int.Parse(date[6]);

            // Verificăm dacă linia veche are și preț, altfel punem 0
            if (date.Length > 7) Pret = decimal.Parse(date[7]);
            else Pret = 0;
        }

        // Salvarea în fișier (acum include prețul la final)
        public string ConversieLaSirPentruFisier() => $"{Id}{SEPARATOR}{MagazinId}{SEPARATOR}{Nume}{SEPARATOR}{(int)Categorie}{SEPARATOR}{(int)Etichete}{SEPARATOR}{Culoar}{SEPARATOR}{Raft}{SEPARATOR}{Pret}";

        public override string ToString() => $"[ID:{Id}] {Nume} - {Pret} Lei -> {NumeMagazin}";

        // --- VALIDARE MVVM ---
        public string Error => null;
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Nume):
                        if (string.IsNullOrWhiteSpace(Nume)) return "Obligatoriu!";
                        if (Nume.Length > 15) return "Maxim 15 caractere!";
                        break;
                    case nameof(Pret): // NOU: Validare Preț
                        if (Pret <= 0) return "Prețul trebuie să fie > 0!";
                        break;
                    case nameof(Culoar):
                        if (Culoar <= 0 || Culoar > 20) return "Invalid (1-20)!";
                        break;
                    case nameof(Raft):
                        if (Raft <= 0 || Raft > 50) return "Invalid (1-50)!";
                        break;
                }
                return null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}