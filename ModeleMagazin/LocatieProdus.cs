using System;

namespace ModeleMagazin
{
    public class LocatieProdus
    {
        public string NumeMagazin { get; set; }
        public string NumeProdus { get; set; }
        public CategorieProdus Categorie { get; set; }
        public EticheteProdus Etichete { get; set; }

        // Locația fizică
        public int Culoar { get; set; }
        public int Raft { get; set; }

        public LocatieProdus(string magazin, string produs, CategorieProdus categorie, EticheteProdus etichete, int culoar, int raft)
        {
            NumeMagazin = magazin;
            NumeProdus = produs;
            Categorie = categorie;
            Etichete = etichete;
            Culoar = culoar;
            Raft = raft;
        }

        public string Info()
        {
            return $"Magazin: {NumeMagazin} | Produs: {NumeProdus} ({Categorie}, {Etichete}) -> Culoar: {Culoar}, Raft: {Raft}";
        }
    }
}