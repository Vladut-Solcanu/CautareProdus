using System.Collections.Generic;
using System.IO;
using ModeleMagazin;

namespace LogicaMagazine
{
    public class AdministrareProduseFisier : IStocareProduse
    {
        private string numeFisier;

        public AdministrareProduseFisier(string numeFisier)
        {
            this.numeFisier = numeFisier;
           
            using (Stream stream = File.Open(numeFisier, FileMode.OpenOrCreate)) { }
        }

        public void AddProdus(Produs p)
        {
           
            using (StreamWriter sw = new StreamWriter(numeFisier, true))
            {
                sw.WriteLine(p.ConversieLaSirPentruFisier());
            }
        }

        public List<Produs> GetProduse()
        {
            List<Produs> produse = new List<Produs>();
            using (StreamReader sr = new StreamReader(numeFisier))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    produse.Add(new Produs(linie));
                }
            }
            return produse;
        }

        
        public void UpdateProdus(Produs produsActualizat)
        {
            List<Produs> produse = GetProduse();

            
            using (StreamWriter sw = new StreamWriter(numeFisier, false))
            {
                foreach (var p in produse)
                {
                    if (p.Id == produsActualizat.Id)
                    {
                        sw.WriteLine(produsActualizat.ConversieLaSirPentruFisier());
                    }
                    else
                    {
                        sw.WriteLine(p.ConversieLaSirPentruFisier());
                    }
                }
            }
        }
    }
}