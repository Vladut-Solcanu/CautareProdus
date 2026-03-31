using System;
using System.Collections.Generic;
using System.IO;
using ModeleMagazin;

namespace LogicaMagazine
{
    public class AdministrareMagazineFisier : IStocareMagazine
    {
        private string numeFisier;

        public AdministrareMagazineFisier(string numeFisier)
        {
            this.numeFisier = numeFisier;

            // Această instrucțiune 'using' creează fișierul pe hard disk
            // dacă acesta nu există deja, ca să evităm erorile la citire.
            using (Stream stream = File.Open(numeFisier, FileMode.OpenOrCreate)) { }
        }

        // Metoda care adaugă un magazin nou la finalul fișierului
        public void AddMagazin(Magazin m)
        {
            // Parametrul 'true' de la StreamWriter înseamnă "Append" (adaugă la final, nu șterge ce era)
            using (StreamWriter streamWriter = new StreamWriter(numeFisier, true))
            {
                streamWriter.WriteLine(m.ConversieLaSirPentruFisier());
            }
        }

        // Metoda care citește toate magazinele din fișier și le returnează ca o Listă
        public List<Magazin> GetMagazine()
        {
            List<Magazin> magazine = new List<Magazin>();

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                // Citește linie cu linie până când ajunge la finalul fișierului (null)
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    // Folosește constructorul special din clasa Magazin care știe să "spargă" linia de text
                    magazine.Add(new Magazin(linieFisier));
                }
            }

            return magazine;
        }
    }
}