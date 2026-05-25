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

           
            using (Stream stream = File.Open(numeFisier, FileMode.OpenOrCreate)) { }
        }

        
        public void AddMagazin(Magazin m)
        {
            
            using (StreamWriter streamWriter = new StreamWriter(numeFisier, true))
            {
                streamWriter.WriteLine(m.ConversieLaSirPentruFisier());
            }
        }

        public void DeleteMagazin(Magazin m)
        {
            throw new NotImplementedException();
        }

        public List<Magazin> GetMagazine()
        {
            List<Magazin> magazine = new List<Magazin>();

            using (StreamReader streamReader = new StreamReader(numeFisier))
            {
                string linieFisier;

                
                while ((linieFisier = streamReader.ReadLine()) != null)
                {
                    
                    magazine.Add(new Magazin(linieFisier));
                }
            }

            return magazine;
        }

        public void UpdateMagazin(Magazin m)
        {
            throw new NotImplementedException();
        }
    }
}