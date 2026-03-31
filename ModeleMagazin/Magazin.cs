using System;

namespace ModeleMagazin
{
    public class Magazin
    {
        private const char SEPARATOR = ';';

        public int Id { get; set; }
        public string Brand { get; set; }
        public string Filiala { get; set; }

        public Magazin(int id, string brand, string filiala)
        {
            Id = id; Brand = brand; Filiala = filiala;
        }

        public Magazin(string linieFisier)
        {
            string[] date = linieFisier.Split(SEPARATOR);
            Id = int.Parse(date[0]);
            Brand = date[1];
            Filiala = date[2];
        }

        public string ConversieLaSirPentruFisier() => $"{Id}{SEPARATOR}{Brand}{SEPARATOR}{Filiala}";
        public string Info() => $"[ID:{Id}] {Brand} - {Filiala}";
    }
}