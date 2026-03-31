using System;

namespace ModeleMagazin
{
    public class Produs
    {
        private const char SEPARATOR = ';';

        public int Id { get; set; }
        public string Nume { get; set; }
        public CategorieProdus Categorie { get; set; }
        public int Culoar { get; set; }
        public int Raft { get; set; }

        public Produs(int id, string nume, CategorieProdus categorie, int culoar, int raft)
        {
            Id = id; Nume = nume; Categorie = categorie; Culoar = culoar; Raft = raft;
        }

        public Produs(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR);
            Id = int.Parse(date[0]);
            Nume = date[1];
            Categorie = (CategorieProdus)int.Parse(date[2]);
            Culoar = int.Parse(date[3]);
            Raft = int.Parse(date[4]);
        }

        
        public string ConversieLaSirPentruFisier()
        {
            return $"{Id}{SEPARATOR}{Nume}{SEPARATOR}{(int)Categorie}{SEPARATOR}{Culoar}{SEPARATOR}{Raft}";
        }

        public string Info() => $"[ID:{Id}] {Nume} ({Categorie}) -> Culoar: {Culoar}, Raft: {Raft}";
    }
}