using System;

namespace ModeleMagazin
{
    public class Produs
    {
        private const char SEPARATOR = ';';

        public int Id { get; set; }
        public int MagazinId { get; set; }
        public string Nume { get; set; }
        public CategorieProdus Categorie { get; set; }
        public EticheteProdus Etichete { get; set; } // <-- NOU
        public int Culoar { get; set; }
        public int Raft { get; set; }

        // Constructor complet actualizat
        public Produs(int id, int magazinId, string nume, CategorieProdus categorie, EticheteProdus etichete, int culoar, int raft)
        {
            Id = id; MagazinId = magazinId; Nume = nume; Categorie = categorie; Etichete = etichete; Culoar = culoar; Raft = raft;
        }

        // Constructor de citire din fișier actualizat
        public Produs(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR);
            Id = int.Parse(date[0]);
            MagazinId = int.Parse(date[1]);
            Nume = date[2];
            Categorie = (CategorieProdus)int.Parse(date[3]);
            Etichete = (EticheteProdus)int.Parse(date[4]); // <-- NOU
            Culoar = int.Parse(date[5]);
            Raft = int.Parse(date[6]);
        }

        public Produs() { }

        public string ConversieLaSirPentruFisier()
        {
            // Am adăugat Etichetele în șirul de salvare
            return $"{Id}{SEPARATOR}{MagazinId}{SEPARATOR}{Nume}{SEPARATOR}{(int)Categorie}{SEPARATOR}{(int)Etichete}{SEPARATOR}{Culoar}{SEPARATOR}{Raft}";
        }

        public string Info() => $"[ID:{Id}] {Nume} ({Categorie}) [Etichete: {Etichete}] -> Magazin ID: {MagazinId} | Culoar: {Culoar}, Raft: {Raft}";
    }
}