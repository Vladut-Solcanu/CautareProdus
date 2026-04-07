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
        public int Culoar { get; set; }
        public int Raft { get; set; }

       
        public Produs(int id, int magazinId, string nume, CategorieProdus categorie, int culoar, int raft)
        {
            Id = id; MagazinId = magazinId; Nume = nume; Categorie = categorie; Culoar = culoar; Raft = raft;
        }

        public Produs(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR);
            Id = int.Parse(date[0]);
            MagazinId = int.Parse(date[1]); 
            Nume = date[2];               
            Categorie = (CategorieProdus)int.Parse(date[3]);
            Culoar = int.Parse(date[4]);
            Raft = int.Parse(date[5]);
        }

        public Produs()
        {
        }

        public string ConversieLaSirPentruFisier()
        {
           
            return $"{Id}{SEPARATOR}{MagazinId}{SEPARATOR}{Nume}{SEPARATOR}{(int)Categorie}{SEPARATOR}{Culoar}{SEPARATOR}{Raft}";
        }

        public string Info() => $"[ID:{Id}] {Nume} ({Categorie}) -> Magazin ID: {MagazinId} | Culoar: {Culoar}, Raft: {Raft}";
    }
}